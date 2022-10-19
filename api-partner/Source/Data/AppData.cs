namespace App;

using System.Text.Json.Serialization;

public class AppResponse : ApiSuccessResponse {
	[JsonPropertyName(name: "data")]
	public AppData data { get; set; }

	public class AppData {
		[JsonPropertyName(name: "os_id")]
		public int osId { get; set; }

		[JsonPropertyName(name: "version")]
		public string version { get; set; }
	}
}
