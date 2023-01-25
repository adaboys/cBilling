namespace App;

using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

public static class JwtAuthenticationConfig {
	public static void ConfigureJwtAuthenticationDk(this IServiceCollection service, AppSetting appSetting) {
		service
			// Use Bearer token for authentication challenge by `Authorize` attribute.
			.AddAuthentication(options => {
				options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(options => {
				options.RequireHttpsMetadata = true;
				options.SaveToken = true;
				options.TokenValidationParameters = new TokenValidationParameters {
					// Always check lifetime of the token (this is used at `Authorize` attribute).
					// Note: Set clockSkew to zero (default value is 5 minutes) so we can start expiration-timeout for a token.
					ClockSkew = TimeSpan.Zero,
					ValidateLifetime = true,

					ValidateIssuer = true,
					ValidIssuer = appSetting.jwt.issuer,

					ValidateAudience = true,
					ValidAudience = appSetting.jwt.audience,

					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSetting.jwt.key))
				};
			});
	}
}
