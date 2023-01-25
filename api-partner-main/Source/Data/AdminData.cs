namespace App;

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class Admin_CreateAdminRequestBody {
	[Required]
	[JsonPropertyName(name: "email")]
	public string email { get; set; }

	[Required]
	[JsonPropertyName(name: "password")]
	public int password { get; set; }
}

public class Admin_CreateAdminResponse : ApiSuccessResponse {
	[JsonPropertyName(name: "data")]
	public Data data { get; set; }

	public class Data {
		[JsonPropertyName(name: "email")]
		public string email { get; set; }
	}
}
