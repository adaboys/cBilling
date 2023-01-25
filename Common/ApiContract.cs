namespace App;

using System.Net;
using System.Text.Json.Serialization;
using Tool.Compet.Http;

/// Api status code which is included inside each api-response.
/// Ref: https://en.wikipedia.org/wiki/List_of_HTTP_status_codes
public class ApiStatusCode {
	public const int NG = -1;
	public const int OK = ((int)HttpStatusCode.OK);
	public const int BAD_REQUEST = ((int)HttpStatusCode.BadRequest);
	public const int UNAUTHORIZED = ((int)HttpStatusCode.Unauthorized);
	public const int NOT_FOUND = ((int)HttpStatusCode.NotFound);
	public const int INTERNAL_SERVER_ERROR = ((int)HttpStatusCode.InternalServerError);
	public const int MAINTENANCE = ((int)HttpStatusCode.ServiceUnavailable);
}

public class ApiResponse : DkApiResponse {
	[JsonPropertyName(name: "status")]
	public override int status { get; set; }

	[JsonPropertyName(name: "code")]
	public override string? code { get; set; }

	[JsonPropertyName(name: "message")]
	public override string? message { get; set; }

	public ApiResponse(int status, string message) {
		this.status = status;
		this.message = message;
	}

	/// Unility method for checking successful result.
	[JsonIgnore]
	public bool succeed => this.status == ApiStatusCode.OK;

	/// Unility method for checking failure result.
	[JsonIgnore]
	public bool failed => this.status != ApiStatusCode.OK;
}

public class ApiSuccessResponse : ApiResponse {
	public ApiSuccessResponse(string message = "Succeed") : base(ApiStatusCode.OK, message) { }
}

public class ApiFailureResponse : ApiResponse {
	public ApiFailureResponse(string code, string message = "Failed") : base(ApiStatusCode.NG, message) {
		this.code = code;
	}
}

public class ApiNotFoundResponse : ApiResponse {
	public ApiNotFoundResponse(string message = "Not found") : base(ApiStatusCode.NOT_FOUND, message) { }
}

public class ApiUnauthorizedResponse : ApiResponse {
	public ApiUnauthorizedResponse(string message = "Unauthorized") : base(ApiStatusCode.UNAUTHORIZED, message) { }
}

public class ApiBadRequestResponse : ApiResponse {
	public ApiBadRequestResponse(string message = "Bad request") : base(ApiStatusCode.BAD_REQUEST, message) { }
}

public class ApiInternalServerErrorResponse : ApiResponse {
	public ApiInternalServerErrorResponse(string message = "IError") : base(ApiStatusCode.INTERNAL_SERVER_ERROR, message) { }
}

public class ApiMaintenanceResponse : ApiResponse {
	public ApiMaintenanceResponse(string message = "Maintenance") : base(ApiStatusCode.MAINTENANCE, message) { }
}
