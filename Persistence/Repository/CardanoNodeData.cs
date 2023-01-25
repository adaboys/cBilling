namespace App;

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class CardanoNode_CreateWalletResponse : ApiSuccessResponse {
	[JsonPropertyName(name: "data")]
	public Data data { get; set; }

	public class Data {
		[JsonPropertyName(name: "wallet_address")]
		public string walletAddress { get; set; }
	}
}

public class CardanoNode_MintNftRequestBody {
	[JsonPropertyName(name: "seller_address")]
	public string seller_address { get; set; }

	[JsonPropertyName(name: "payer_address")]
	public string payer_address { get; set; }

	[JsonPropertyName(name: "receiver_address")]
	public string receiver_address { get; set; }

	/// Token id at Cardano system.
	/// Normally it is lovelace.
	[Required]
	[JsonPropertyName(name: "pay_with_token")]
	public string pay_with_token { get; set; }

	/// How much to pay to seller
	[Required]
	[JsonPropertyName(name: "nft_price")]
	public long nft_price { get; set; }

	/// If this is provided, then we will subtract fee from the nft_price.
	/// For eg,. if nft_price is 16 ADA, and fee is 0.2 ADA, then payer just need pay to seller 15.8 ADA.
	[Required]
	[JsonPropertyName(name: "discount_fee_from_price")]
	public bool discount_fee_from_price { get; set; }

	[Required]
	[JsonPropertyName(name: "nft_quantity")]
	public long nft_quantity { get; set; }

	[Required]
	[JsonPropertyName(name: "nft_name")]
	public string nft_name { get; set; }

	[Required]
	[JsonPropertyName(name: "nft_display_name")]
	public string nft_display_name { get; set; }

	[Required]
	[JsonPropertyName(name: "nft_description")]
	public string nft_description { get; set; }

	[Required]
	[JsonPropertyName(name: "nft_ipfs_path")]
	public string nft_ipfs_path { get; set; }

	[Required]
	[JsonPropertyName(name: "nft_attribute")]
	public Dictionary<string, object> nft_attribute { get; set; }
}

public class CardanoNode_CreateAndBuyNftResponse : ApiSuccessResponse {
	[JsonPropertyName(name: "data")]
	public Data data { get; set; }

	public class Data {
		[JsonPropertyName(name: "nft_id")]
		public string nft_id { get; set; }

		[JsonPropertyName(name: "fee")]
		public int fee { get; set; }
	}
}

public class CardanoNode_SendAssetRequestBody {
	[Required]
	[JsonPropertyName(name: "sender_address")]
	public string sender_address { get; set; }

	[Required]
	[JsonPropertyName(name: "receiver_address")]
	public string receiver_address { get; set; }

	[Required]
	[JsonPropertyName(name: "sender_is_fee_payer")]
	public bool sender_is_fee_payer { get; set; }

	[Required]
	[JsonPropertyName(name: "assets")]
	public CardanoNode_AssetAndQuantity[] assets { get; set; }
}

public class CardanoNode_TransactAssetRequestBody {
	[Required]
	[JsonPropertyName(name: "fee_payer_address")]
	public string fee_payer_address { get; set; }

	[Required]
	[JsonPropertyName(name: "transactions")]
	public TransactionItem[] transactions { get; set; }

	public class TransactionItem {
		[Required]
		[JsonPropertyName(name: "sender_address")]
		public string sender_address { get; set; }

		[Required]
		[JsonPropertyName(name: "receiver_address")]
		public string receiver_address { get; set; }

		[Required]
		[JsonPropertyName(name: "assets")]
		public CardanoNode_AssetAndQuantity[] assets { get; set; }
	}
}

public class CardanoNode_AssetAndQuantity {
	[Required]
	[JsonPropertyName(name: "asset")]
	public string asset { get; set; }

	[Required]
	[JsonPropertyName(name: "quantity")]
	public long quantity { get; set; }
}

public class CardanoNode_CreateTokenRequestBody {
	[Required]
	[JsonPropertyName(name: "payer_address")]
	public string payer_address { get; set; }

	[Required]
	[JsonPropertyName(name: "receiver_address")]
	public string receiver_address { get; set; }

	[Required]
	[JsonPropertyName(name: "token_name")]
	public string token_name { get; set; }

	[Required]
	[JsonPropertyName(name: "token_quantity")]
	public long token_quantity { get; set; }
}

public class CardanoNode_CreateTokenResponse : ApiSuccessResponse {
	[JsonPropertyName(name: "data")]
	public Data data { get; set; }

	public class Data {
		[JsonPropertyName(name: "token_name")]
		public string token_name { get; set; }

		[JsonPropertyName(name: "token_id")]
		public string token_id { get; set; }
	}
}

public class CardanoNode_SendAssetResponse : ApiSuccessResponse {
	[JsonPropertyName(name: "data")]
	public Data data { get; set; }

	public class Data {
		[JsonPropertyName(name: "fee")]
		public int fee { get; set; }
	}
}

public class CardanoNode_TransactAssetResponse : ApiSuccessResponse {
	[JsonPropertyName(name: "data")]
	public Data data { get; set; }

	public class Data {
		[JsonPropertyName(name: "fee")]
		public int fee { get; set; }
	}
}

public class CardanoNode_GetBalanceResponse : ApiSuccessResponse {
	[JsonPropertyName(name: "data")]
	public Data data { get; set; }

	public class Data {
		[JsonPropertyName(name: "balance")]
		public CardanoNode_AssetAndQuantity[] balance { get; set; }
	}
}
