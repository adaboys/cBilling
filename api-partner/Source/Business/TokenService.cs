namespace App {
	using System.IdentityModel.Tokens.Jwt;
	using System.Security.Claims;
	using System.Security.Cryptography;
	using System.Text;
	using Microsoft.Extensions.Options;
	using Microsoft.IdentityModel.Tokens;

	public class TokenService {
		private readonly ILogger<AuthService> logger;
		private readonly AppSetting appSetting;
		private readonly AppDbContext dbContext;

		public TokenService(
			ILogger<AuthService> logger,
			IOptions<AppSetting> appSettingOpt,
			AppDbContext dbContext
		) {
			this.logger = logger;
			this.appSetting = appSettingOpt.Value;
			this.dbContext = dbContext;
		}

		internal string GenerateAccessToken(IEnumerable<Claim> claims) {
			var jwtSection = this.appSetting.jwt;
			var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSection.key));

			var securityToken = new JwtSecurityToken(
				// Server
				issuer: jwtSection.issuer,
				// Client
				audience: jwtSection.audience,
				// We also attach custom claims
				claims: claims,
				// Null expiration means never expired??
				expires: DateTime.Now.AddSeconds(jwtSection.expiresInSeconds),
				// Use `HmacSha256` instead of `HmacSha256Signature` since another services need decode the token
				signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
			);

			return new JwtSecurityTokenHandler().WriteToken(securityToken);
		}

		internal string GenerateRefreshToken(int tokenLength = 36) {
			var guid = Guid.NewGuid().ToStringDk();

			if (tokenLength > guid.Length) {
				var randomNumber = new byte[tokenLength - guid.Length];
				using var rng = RandomNumberGenerator.Create();
				rng.GetBytes(randomNumber);

				return guid + Convert.ToBase64String(randomNumber);
			}

			return guid;
		}

		internal ClaimsPrincipal? GetClaimsPrincipalFromExpiredAccessToken(string accessToken) {
			try {
				var tokenValidationParameters = new TokenValidationParameters {
					// You might want to validate the audience and issuer depending on your use case
					ValidateAudience = false,
					ValidateIssuer = false,
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.appSetting.jwt.key)),
					// Here we are saying that we don't care about the token's expiration date
					ValidateLifetime = false
				};
				var tokenHandler = new JwtSecurityTokenHandler();
				var claimsPrincipal = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out var securityToken);
				var jwtSecurityToken = securityToken as JwtSecurityToken;

				if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase)) {
					throw new SecurityTokenException("Invalid token");
				}

				return claimsPrincipal;
			}
			catch {
				return null;
			}
		}
	}
}
