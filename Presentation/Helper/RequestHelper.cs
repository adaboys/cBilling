namespace App;

using Microsoft.Extensions.Primitives;

public class RequestHelper {
	/// Try to get request ip from various methods.
	/// @returns Client ip address or null if not found.
	public static string? GetClientIp(HttpRequest request, bool tryUseXForwardHeader = true) {
		var ip = request.HttpContext.Connection.RemoteIpAddress?.ToString();

		// X-Forwarded-For (csv list):  Using the First entry in the list seems to work
		// for 99% of cases however it has been suggested that a better (although tedious)
		// approach might be to read each IP from right to left and use the first public IP.
		// http://stackoverflow.com/a/43554000/538763
		if (string.IsNullOrWhiteSpace(ip) && tryUseXForwardHeader) {
			ip = _GetHeaderValue(request, "X-Forwarded-For");
		}

		// Try with header entry: REMOTE_ADDR
		if (string.IsNullOrWhiteSpace(ip)) {
			ip = _GetHeaderValue(request, "REMOTE_ADDR");
		}

		// _httpContextAccessor.HttpContext?.Request?.Host this is the local host.

		return ip;
	}

	public static string? GetUserAgent(HttpRequest request) {
		return _GetHeaderValue(request, "User-Agent");
	}

	private static string _GetHeaderValue(HttpRequest request, string headerName) {
		StringValues values;

		if (request.HttpContext.Request.Headers.TryGetValue(headerName, out values)) {
			return values.First();

			// // Writes out as Csv when there are multiple.
			// string rawValues = values.ToString();

			// if (!string.IsNullOrWhiteSpace(rawValues)) {
			// 	// return Convert.ChangeType(values.ToString(), typeof(string));
			// 	return values.ToString();
			// }
		}

		return string.Empty;
	}

	// private static List<string> _SplitCsv(string csvList) {
	// 	if (string.IsNullOrWhiteSpace(csvList)) {
	// 		return new List<string>();
	// 	}

	// 	return csvList
	// 		.TrimEnd(',')
	// 		.Split(',')
	// 		.AsEnumerable<string>()
	// 		.Select(s => s.Trim())
	// 		.ToList();
	// }
}
