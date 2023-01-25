namespace App;

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class AttemptRegisterUserRequestBody {
	[Required]
	[EmailAddress]
	[JsonPropertyName(name: "email")]
	public string email { get; set; }
}

public class UpdateUserIdentityRequestBody {
	[Required]
	[JsonPropertyName(name: "full_name")]
	public string full_name { get; set; }

	[Required]
	[JsonPropertyName(name: "telno")]
	public string telno { get; set; }
}

public class UpdateUserIdentityResponse : ApiSuccessResponse {
	[JsonPropertyName(name: "data")]
	public Data data { get; set; }

	public class Data {
		[JsonPropertyName(name: "full_name")]
		public string full_name { get; set; }

		[JsonPropertyName(name: "telno")]
		public string telno { get; set; }
	}
}

public class AttemptRegisterUserResponse : ApiSuccessResponse {
	[JsonPropertyName(name: "data")]
	public Data data { get; set; }

	public class Data {
		[JsonPropertyName(name: "token")]
		public string token { get; set; }
	}
}

public class ConfirmRegisterUserRequestBody {
	[Required]
	[JsonPropertyName(name: "otp_code")]
	public string otp_code { get; set; }

	[Required]
	[EmailAddress]
	[JsonPropertyName(name: "email")]
	public string email { get; set; }

	[Required]
	[JsonPropertyName(name: "password")]
	public string password { get; set; }
}

public class UpdateUserPasswordRequestBody {
	[JsonPropertyName(name: "old_pass")]
	public string? old_pass { get; set; }

	[Required]
	[JsonPropertyName(name: "new_pass")]
	public string new_pass { get; set; }

	[Required]
	[JsonPropertyName(name: "confirm_new_pass")]
	public string confirm_new_pass { get; set; }

	[Required]
	[JsonPropertyName(name: "is_logout")]
	public bool is_logout { get; set; } = true;
}

public class GetUserNftsResponse : ApiSuccessResponse {
	[JsonPropertyName(name: "data")]
	public Data data { get; set; }

	public class Data {
		[JsonPropertyName(name: "nfts")]
		public List<DisplayNft> nfts { get; set; }
	}

	public class DisplayNft {
		[JsonPropertyName(name: "name")]
		public string name { get; set; }

		[JsonPropertyName(name: "asset_id")]
		public string? asset_id { get; set; }

		[JsonPropertyName(name: "image_path")]
		public string image_path { get; set; }

		// [JsonPropertyName(name: "price_in_ada")]
		// public decimal price_in_ada { get; set; }

		// [JsonPropertyName(name: "sold_at")]
		// public DateTime? sold_at { get; set; }

		// [JsonPropertyName(name: "sold_price_in_isky")]
		// public decimal sold_price_in_isky { get; set; }

		// [JsonPropertyName(name: "sold_price_in_dollar")]
		// public decimal sold_price_in_dollar { get; set; }
	}
}

public class GetUserProfileResponse : ApiSuccessResponse {
	[JsonPropertyName(name: "data")]
	public Data data { get; set; }

	public class Data {
		[JsonPropertyName(name: "email")]
		public string email { get; set; }

		[JsonPropertyName(name: "wallet_address")]
		public string wallet_address { get; set; }

		[JsonPropertyName(name: "player_name")]
		public string player_name { get; set; }

		[JsonPropertyName(name: "provider")]
		public byte provider { get; set; }
	}
}
