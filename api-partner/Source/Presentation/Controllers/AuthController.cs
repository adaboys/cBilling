namespace App;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tool.Compet.Core;

/// Framework will auto convert our result type to client's request Accept-Content type.
/// By default, it implicit convert response R to ActionResult<R>.
/// See https://docs.microsoft.com/en-us/aspnet/core/web-api/action-return-types?view=aspnetcore-6.0
[ApiController, Route(Routes.api_prefix)]
public class AuthController : BaseController {
	private readonly AuthService authService;

	public AuthController(AuthService authService) {
		this.authService = authService;
	}

	/// <summary>
	/// Login an user with email/password.
	/// </summary>
	/// <param name="requestBody">
	///	- client_type: 1 (app-android), 2 (app-ios), 3 (web),
	/// </param>
	/// <response code="200"></response>
	[HttpPost, Route(Routes.auth_login)]
	public async Task<ActionResult<ApiResponse>> LogIn([FromBody] LoginRequestBody requestBody) {
		DkReflections.TrimJsonAnnotatedProperties(requestBody);

		return await authService.LogIn(requestBody, RequestHelper.GetClientIp(this.Request), RequestHelper.GetUserAgent(this.Request));
	}

	/// <summary>
	/// Login or Signup an user.
	/// </summary>
	/// <param name="requestBody">
	/// - provider: One of: google, facebook.
	/// - id_token: Client get this field after login with the provider (can be null, but must provide access_token)
	/// - access_token: Client get this field after login with the provider (can be null, but must provide id_token)
	/// - client_type: 1 (app-android), 2 (app-ios), 3 (web),
	/// </param>
	/// <response code="200"></response>
	/// <response code="400">Invalid request body</response>
	[HttpPost, Route(Routes.auth_loginWithProvider)]
	public async Task<ActionResult<ApiResponse>> LogInWithProvider([FromBody] ProviderLoginRequestBody requestBody) {
		DkReflections.TrimJsonAnnotatedProperties(requestBody);
	
		return await authService.LogInWithProvider(requestBody, RequestHelper.GetClientIp(this.Request), RequestHelper.GetUserAgent(this.Request));
	}

	/// <summary>
	/// Logout an user from current place or everywhere.
	/// </summary>
	/// <param name="requestBody">
	/// - logout_everywhere: Default is false. If provide true, this will logout the user from all places.
	/// </param>
	/// <response code="200"></response>
	[Authorize]
	[HttpPost, Route(Routes.auth_logout)]
	public async Task<ActionResult<ApiResponse>> LogOut([FromBody] LogoutRequestBody requestBody) {
		var userId = this.ClaimUserId();
		if (userId == null) {
			return new ApiUnauthorizedResponse();
		}

		DkReflections.TrimJsonAnnotatedProperties(requestBody);

		return await authService.LogOut((Guid)userId, requestBody, RequestHelper.GetClientIp(this.Request), RequestHelper.GetClientIp(this.Request));
	}
}
