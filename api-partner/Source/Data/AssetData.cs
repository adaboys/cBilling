namespace App;

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class SendNftRequestBody {
	[Required]
	[JsonPropertyName(name: "receiver_address")]
	public string receiver_address { get; set; }

	[Required]
	[JsonPropertyName(name: "nft_id")]
	public long nft_id { get; set; }

	[Required]
	[JsonPropertyName(name: "nft_quantity")]
	public long nft_quantity { get; set; }
}

public class SendTokenRequestBody {
	[Required]
	[JsonPropertyName(name: "receiver_address")]
	public string receiver_address { get; set; }

	/// For eg,. lovelace, isky,...
	[Required]
	[JsonPropertyName(name: "name")]
	public string name { get; set; }

	[Required]
	[JsonPropertyName(name: "quantity")]
	public long quantity { get; set; }
}

public class BuyAndMintNftRequestBody {
	[Required]
	[JsonPropertyName(name: "nft_id")]
	public long nft_id { get; set; }

	/// lovelace or isky
	// [Required]
	// [JsonPropertyName(name: "pay_with_token")]
	// public string pay_with_token { get; set; }

	/// If not present, internal wallet is used instead.
	// [JsonPropertyName(name: "receiver_address")]
	// public string? receiver_address { get; set; }
}

public class GetRecentListedAircraftsResponse : ApiSuccessResponse {
	[JsonPropertyName(name: "data")]
	public Data data { get; set; }

	public class Data {
		[JsonPropertyName(name: "page_pos")]
		public int page_pos { get; set; }

		[JsonPropertyName(name: "page_count")]
		public int page_count { get; set; }

		[JsonPropertyName(name: "aircrafts")]
		public Aircraft[] aircrafts { get; set; }

		public class Aircraft {
			[JsonPropertyName(name: "name")]
			public string name { get; set; }

			[JsonPropertyName(name: "id")]
			public long id { get; set; }

			[JsonPropertyName(name: "image_path")]
			public string image_path { get; set; }

			[JsonPropertyName(name: "price_in_ada")]
			public decimal price_in_ada { get; set; }

			[JsonPropertyName(name: "price_in_isky")]
			public decimal price_in_isky { get; set; }

			[JsonPropertyName(name: "est_price_in_dollar")]
			public decimal est_price_in_dollar { get; set; }

			[JsonPropertyName(name: "elapsed_seconds")]
			public long elapsed_seconds { get; set; }

			[JsonPropertyName(name: "rank_type")]
			public string rank_type { get; set; }

			[JsonPropertyName(name: "buyer_name")]
			public string buyer_name { get; set; }

			[JsonPropertyName(name: "buyer_address")]
			public string buyer_address { get; set; }

			[JsonPropertyName(name: "seller_name")]
			public string seller_name { get; set; }

			[JsonPropertyName(name: "seller_address")]
			public string seller_address { get; set; }

			[JsonPropertyName(name: "head")]
			public Asset_Head head { get; set; }

			[JsonPropertyName(name: "body")]
			public Asset_Body body { get; set; }

			[JsonPropertyName(name: "tail")]
			public Asset_Tail tail { get; set; }

			[JsonPropertyName(name: "wing")]
			public Asset_Wing wing { get; set; }
		}
	}
}

public class Asset_Head {
	[JsonPropertyName(name: "name")]
	public string name { get; set; }

	[JsonPropertyName(name: "shield")]
	public int shield { get; set; }

	[JsonPropertyName(name: "image_path")]
	public string image_path { get; set; }
}

public class Asset_Body {
	[JsonPropertyName(name: "name")]
	public string name { get; set; }

	[JsonPropertyName(name: "damage")]
	public int damage { get; set; }

	[JsonPropertyName(name: "image_path")]
	public string image_path { get; set; }
}

public class Asset_Wing {
	[JsonPropertyName(name: "name")]
	public string name { get; set; }

	[JsonPropertyName(name: "crit")]
	public int crit { get; set; }

	[JsonPropertyName(name: "image_path")]
	public string image_path { get; set; }
}

public class Asset_Tail {
	[JsonPropertyName(name: "name")]
	public string name { get; set; }

	[JsonPropertyName(name: "atk_speed")]
	public int atk_speed { get; set; }

	[JsonPropertyName(name: "image_path")]
	public string image_path { get; set; }
}

public class Aircraft_DetailResponse : ApiSuccessResponse {
	[JsonPropertyName(name: "data")]
	public Data data { get; set; }

	public class Data {
		[JsonPropertyName(name: "name")]
		public string name { get; set; }

		[JsonPropertyName(name: "id")]
		public long id { get; set; }

		[JsonPropertyName(name: "group_type")]
		public int group_type { get; set; }

		[JsonPropertyName(name: "rank_type")]
		public string rank_type { get; set; }

		[JsonPropertyName(name: "image_path")]
		public string image_path { get; set; }

		[JsonPropertyName(name: "owner_name")]
		public string? owner_name { get; set; }

		[JsonPropertyName(name: "owner_address")]
		public string? owner_address { get; set; }

		[JsonPropertyName(name: "price_in_ada")]
		public decimal price_in_ada { get; set; }

		[JsonPropertyName(name: "price_in_isky")]
		public decimal price_in_isky { get; set; }

		[JsonPropertyName(name: "est_price_in_dollar")]
		public decimal est_price_in_dollar { get; set; }

		[JsonPropertyName(name: "head")]
		public Asset_Head head { get; set; }

		[JsonPropertyName(name: "body")]
		public Asset_Body body { get; set; }

		[JsonPropertyName(name: "tail")]
		public Asset_Tail tail { get; set; }

		[JsonPropertyName(name: "wing")]
		public Asset_Wing wing { get; set; }
	}
}

public class Wingman_DetailResponse : ApiSuccessResponse {
	[JsonPropertyName(name: "data")]
	public Data data { get; set; }

	public class Data {
		[JsonPropertyName(name: "name")]
		public string name { get; set; }

		[JsonPropertyName(name: "id")]
		public long id { get; set; }

		[JsonPropertyName(name: "image_path")]
		public string image_path { get; set; }

		[JsonPropertyName(name: "owner_name")]
		public string? owner_name { get; set; }

		[JsonPropertyName(name: "owner_address")]
		public string? owner_address { get; set; }

		[JsonPropertyName(name: "price_in_ada")]
		public decimal price_in_ada { get; set; }

		[JsonPropertyName(name: "price_in_isky")]
		public decimal price_in_isky { get; set; }

		[JsonPropertyName(name: "est_price_in_dollar")]
		public decimal est_price_in_dollar { get; set; }

		[JsonPropertyName(name: "damage")]
		public int damage { get; set; }

		[JsonPropertyName(name: "atk_speed")]
		public int atk_speed { get; set; }

		[JsonPropertyName(name: "crit")]
		public int crit { get; set; }

		[JsonPropertyName(name: "shield")]
		public int shield { get; set; }
	}
}

public class Commander_DetailResponse : ApiSuccessResponse {
	[JsonPropertyName(name: "data")]
	public Data data { get; set; }

	public class Data {
		[JsonPropertyName(name: "name")]
		public string name { get; set; }

		[JsonPropertyName(name: "id")]
		public long id { get; set; }

		[JsonPropertyName(name: "image_path")]
		public string image_path { get; set; }

		[JsonPropertyName(name: "owner_name")]
		public string? owner_name { get; set; }

		[JsonPropertyName(name: "owner_address")]
		public string? owner_address { get; set; }

		[JsonPropertyName(name: "price_in_ada")]
		public decimal price_in_ada { get; set; }

		[JsonPropertyName(name: "price_in_isky")]
		public decimal price_in_isky { get; set; }

		[JsonPropertyName(name: "est_price_in_dollar")]
		public decimal est_price_in_dollar { get; set; }

		[JsonPropertyName(name: "damage")]
		public int damage { get; set; }

		[JsonPropertyName(name: "atk_speed")]
		public int atk_speed { get; set; }

		[JsonPropertyName(name: "crit")]
		public int crit { get; set; }

		[JsonPropertyName(name: "shield")]
		public int shield { get; set; }
	}
}
