namespace App;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tool.Compet.Core;

[ApiController, Route(Routes.api_prefix)]
public class PartnerController : BaseController {
	private readonly PartnerService partnerService;

	public PartnerController(PartnerService partnerService) {
		this.partnerService = partnerService;
	}

	/// <summary>
	/// Register partner temp
	/// </summary>
	/// <param name="requestBody">
	/// - "email": "email@gmail.com",
	/// - "phone": 0775667788,
	/// </param>
	/// <response code="200"></response>
	[HttpPost, Route(Routes.register_temp)]
	public async Task<ActionResult<ApiResponse>> AttemptRegister([FromBody] RegisterPartnerTempRequestBody requestBody) {
		DkReflections.TrimJsonAnnotatedProperties(requestBody);

		return await partnerService.AttemptRegister(requestBody);
	}

	/// <summary>
	/// Register partner temp
	/// </summary>
	/// <param name="requestBody">
	/// - otp:1234
	///	- email
	///	- phone
	///	- name
	///	- password
	/// </param>
	/// <response code="200"></response>
	[HttpPost, Route(Routes.register_confirm)]
	public async Task<ActionResult<ApiResponse>> ConfirmRegister([FromBody] RegisterPartnerConfirmRequestBody requestBody) {
		DkReflections.TrimJsonAnnotatedProperties(requestBody);

		return await partnerService.ConfirmRegister(requestBody);
	}

	[HttpGet, Route(Routes.partner_profile)]
	public async Task<ActionResult<ApiResponse>> GetPartnerProfile([FromRoute] String code) {

		return await partnerService.GetPartnerProfile(code);
	}
}
