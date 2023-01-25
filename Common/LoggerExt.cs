namespace App;

//todo: for high performance, use LoggerMessage instead of calling directly logger.LogXXX()
public static class LoggerExt {
	public static void DebugDk(this ILogger logger, object where, string format, params object?[] args) {
		logger.LogDebug(_DecorateLogFormat(where, format), args);
	}

	public static void InfoDk(this ILogger logger, object where, string format, params object?[] args) {
		logger.LogInformation(_DecorateLogFormat(where, format), args);
	}

	public static void WarningDk(this ILogger logger, object where, string format, params object?[] args) {
		logger.LogWarning(_DecorateLogFormat(where, format), args);
	}

	public static void ErrorDk(this ILogger logger, object where, string format, params object?[] args) {
		logger.LogError(_DecorateLogFormat(where, format), args);
	}

	public static void CriticalDk(this ILogger logger, object where, string format, params object?[] args) {
		logger.LogCritical(_DecorateLogFormat(where, format), args);
	}

	private static string _DecorateLogFormat(object where, string format) {
		return $"{where.GetType().Name}~ {format}";
	}
}
