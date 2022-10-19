namespace App;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Tool.Compet.Json;
using System.Web;
using Tool.Compet.Core;

/// Raw query with interpolated: https://docs.microsoft.com/en-us/ef/core/querying/raw-sql
public class OrderService : BaseService {

	private readonly ILogger<OrderService> logger;
	private readonly CardanoNodeRepo cardanoNodeRepo;
	public OrderService(
		AppDbContext dbContext,
		CardanoNodeRepo cardanoNodeRepo,
		ILogger<OrderService> logger,
		IOptions<AppSetting> appSettingOpt
	) : base(dbContext, appSettingOpt) {
		this.logger = logger;
		this.cardanoNodeRepo = cardanoNodeRepo;
	}

	public async Task<ApiResponse> CreateNewOrder(CreateOrderRequestBody requestBody) {
		using var transaction = await this.dbContext.Database.BeginTransactionAsync();

		try {
			//todo logic check hashmac security here
			//todo check api auth with secret key

			var partner_project = await this.dbContext.partnerProjects.FirstOrDefaultAsync(m =>
				m.project_code == requestBody.project_id &&
				m.status == PartnerProjectModelConst.STATUS_ACTIVE &&
				m.deleted_at == null
			);
			if (partner_project == null) {
				return new ApiBadRequestResponse("Invalid");
			}
			var order = await this.dbContext.orders.FirstOrDefaultAsync(m =>
				m.partner_project_id == partner_project.id && m.partner_tx_id == requestBody.transaction_id
			);
			if (order != null) {
				return new ApiBadRequestResponse("Invalid");
			}

			var newOrder = new OrderModel {
				partner_project_id = partner_project.id,
				partner_tx_id = requestBody.transaction_id,
				code = Helper.GenerateAlphabetCode(),
				price_in_ada = requestBody.price_in_ada,
				description = requestBody.description,
				items = DkJsons.Obj2Json(requestBody.items),
				hmac = requestBody.hmac,
				status = OrderModelConst.STATUS_NORMAL,
				created_at = DateTime.Now,
				expired_at = DateTime.Now.AddMinutes(10),
			};

			this.dbContext.orders.Attach(newOrder);
			await this.dbContext.SaveChangesAsync();
			await transaction.CommitAsync();

			return new CreateOrderResponse {
				data = new() {
					target_url = "getway.domain?order_code=" + newOrder.code
				}
			};
		}
		catch (Exception e) {
			await transaction.RollbackAsync();
			return new ApiInternalServerErrorResponse("Create new order fail");
		}
	}

	// get order info
	public async Task<ApiResponse> GetOrderDetail(string orderCode) {
		var query =
			from _order in this.dbContext.orders
			join _partner_project in this.dbContext.partnerProjects on _order.partner_project_id equals _partner_project.id
			join _partner in this.dbContext.partners on _partner_project.partner_id equals _partner.id
			where _order.code == orderCode && _order.status == OrderModelConst.STATUS_NORMAL
			select new {
				_partner_order_id = _order.partner_tx_id,
				_partner_name = _partner.name,
				_order_amount = _order.price_in_ada,
				_order_description = _order.description,
				_order_expired_at = _order.expired_at
			}
		;
		var result = await query.FirstOrDefaultAsync();
		if (result == null) {
			return new ApiNotFoundResponse("Order not found");
		}

		return new OrderInfoResponse {
			data = new() {
				partner_order_id = result._partner_order_id,
				partner_name = result._partner_name,
				amount = result._order_amount,
				description = result._order_description,
				timeout = Math.Max(0, (long)result._order_expired_at.Subtract(DateTime.Now).TotalSeconds)
			}
		};
	}

	public async Task<ApiResponse> PayOrder(Guid userId, PayOrderRequestBody requestBody) {
		var orderCode = requestBody.order_code;

		try {
			var query =
			from _order in this.dbContext.orders
			join _partner_project in this.dbContext.partnerProjects on _order.partner_project_id equals _partner_project.id
			join _partner in this.dbContext.partners on _partner_project.partner_id equals _partner.id
			where _order.code == orderCode && _order.status == OrderModelConst.STATUS_NORMAL
			select new {
				_partner_project_id = _partner_project.id,
				_partner_order_id = _order.partner_tx_id,
				_partner_name = _partner.name,
				_order_amount = _order.price_in_ada,
				_order_description = _order.description,
				_partner_wallet = _partner_project.wallet_address
			};

			var result = await query.FirstOrDefaultAsync();

			var userwallet = await this.dbContext.userWallets.FirstOrDefaultAsync(m =>
				m.user_id == userId
			);

			if (result == null || userwallet == null) {
				return new ApiNotFoundResponse("Order Invalid");
			}

			var order = await this.dbContext.orders.FirstOrDefaultAsync(m =>
				m.code == orderCode && m.partner_project_id == result._partner_project_id
			);

			if (order == null || order.status != OrderModelConst.STATUS_NORMAL) {
				return new ApiNotFoundResponse("Order Invalid");
			}

			// await this.dbContext.orders
			// 	.FromSqlRaw($"SELECT * FROM {DbConst.table_order} WITH (UPDLOCK) WHERE id = {{0}}", order.id)
			// 	.FirstAsync();


			// convert ada to lovelace
			var lovelaceToken = (int)(order.price_in_ada * 1.0m * AppConst.ADA_COIN2TOKEN);

			//call api convert lovelace from wallet buyer to wallet shop
			var out_reqBody = new CardanoNode_SendAssetRequestBody().AlsoDk(body => {
				body.sender_address = userwallet.wallet_address;
				body.receiver_address = result._partner_wallet;
				body.sender_is_fee_payer = true;

				// Note: need attach 1.4 ADA when send non-ADA asset
				var assets = new List<CardanoNode_AssetAndQuantity> {
					new() {
						asset = MstCoinModelConst.TOKEN_ID_ADA,
						quantity = lovelaceToken,
					}
				};
				body.assets = assets.ToArray();
			});

			var sendTokenResponse = await this.cardanoNodeRepo.SendAsset(out_reqBody);
			if (sendTokenResponse.failed) {
				order.status = OrderModelConst.STATUS_PAYENT_FAIL;
				// throw new SystemException(this.appSetting.debug ? sendTokenResponse.message : "Could not pay");
			}
			else {
				order.status = OrderModelConst.STATUS_PAYENT_SUCCESS;
			}

			order.updated_at = DateTime.Now;

			return new PayOrderResponse {
				data = new() {
					order_code = orderCode,
					message = "payment successful",
					callback_url = "https://www.dev.cbilling.io/callback?order_code=" + orderCode + HttpUtility.UrlEncode("&message=payment successful"),
				}
			};
		}
		catch (Exception e) {
			return new PayOrderResponse {
				data = new() {
					order_code = orderCode,
					message = "payment fail",
					callback_url = "https://www.dev.cbilling.io/callback?order_code=" + orderCode + HttpUtility.UrlEncode("&message=payment fail"),
				}
			};
		}
		finally {
			// Save changes
			await this.dbContext.SaveChangesAsync();
		}
	}


	public async Task<ApiResponse> CancelOrder(String order_code) {
		var order = await this.dbContext.orders.FirstOrDefaultAsync(m =>
			m.code == order_code &&
			m.status == OrderModelConst.STATUS_NORMAL
		);

		if (order == null) {
			return new ApiNotFoundResponse();
		}

		// callback to shop
		// toto


		// Mark this order was cancelled
		order.status = OrderModelConst.STATUS_PAYENT_CANCEL;
		order.expired_at = DateTime.Now;

		await this.dbContext.SaveChangesAsync();

		return new CancelOrderResponse {
			data = new() {
				status = order.status,
				hmac = order.hmac,
			}
		};
	}
}
