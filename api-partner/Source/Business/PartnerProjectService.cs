namespace App;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using Tool.Compet.Json;
using System.Transactions;
using System.Web;
using Tool.Compet.Core;
using Tool.Compet.EntityFrameworkCore;

/// Raw query with interpolated: https://docs.microsoft.com/en-us/ef/core/querying/raw-sql
public class PartnerProjectService : BaseService {

	private readonly ILogger<PartnerProjectService> logger;
	private readonly CardanoNodeRepo cardanoNodeRepo;
	private readonly MailService mailService;
	public PartnerProjectService(
		AppDbContext dbContext,
		CardanoNodeRepo cardanoNodeRepo,
		ILogger<PartnerProjectService> logger,
		MailService mailService,
		IOptions<AppSetting> appSettingOpt
	) : base(dbContext, appSettingOpt) {
		this.logger = logger;
		this.cardanoNodeRepo = cardanoNodeRepo;
		this.mailService = mailService;
	}

	public async Task<ActionResult<ApiResponse>> CreateProject(CreateProjectRequestBody requestBody) {
		using var transaction = await this.dbContext.Database.BeginTransactionAsync();
		try {

			var project = await this.dbContext.partnerProjects.FirstOrDefaultAsync(m =>
				m.project_name == requestBody.project_name && m.partner_id == requestBody.partner_id
			);

			if (project != null) {
				return new ApiBadRequestResponse("project name already exist");
			}

			var newProject = new PartnerProjectModel {
				partner_id = requestBody.partner_id,
				project_code = Helper.GenerateAlphabetCode(),
				project_name = requestBody.project_name,
				wallet_address = requestBody.wallet_address,
				secret_key = Helper.GenerateSecretKey(),
				url_callback = requestBody.url_callback,
				created_at = DateTime.Now,
				status = PartnerProjectModelConst.STATUS_ACTIVE
			};

			this.dbContext.partnerProjects.Attach(newProject);

			await this.dbContext.SaveChangesAsync();
			await transaction.CommitAsync();
			return new ApiSuccessResponse("Cretae project success");
		}
		catch (Exception e) {
			await transaction.RollbackAsync();
			return new ApiBadRequestResponse(this.appSetting.debug ? e.Message : "Create project fail");
		}
	}
	public async Task<ApiResponse> ListProject(GetListProjectRequestBody requestBody) {
		var projects = from _project in this.dbContext.partnerProjects
									 where _project.partner_id == requestBody.partner_id && _project.deleted_at == null
									 select new {
										 _project = _project
									 };

		var pagedResult = await projects.PaginateDk(requestBody.perPage, requestBody.limit);
		if (pagedResult.items.IsEmptyDk()) {
			return new ApiSuccessResponse("Empty");
		}

		var pagedResultItems = pagedResult.items!;
		var data_projects = new ProjectListResponse.project[pagedResultItems.Length];

		for (int index = 0, N = pagedResultItems.Length; index < N; ++index) {
			var pagedResultItem = pagedResultItems[index];
			var project = pagedResultItem._project;

			data_projects[index] = new() {
				id = project.id,
				project_code = project.project_name,
				project_name = project.project_name,
				url_callback = project.url_callback,
				wallet_address = project.wallet_address,
				secret_key = project.secret_key,
				status = project.status
			};
		}

		return new ProjectListResponse {
			data = new() {
				page_pos = pagedResult.pagePos,
				page_count = pagedResult.pageCount,
				total_item_count = pagedResult.totalItemCount,
				projects = data_projects,
			}
		};
	}

	public async Task<ApiResponse> removeProject(long id) {

		var partnerProject = await this.dbContext.partnerProjects.FirstOrDefaultAsync(m =>
			m.id == id && m.deleted_at == null
		);

		if (partnerProject == null) {
			return new ApiNotFoundResponse();
		}

		partnerProject.status = PartnerProjectModelConst.STATUS_INACTIVE;
		partnerProject.deleted_at = DateTime.Now;
		await this.dbContext.SaveChangesAsync();

		return new ApiSuccessResponse();

	}
}
