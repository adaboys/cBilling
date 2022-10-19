namespace App;

using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

[ApiController, Route(Routes.api_prefix)]
public class TopController : ControllerBase {
	private readonly AppSetting appSetting;
	private readonly ILogger logger;
	private readonly TopService topService;
	private readonly AuthService authService;

	public TopController(IOptions<AppSetting> option, ILogger<TopController> logger, TopService topService, AuthService authService) {
		this.appSetting = option.Value;
		this.logger = logger;
		this.topService = topService;
		this.authService = authService;
	}

	/// Note:
	/// - It does NOT work if we use empty route as `Route("")`
	/// - It does not work if we add prefix slash to route as `Route("/api/auth/login")`
	[HttpGet, Route(Routes.top)]
	public async Task<ActionResult<object>> Top() {
		return await this.topService.Foo();
	}

	[RequireServiceCallDk]
	[HttpGet, Route("top/service")]
	public ActionResult<string> TopForService() {
		return $"[{this.appSetting.environment}-{this.appSetting.version.name}] Hellow, this is top page for service.";
	}

	[HttpGet, Route("top/genApiKey")]
	public ActionResult<string> GenApiKey([FromQuery] int size = 64) {
		var specialChars = "_.-@!";
		var digitChars = "0123456789";
		var romajiChars = "abcdefghijklmnopqrstuvwxyz";
		var allChars = romajiChars + romajiChars.ToUpper() + digitChars + specialChars;

		var apiKeyLength = size;
		var apiKey = Guid.NewGuid().ToStringDk();
		var remainCount = apiKeyLength - apiKey.Length;
		for (var index = 0; index < remainCount; ++index) {
			apiKey += allChars[RandomNumberGenerator.GetInt32(0, allChars.Length)];
		}

		var containSpecialCharCount = 0;
		for (var index = specialChars.Length - 1; index >= 0; --index) {
			if (apiKey.Contains(specialChars[index])) {
				containSpecialCharCount++;
			}
		}
		if (containSpecialCharCount < specialChars.Length) {
			Console.WriteLine($"Re-gen api key ({containSpecialCharCount}/{specialChars.Length})");
			return GenApiKey();
		}

		return apiKey;
	}
}
