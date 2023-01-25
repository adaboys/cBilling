namespace App;

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


public class CreateProjectRequestBody {
	[Required]
	[JsonPropertyName(name: "partner_id")]
	public long partner_id { get; set; }
	
	[Required]
	[JsonPropertyName(name: "project_name")]
	public string project_name { get; set; }

	[Required]
	[JsonPropertyName(name: "wallet_address")]
	public string wallet_address { get; set; }

	[Required]
	[JsonPropertyName(name: "url_callback")]
	public string url_callback { get; set; }
}


public class GetListProjectRequestBody {
	//because api login partner shop not already
	[Required]
	[JsonPropertyName(name: "partner_id")]
	public long partner_id { get; set; }

	[Required]
	[JsonPropertyName(name: "perPage")]
	public int perPage { get; set; }

	[Required]
	[JsonPropertyName(name: "limit")]
	public int limit { get; set; }
}



public class ProjectResponse : ApiSuccessResponse {
	[JsonPropertyName(name: "data")]
	public Data data { get; set; }

	public class Data {
		[JsonPropertyName(name: "project_code")]
		public string order_code { get; set; }

		[JsonPropertyName(name: "project_name")]
		public string message { get; set; }

		[JsonPropertyName(name: "url_callback")]
		public string url_callback { get; set; }

		
		[JsonPropertyName(name: "wallet_address")]
		public string wallet_address { get; set; }

		[JsonPropertyName(name: "status")]
		public byte status { get; set; }
	}
}

public class ProjectListResponse : ApiSuccessResponse {
	[JsonPropertyName(name: "data")]
	public Data data { get; set; }

	public class Data {
		[JsonPropertyName(name: "page_pos")]
		public int page_pos { get; set; }

		[JsonPropertyName(name: "page_count")]
		public int page_count { get; set; }

		[JsonPropertyName(name: "total_item_count")]
		public int total_item_count { get; set; }

		[JsonPropertyName(name: "projects")]
		public project[] projects { get; set; }
	}

	public class project {
		[JsonPropertyName(name: "id")]
		public long id { get; set; }
		
		[JsonPropertyName(name: "project_code")]
		public string project_code { get; set; }

		[JsonPropertyName(name: "project_name")]
		public string project_name { get; set; }

		[JsonPropertyName(name: "url_callback")]
		public string url_callback { get; set; }

		[JsonPropertyName(name: "wallet_address")]
		public string wallet_address { get; set; }

		[JsonPropertyName(name: "secret_key")]
		public string secret_key { get; set; }

		[JsonPropertyName(name: "status")]
		public byte status { get; set; }
	}
}

