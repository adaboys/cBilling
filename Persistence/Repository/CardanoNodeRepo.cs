namespace App;

using System;
using Microsoft.Extensions.Options;
using Tool.Compet.Http;

public class CardanoNodeRepo : BaseRepo {
	public CardanoNodeRepo(IOptions<AppSetting> appSettingOpt) : base(appSettingOpt) {
	}

	private DkHttpClient httpClient {
		get {
			// Don't make it static since each task holds different request setting.
			var httpClient = new DkHttpClient();

			// Attach api key to `Authorization` header entry.
			// TechNote: since restriction of CORS, we should not use custom header entry.
			// Lets use safe pre-defined header entries.
			httpClient.SetAuthorization("api_key", this.appSetting.cardanoNode.apiKey);

			return httpClient;
		}
	}

	/// Calculate url for api.
	private string CardanoNodeApiUrl(string relativePath) {
		if (relativePath.StartsWith('/')) {
			relativePath = relativePath.TrimStart('/');
		}
		return $"{this.appSetting.cardanoNode.apiBaseUrl}/{relativePath}";
	}

	public async Task<CardanoNode_CreateWalletResponse> CreateWallet(string client_id) {
		var url = CardanoNodeApiUrl(CardanoNodeRoutes.wallet_create);
		var body = new Dictionary<string, string>() {
			{ "client_id", client_id }
		};
		return await this.httpClient.Post<CardanoNode_CreateWalletResponse>(url, body);
	}

	public async Task<CardanoNode_CreateAndBuyNftResponse> CreateAndBuyNft(CardanoNode_MintNftRequestBody requestBody) {
		var url = CardanoNodeApiUrl(CardanoNodeRoutes.nft_create_and_buy);
		return await this.httpClient.Post<CardanoNode_CreateAndBuyNftResponse>(url, requestBody);
	}

	public async Task<CardanoNode_CreateTokenResponse> CreateToken(CardanoNode_CreateTokenRequestBody requestBody) {
		var url = CardanoNodeApiUrl(CardanoNodeRoutes.token_create);
		return await this.httpClient.Post<CardanoNode_CreateTokenResponse>(url, requestBody);
	}

	public async Task<CardanoNode_SendAssetResponse> SendAsset(CardanoNode_SendAssetRequestBody requestBody) {
		var url = CardanoNodeApiUrl(CardanoNodeRoutes.asset_send);
		return await this.httpClient.Post<CardanoNode_SendAssetResponse>(url, requestBody);
	}

	public async Task<CardanoNode_TransactAssetResponse> TransactAsset(CardanoNode_TransactAssetRequestBody requestBody) {
		var url = CardanoNodeApiUrl(CardanoNodeRoutes.asset_transact);
		return await this.httpClient.Post<CardanoNode_TransactAssetResponse>(url, requestBody);
	}

	public async Task<CardanoNode_GetBalanceResponse> GetBalance(string wallet_address) {
		var url = CardanoNodeApiUrl(CardanoNodeRoutes.address_balance.Replace("{address}", wallet_address));
		return await this.httpClient.Get<CardanoNode_GetBalanceResponse>(url);
	}
}
