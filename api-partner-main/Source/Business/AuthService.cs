namespace App;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using Google.Apis.Auth;
using Tool.Compet.Http;
using Microsoft.EntityFrameworkCore;
using Tool.Compet.Core;
using Microsoft.AspNetCore.Identity;

/// For 3rd-authentication provider (google, facebook,...)
/// https://developers.google.com/identity/one-tap/android/idtoken-auth
/// https://developers.google.com/api-client-library

/// AccessToken and RefreshToken: https://codepedia.info/aspnet-core-jwt-refresh-token-authentication
public class AuthService : BaseService {
	private readonly ILogger<AuthService> logger;
	private readonly TokenService tokenService;
	private readonly CardanoNodeRepo cardanoNodeRepo;

	public AuthService(
		AppDbContext dbContext,
		IOptions<AppSetting> appSettingOpt,
		ILogger<AuthService> logger,
		TokenService tokenService,
		CardanoNodeRepo cardanoNodeRepo
	) : base(dbContext, appSettingOpt) {
		this.logger = logger;
		this.tokenService = tokenService;
		this.cardanoNodeRepo = cardanoNodeRepo;
	}

	public async Task<ApiResponse> LogIn(LoginRequestBody reqBody, string? ipAddress, string? userAgent) {
		var query =
			from _user in this.dbContext.users
			where _user.email == reqBody.email
			select new {
				_user
			}
		;
		var result = await query.FirstOrDefaultAsync();
		if (result is null) {
			return new ApiNotFoundResponse("Not found user");
		}

		var user = result._user;

		// Check password
		var passwordHasher = new PasswordHasher<UserModel>();
		if (passwordHasher.VerifyHashedPassword(user, user.password, reqBody.password) != PasswordVerificationResult.Success) {
			return new ApiBadRequestResponse("Incorrect login info");
		}

		return await this._BuildClaimsAndDoLogin(user, ipAddress, userAgent, reqBody.client_type);
	}

	/// Create new user if not exist, then Login the user.
	public async Task<ApiResponse> LogInWithProvider(ProviderLoginRequestBody in_reqBody, string? ipAddress, string? userAgent) {
		ProviderUser? providerUser = null;
		string? providerShortName = null;
		byte provider = UserModelConst.PROVIDER_MANUAL;

		switch (in_reqBody.provider) {
			case AppConst.AUTH_PROVIDER_GOOGLE: {
				providerShortName = "gg";
				provider = UserModelConst.PROVIDER_GOOGLE;
				providerUser = await this._GetUserFromGoogle(in_reqBody.id_token, in_reqBody.access_token);
				break;
			}
			case AppConst.AUTH_PROVIDER_FACEBOOK: {
				providerShortName = "fb";
				provider = UserModelConst.PROVIDER_FACEBOOK;
				providerUser = await this._GetUserFromFacebook(in_reqBody.id_token);
				break;
			}
			default: {
				return new ApiBadRequestResponse("Invalid provider");
			}
		}

		if (providerUser == null) {
			return new ApiBadRequestResponse("Wrong token");
		}

		// Create new user if not exist
		var user = await this._GetOrCreateUser(providerShortName, providerUser.email, provider);
		if (user == null) {
			return new ApiInternalServerErrorResponse("Could not create new user");
		}

		// Perform login
		return await _BuildClaimsAndDoLogin(user, ipAddress, userAgent, in_reqBody.client_type);
	}

	private async Task<UserModel?> _GetOrCreateUser(string providerShortName, string email, byte provider) {
		var query =
			from _user in this.dbContext.users
			where _user.email == email
			select new {
				_user
			}
		;
		var result = await query.FirstOrDefaultAsync();
		if (result != null) {
			return (result._user);
		}

		// Start transaction.
		// Ref: https://docs.microsoft.com/en-us/ef/core/saving/transactions
		using var transaction = await this.dbContext.Database.BeginTransactionAsync();

		try {
			// Create new user with extra work
			var userCmp = new UserComponent(this.dbContext, this.cardanoNodeRepo, this.appSetting);
			var newUserTuple = await userCmp.CreateUserOrThrow(
				role: UserModelConst.ROLE_USER,
				email: email,
				password: null,
				provider: provider
			);

			// Save changes and Commit
			await this.dbContext.SaveChangesAsync();
			await transaction.CommitAsync();

			return newUserTuple;
		}
		catch (Exception e) {
			// Write filelog
			this.logger.CriticalDk(this, $"Could not create new user from email: {email}, error: {e.Message}");

			await transaction.RollbackAsync();

			return (null);
		}
	}


	private async Task<ApiResponse> _BuildClaimsAndDoLogin(UserModel user, string? ipAddress, string? userAgent, byte clientType) {
		var claims = new[] {
			// Subject of the site
			new Claim(JwtRegisteredClaimNames.Sub, this.appSetting.jwt.subject),
			// This is id of jwt (should different between issuers)
			new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToStringDk()),
			// Issued at
			new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
			// Attach our custom claim
			new Claim(AppConst.jwt.claim_type_user_id, user.id.ToStringDk()),
			new Claim(AppConst.jwt.claim_type_user_role, user.role.ToString()),
			new Claim(AppConst.jwt.claim_type_client_type, clientType.ToString()),
		};

		return await this._DoLogin(false, user.id, claims, ipAddress, userAgent, clientType);
	}

	private async Task<ApiResponse> _DoLogin(bool silent, Guid userId, IEnumerable<Claim> claims, string? ipAddress, string? userAgent, byte clientType) {
		await this.IncreaseCardanoSyncPriorityForUserAsync(userId);

		var refreshToken = this.tokenService.GenerateRefreshToken();
		var accessToken = this.tokenService.GenerateAccessToken(claims);

		try {
			// Add access
			this.dbContext.userAuthTokens.Attach(new() {
				user_id = userId,
				token = refreshToken,
				client_type = clientType,
				token_expired_at = DateTime.Now.AddSeconds(this.appSetting.jwt.refreshExpiresInSeconds),
				created_by_ip = ipAddress,
				created_by_agent = userAgent,
			});
			await this.dbContext.SaveChangesAsync();

			return new LoginResponse {
				data = new() {
					token_schema = "Bearer",
					access_token = accessToken,
					refresh_token = refreshToken
				}
			};
		}
		catch (Exception e) {
			return new ApiInternalServerErrorResponse(this.appSetting.debug ? e.Message : "Could not login");
		}
	}

	public async Task<ApiResponse> LogOut(Guid userId, LogoutRequestBody requestBody, string? ipAddress, string? userAgent) {
		var user = await this.FindUserByIdAsync(userId);
		if (user == null) {
			return new ApiBadRequestResponse("User not found");
		}

		// Revoke all accesses (even though tokens are active).
		var query = this.dbContext.userAuthTokens.Where(m => m.revoked_at == null && m.user_id == user.id);
		if (!requestBody.logout_everywhere) {
			query = query.Where(m => m.token == requestBody.refresh_token);
		}

		var accesses = await query.ToArrayAsync();
		if (accesses != null && accesses.Length > 0) {
			foreach (var access in accesses) {
				access.revoked_at = DateTime.Now;
				access.revoked_by_token = requestBody.refresh_token;
				access.revoked_by_ip = ipAddress;
				access.revoked_by_agent = userAgent;
			}
			await this.dbContext.SaveChangesAsync();

			return new ApiSuccessResponse("Logged out" + (requestBody.logout_everywhere ? " everywhere" : ""));
		}

		return new ApiSuccessResponse("Nothing affected");
	}

	/// @return Nullable userId in Guid.
	private Guid? _ValidateAccessToken(string accessToken) {
		try {
			new JwtSecurityTokenHandler().ValidateToken(accessToken, new TokenValidationParameters {
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSetting.jwt.key)),

				ValidateIssuer = true,
				ValidIssuer = appSetting.jwt.issuer,

				ValidateAudience = true,
				ValidAudience = appSetting.jwt.audience,

				// Set clockskew to zero so tokens expire exactly at
				// token expiration time (instead of 5 minutes later)
				ClockSkew = TimeSpan.Zero
			}, out var validatedToken);

			var jwtToken = (JwtSecurityToken)validatedToken;
			var userId = jwtToken.Claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Name)?.Value;
			if (userId == null) {
				return null;
			}

			return new Guid(userId);
		}
		catch (Exception e) {
		}

		return null;
	}

	/// @param idToken: This is jwt.
	/// @param accessToken: This is jwt.
	private async Task<ProviderUser?> _GetUserFromGoogle(string? idToken, string? accessToken) {
		try {
			if (idToken != null) {
				var response = await GoogleJsonWebSignature.ValidateAsync(idToken);
				return new ProviderUser {
					email = response.Email
				};
			}

			if (accessToken != null) {
				// To get user info: https://www.googleapis.com/oauth2/v3/userinfo?access_token=<access token>
				// To get token info: https://www.googleapis.com/oauth2/v3/tokeninfo?access_token=<access token>
				var url = $"https://www.googleapis.com/oauth2/v3/tokeninfo?access_token={accessToken}";
				var response = await new DkHttpClient().GetRaw<GoogleAccessTokenDecodingResponse>(url);

				if (response != null && response.email != null) {
					return new ProviderUser {
						email = response.email
					};
				}
			}

			return null;
		}
		catch {
			return null;
		}
	}

	private async Task<ProviderUser> _GetUserFromFacebook(string idToken) {
		try {
			//todo: impl for fb
			var response = await GoogleJsonWebSignature.ValidateAsync(idToken);

			return new ProviderUser {
				email = response.Email
			};
		}
		catch {
			return null;
		}
	}

	private class ProviderUser {
		public string email;
	}
}
