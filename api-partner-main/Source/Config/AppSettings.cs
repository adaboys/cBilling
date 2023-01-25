namespace App;

/// This mapping with `appsettings.json` file to avoid
/// hardcoded setting-reference at multiple places.

/// NOTE: If you want to rename field names, must rename section name in `appsettings.json` too.

/// dkopt: For better we should annotate fields to each name if appsettings.json
/// to avoid changes in refactoring process.
public partial class AppSetting {
	public ExchangeRate exchangeRate { get; set; }

	public class ExchangeRate {
		public string apiBaseUrl { get; set; }
		public string apiKey { get; set; }
	}
}
