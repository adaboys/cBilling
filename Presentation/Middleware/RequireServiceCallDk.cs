namespace App;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Tool.Compet.Core;

/// Annotate with [RequireServiceCallDk] to prevent access from unauthorized place.
/// Note: to call service api, each service must provide api_key in the request header.
public class RequireServiceCallDk : AuthorizeAttribute, IAuthorizationFilter {
	public void OnAuthorization(AuthorizationFilterContext context) {
		try {
			// We perform try/catch here since this is dangerous get value, maybe cause exception.
			var authArr = context.HttpContext.Request.Headers.Authorization.FirstOrDefault()?.Split(" ");
			if (authArr is null || authArr.Length != 2 || authArr[0] != "api_key") {
				context.Result = new UnauthorizedResult();
				return;
			}

			var apiKey = authArr[1];
			var config = context.HttpContext.RequestServices.GetService<IConfiguration>()!;
			var appSetting = config.GetSection(AppSetting.SECTION_APP).Get<AppSetting>();

			// From our credential list, find a credential which has apiKey matches with incoming apiKey.
			var ok = false;
			foreach (var predefinedApiKey in appSetting.serviceApiKeys) {
				if (predefinedApiKey.EqualsDk(apiKey)) {
					ok = true;
					break;
				}
			}
			if (!ok) {
				context.Result = new ForbidResult();
				return;
			}
		}
		catch {
			context.Result = new UnauthorizedResult();
			return;
		}

		// For extra actions
		// We can check permissions of found service-authorization...

		// For db access
		// var dbContext = context.HttpContext.RequestServices.GetRequiredService<AppDbContext>();
		// var user = dbContext.users.FirstOrDefault(m => m.id == Guid.Parse(userId));

		// For JWT decoding
		// var claims = context.HttpContext.User.Claims;
		// var incomingServiceName = claims.FirstOrDefault(claim => claim.Type == R.jwts.jwt_service_name)?.Value;
		// var incomingServiceCredential = claims.FirstOrDefault(claim => claim.Type == R.jwts.jwt_service_credential)?.Value;
	}
}
