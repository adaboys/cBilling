namespace App;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tool.Compet.Core;

[ApiController, Route(Routes.api_prefix)]
public class PartnerProjectController : BaseController {
	private readonly PartnerProjectService partnerProjectService;

	public PartnerProjectController(PartnerProjectService partnerProjectService) {
		this.partnerProjectService = partnerProjectService;
	}

	/// <summary>
	/// Register partner temp
	/// </summary>
	/// <param name="requestBody">
	/// - partner_id: for test because api login partner shop not already
	/// - project_name
	/// - wallet_address
	/// - url_callback
	/// </param>
	/// <response code="200"></response>
	[HttpPost, Route(Routes.project_create)]
	public async Task<ActionResult<ApiResponse>> CreateProject([FromBody] CreateProjectRequestBody requestBody) {
		DkReflections.TrimJsonAnnotatedProperties(requestBody);

		return await partnerProjectService.CreateProject(requestBody);
	}

	/// <summary>
	/// Register partner temp
	/// </summary>
	/// <param name="requestBody">
	/// - partner_id: for test because api login partner shop not already
	/// </param>
	/// <response code="200"></response>
	[HttpPost, Route(Routes.project_list)]
	public async Task<ActionResult<ApiResponse>> ListProject([FromBody] GetListProjectRequestBody requestBody) {

		return await partnerProjectService.ListProject(requestBody);
	}


	/// <summary>
	/// Remove | Deactive project
	/// </summary>
	/// <param name="requestBody">
	/// - id
	/// </param>
	/// <response code="200"></response>
	/// <response code="404"></response>
	[HttpPost, Route(Routes.project_remove)]
	public async Task<ActionResult<ApiResponse>> RemoveProject([FromRoute] long id) {

		return await partnerProjectService.removeProject(id);
	}

}
