namespace App;

/// Api routes to Cardano node server.
public class CardanoNodeRoutes {
	/// Wallet
	public const string wallet_create = "wallet/create";

	/// NFT
	public const string nft_create_and_buy = "nft/create_and_buy";

	/// Asset
	public const string asset_send = "asset/send";
	public const string asset_transact = "asset/transact";

	/// Token
	public const string token_create = "token/create";
	public const string token_mint = "token/mint";
	public const string token_burn = "token/burn";

	/// Address
	public const string address_balance = "address/{address}/balance";
}
