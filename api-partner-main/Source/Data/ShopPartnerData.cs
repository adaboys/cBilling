namespace App;

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


public class CreateOrderRequestBody {
	/// Mapping with partner_project.project_code
	[Required]
	[JsonPropertyName(name: "project_id")]
	public string project_id { get; set; }

	/// Mapping with order.partner_tx_id
	[Required]
	[JsonPropertyName(name: "transaction_id")]
	public string transaction_id { get; set; }

	[Required]
	[JsonPropertyName(name: "price_in_ada")]
	public decimal price_in_ada { get; set; }

	[Required]
	[JsonPropertyName(name: "items")]
	public object items { get; set; }

	[Required]
	[JsonPropertyName(name: "description")]
	public string description { get; set; }

	[Required]
	[JsonPropertyName(name: "hmac")]
	public string hmac { get; set; }
}

public class PayOrderRequestBody {
	[Required]
	[JsonPropertyName(name: "order_code")]
	public string order_code { get; set; }
}

public class RegisterPartnerTempRequestBody {
	[Required]
	[JsonPropertyName(name: "email")]
	public string email { get; set; }

	[Required]
	[JsonPropertyName(name: "phone")]
	public string phone { get; set; }
}

public class RegisterPartnerConfirmRequestBody {
	[Required]
	[JsonPropertyName(name: "otp")]
	public string otp { get; set; }

	[Required]
	[JsonPropertyName(name: "email")]
	public string email { get; set; }

	[Required]
	[JsonPropertyName(name: "phone")]
	public string phone { get; set; }

	[Required]
	[JsonPropertyName(name: "password")]
	public string password { get; set; }

	[Required]
	[JsonPropertyName(name: "name")]
	public string name { get; set; }
}

public class PayOrderResponse : ApiSuccessResponse {
	[JsonPropertyName(name: "data")]
	public Data data { get; set; }

	public class Data {
		[JsonPropertyName(name: "order_code")]
		public string order_code { get; set; }

		[JsonPropertyName(name: "message")]
		public string message { get; set; }

		[JsonPropertyName(name: "callback_url")]
		public string callback_url { get; set; }
	}
}

public class OrderInfoResponse : ApiSuccessResponse {
	[JsonPropertyName(name: "data")]
	public Data data { get; set; }

	public class Data {
		[JsonPropertyName(name: "partner_order_id")]
		public string partner_order_id { get; set; }

		[JsonPropertyName(name: "partner_name")]
		public string partner_name { get; set; }

		[JsonPropertyName(name: "description")]
		public string description { get; set; }

		[JsonPropertyName(name: "amount")]
		public decimal amount { get; set; }

		[JsonPropertyName(name: "timeout")]
		public long timeout { get; set; }
	}
}

public class CreateOrderResponse : ApiSuccessResponse {
	[JsonPropertyName(name: "data")]
	public Data data { get; set; }

	public class Data {
		[JsonPropertyName(name: "target_url")]
		public string target_url { get; set; }

	}
}

public class PartnerResponse : ApiSuccessResponse {
	[JsonPropertyName(name: "data")]
	public Data data { get; set; }


	public class Data {
		[JsonPropertyName(name: "code")]
		public string code { get; set; }

		[JsonPropertyName(name: "phone")]
		public string phone { get; set; }

		[JsonPropertyName(name: "email")]
		public string email { get; set; }

		[JsonPropertyName(name: "name")]
		public string name { get; set; }

	}
}


public class ProjectReqponse: ApiSuccessResponse {
	[JsonPropertyName(name: "data")]
	public Data data { get; set; }


	public class Data {
		[JsonPropertyName(name: "id")]
		public string id { get; set; }

		[JsonPropertyName(name: "project_code")]
		public Data project_code { get; set; }

		[JsonPropertyName(name: "project_name")]
		public Data project_name { get; set;}

		[JsonPropertyName(name: "secret_key")]
		public Data secret_key { get; set;}

		[JsonPropertyName(name: "status")]
		public Data status { get; set;}
	}
}
