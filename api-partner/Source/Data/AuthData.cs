namespace App;

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class VerifyAuthRequestBody {
	[Required]
	[JsonPropertyName(name: "access_token")]
	public string access_token { get; set; }
}

public class VerifyAuthResponse : ApiSuccessResponse {
	[JsonPropertyName(name: "data")]
	public VerifyAuthResponseData data { get; set; }

	public class VerifyAuthResponseData {
		[JsonPropertyName(name: "user_id")]
		public string userId { get; set; }
	}
}

public class LoginRequestBody {
	[Required]
	[JsonPropertyName(name: "email")]
	public string email { get; set; }

	[Required]
	[JsonPropertyName(name: "password")]
	public string password { get; set; }

	[Required]
	[Range(1, 3)]
	[JsonPropertyName(name: "client_type")]
	public byte client_type { get; set; }
}

public class LoginResponse : ApiSuccessResponse {
	[JsonPropertyName(name: "data")]
	public Data data { get; set; }

	public class Data {
		[JsonPropertyName(name: "token_schema")]
		public string token_schema { get; set; }

		[JsonPropertyName(name: "access_token")]
		public string access_token { get; set; }

		[JsonPropertyName(name: "refresh_token")]
		public string refresh_token { get; set; }
	}
}

public class ProviderLoginRequestBody {
	[Required]
	[JsonPropertyName(name: "provider")]
	public string provider { get; set; }

	[JsonPropertyName(name: "id_token")]
	public string? id_token { get; set; }

	[JsonPropertyName(name: "access_token")]
	public string? access_token { get; set; }
	
	[Required]
	[Range(1, 3)]
	[JsonPropertyName(name: "client_type")]
	public byte client_type { get; set; }
}

public class GoogleAccessTokenDecodingResponse : ApiSuccessResponse {
	[JsonPropertyName(name: "email")]
	public string email { get; set; }
}

public class SilentLoginRequestBody {
	[Required]
	[JsonPropertyName(name: "access_token")]
	public string access_token { get; set; }

	[Required]
	[JsonPropertyName(name: "refresh_token")]
	public string refresh_token { get; set; }
}

public class LogoutRequestBody {
	[Required]
	[JsonPropertyName(name: "refresh_token")]
	public string refresh_token { get; set; }

	[JsonPropertyName(name: "logout_everywhere")]
	public bool logout_everywhere { get; set; } = false;
}
