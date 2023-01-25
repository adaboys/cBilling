namespace App;

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

[ApiController, Route(Routes.api_prefix)]
public class UnhandledExceptionHandlerController : ControllerBase {
	private readonly AppSetting appSetting;
	private readonly ILogger logger;

	public UnhandledExceptionHandlerController(
		IOptions<AppSetting> appSettingOpt,
		ILogger<UnhandledExceptionHandlerController> logger
	) {
		this.appSetting = appSettingOpt.Value;
		this.logger = logger;
	}

	// Ignore api to work with swagger since we don't specify which type of HttpXXX method
	[ApiExplorerSettings(IgnoreApi = true)]
	[Route("error")]
	public ActionResult<ApiResponse> HandleException() {
		return new ApiResponse(HttpContext.Response.StatusCode, "UError");
	}

	// Ignore api to work with swagger since we don't specify which type of HttpXXX method
	[ApiExplorerSettings(IgnoreApi = true)]
	[Route("error-development")]
	public ActionResult<ApiResponse> HandleExceptionForDevelopment() {
		var handler = HttpContext.Features.Get<IExceptionHandlerFeature>()!;
		return new ApiResponse(HttpContext.Response.StatusCode, $"UError, error: {handler.Error.Message}");
	}
}
