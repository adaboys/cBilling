namespace App;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tool.Compet.Core;

[ApiController, Route(Routes.api_prefix)]
public class OrderController : BaseController {
	private readonly OrderService orderService;

	public OrderController(OrderService orderaService) {
		this.orderService = orderaService;
	}

	/// <summary>
	/// Create new order from shop partner
	/// </summary>
	/// <param name="requestBody">
	/// - "project_id": "testproj2",
	///	- "transaction_id": "transactions_0011",
	/// - "price_in_ada": 1,
	/// - "items": {"item1": 10,"item2": 20},
	/// - "description": "test create order",
	/// - "hmac": "secret_key"
	/// </param>
	/// <response code="200"></response>
	[HttpPost, Route(Routes.order_create)]
	public async Task<ActionResult<ApiResponse>> CreateOrder([FromBody] CreateOrderRequestBody requestBody) {
		DkReflections.TrimJsonAnnotatedProperties(requestBody);

		return await orderService.CreateNewOrder(requestBody);
	}

	/// <summary>
	/// Get order info dispaly to form payment
	/// </summary>
	/// <param name="requestBody">
	///	- order_code: from url target after shop create order
	/// </param>
	/// <response code="200">
	/// </response>
	[HttpGet, Route(Routes.order_info)]
	public async Task<ActionResult<ApiResponse>> OrderDetail([FromRoute] string order_code) {
		return await this.orderService.GetOrderDetail(order_code);
	}

	/// <summary>
	/// Submit payment
	/// </summary>
	/// <param name="requestBody">
	///	- order_code: from url target after shop create order
	/// </param>
	/// <response code="200">
	/// </response>
	[Authorize]
	[HttpPost, Route(Routes.order_pay)]
	public async Task<ActionResult<ApiResponse>> PayOrder([FromBody] PayOrderRequestBody requestBody) {
		var userId = this.ClaimUserId();
		if (userId == null) {
			return new ApiUnauthorizedResponse();
		}

		DkReflections.TrimJsonAnnotatedProperties(requestBody);

		return await this.orderService.PayOrder((Guid)userId, requestBody);
	}

	/// <summary>
	/// Cancel order
	/// </summary>
	/// <param name="requestBody">
	/// </param>
	/// <response code="200">
	/// </response>
	/// <response code="404">
	/// </response>
	[HttpPost, Route(Routes.order_cancel)]
	public async Task<ActionResult<ApiResponse>> CancelOrder([FromRoute] String order_code) {

		return await this.orderService.CancelOrder(order_code);
	}
}
