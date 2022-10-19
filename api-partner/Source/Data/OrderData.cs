namespace App;

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class CancelOrderResponse : ApiSuccessResponse {
	[JsonPropertyName(name: "data")]
	public Data data { get; set; }

	public class Data {
		[JsonPropertyName(name: "status")]
		public byte status { get; set; }

		[JsonPropertyName(name: "hmac")]
		public string hmac { get; set; }
	}
}
