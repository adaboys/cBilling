namespace App;

public class DbConst {
	public const string table_mst_app = "mst_app";
	public const string table_mst_aircraft_head = "mst_aircraft_head";
	public const string table_mst_aircraft_body = "mst_aircraft_body";
	public const string table_mst_aircraft_wing = "mst_aircraft_wing";
	public const string table_mst_aircraft_tail = "mst_aircraft_tail";
	public const string table_mst_exchange_rate = "mst_exchange_rate";
	public const string table_mst_coin = "mst_coin";

	/// [User]
	public const string table_user = "user";
	public const string table_user_registry = "user_registry";
	public const string table_user_auth_token = "user_auth_token";
	public const string table_user_wallet = "user_wallet";


	/// [Partner] This is merchant/shop.
	public const string table_partner = "partner";
	public const string table_partner_wallet = "partner_wallet";
	public const string table_project = "project";
	public const string table_partner_auth_token = "partner_auth_token";

	public const string table_order = "order";

	public const string table_tx = "tx";
	public const string table_tx_detail = "tx_detail";
	public const string table_nft_owner_sync = "nft_owner_sync";
}
