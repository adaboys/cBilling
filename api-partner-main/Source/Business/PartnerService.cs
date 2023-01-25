namespace App;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using Tool.Compet.Json;
using System.Transactions;
using System.Web;
using Tool.Compet.Core;

/// Raw query with interpolated: https://docs.microsoft.com/en-us/ef/core/querying/raw-sql
public class PartnerService : BaseService {

	private readonly ILogger<PartnerService> logger;
	private readonly CardanoNodeRepo cardanoNodeRepo;
	private readonly MailService mailService;
	public PartnerService(
		AppDbContext dbContext,
		CardanoNodeRepo cardanoNodeRepo,
		ILogger<PartnerService> logger,
		MailService mailService,
		IOptions<AppSetting> appSettingOpt
	) : base(dbContext, appSettingOpt) {
		this.logger = logger;
		this.cardanoNodeRepo = cardanoNodeRepo;
		this.mailService = mailService;
	}

	public async Task<ActionResult<ApiResponse>> AttemptRegister(RegisterPartnerTempRequestBody requestBody) {

		using var transaction = await this.dbContext.Database.BeginTransactionAsync();
		try {
			var partner = await this.dbContext.partners.FirstOrDefaultAsync(m =>
				(m.email == requestBody.email || m.telno == requestBody.phone) && m.status == PartnerModelConst.STATUS_ACTIVE
			);

			if (partner != null) {
				return new ApiBadRequestResponse("Email or phone number already exists");
			}

			// Check to avoid multiple attempts
			var userRegistry = await this.dbContext.userRegistries.FirstOrDefaultAsync(m =>
				m.email == requestBody.email && m.expired_at > DateTime.Now && m.user_type == UserRegistryModelConst.TYPE_PARTNER
			);

			if (userRegistry != null) {
				return new ApiBadRequestResponse("Should check email").AlsoDk(res => res.code = "duplicated_register");
			}

			var optCode = Helper.GenerateOtpCode();
			var newUserRegister = new UserRegistryModel {
				token = optCode,
				email = requestBody.email,
				user_type = UserRegistryModelConst.TYPE_PARTNER,
				expired_at = DateTime.Now.AddMinutes(10),
			};
			this.dbContext.userRegistries.Attach(newUserRegister);

			// Send mail to user
			var sendMailResponse = await this.mailService.Send(
				toEmail: requestBody.email,
				subject: "Register partner confirmation",
				body: $"Your verification code is: {optCode}. Please do NOT share with anyone."
			);

			// Force rollback
			if (sendMailResponse.failed) {
				throw new SystemException(sendMailResponse.message);
			}

			await this.dbContext.SaveChangesAsync();
			await transaction.CommitAsync();
			return new ApiSuccessResponse("Register partner success");
		}
		catch (Exception e) {
			await transaction.RollbackAsync();
			return new ApiBadRequestResponse(this.appSetting.debug ? e.Message : "Register partner fail");
		}
	}

	public async Task<ApiResponse> ConfirmRegister(RegisterPartnerConfirmRequestBody requestBody) {
		var partner = await this.dbContext.partners.FirstOrDefaultAsync(m =>
			m.email == requestBody.email || m.telno == requestBody.phone
		);
		if (partner is not null) {
			return new ApiBadRequestResponse("Partner existed").AlsoDk(res => res.code = "partner_existed");
		}

		var userRegistry = await this.dbContext.userRegistries.FirstOrDefaultAsync(m =>
			m.email == requestBody.email &&
			m.token == requestBody.otp &&
			m.confirmed_at == null &&
			m.expired_at > DateTime.Now &&
			m.user_type == UserRegistryModelConst.TYPE_PARTNER
		);
		if (userRegistry is null) {
			return new ApiBadRequestResponse("Partner Invalid or confirmed registeration").AlsoDk(res => res.code = "invalid_register");
		}

		using (var txScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled)) {
			try {

				userRegistry = await this.dbContext.userRegistries
					.FromSqlRaw($"SELECT * FROM {DbConst.table_user_registry} WITH (UPDLOCK) WHERE id = {{0}}", userRegistry.id)
					.FirstOrDefaultAsync();

				if (userRegistry == null || userRegistry.confirmed_at != null) {
					return new ApiBadRequestResponse("Partner Invalid or confirmed registeration");
				}

				var passwordHasher = new PasswordHasher<PartnerModel>();
				var newPartner = new PartnerModel {
					status = PartnerModelConst.STATUS_ACTIVE,
					telno = requestBody.phone,
					email = requestBody.email,
					name = requestBody.name,
					code = Helper.GenerateAlphabetCode(),
					created_at = DateTime.Now,
				};
				newPartner.password = passwordHasher.HashPassword(newPartner, requestBody.password);
				this.dbContext.partners.Attach(newPartner);

				userRegistry.confirmed_at = DateTime.Now;
				await this.dbContext.SaveChangesAsync();

				return new ApiSuccessResponse();
			}
			catch (Exception e) {
				return new ApiInternalServerErrorResponse(this.appSetting.debug ? e.Message : "Could not register new partner");
			}
			finally {
				txScope.Complete();
			}
		}
	}

	public async Task<ApiResponse> GetPartnerProfile(String code) {
		var query =
			from _partner in this.dbContext.partners
			where _partner.code == code && _partner.status == PartnerModelConst.STATUS_ACTIVE
			select new {
				_partner,
			};

		var result = await query.FirstOrDefaultAsync();

		if (result == null) {
			return new ApiNotFoundResponse();
		}

		return new PartnerResponse {
			data = new() {
				phone = result._partner.telno,
				code = result._partner.code,
				email = result._partner.email,
				name = result._partner.name,
			}
		};

	}
}
