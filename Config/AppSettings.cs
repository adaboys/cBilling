namespace App;

/// This mapping with `appsettings.json` file to avoid
/// hardcoded setting-reference at multiple places.

/// NOTE: If you want to rename field names, must rename section name in `appsettings.json` too.

/// dkopt: For better we should annotate fields to each name if appsettings.json
/// to avoid changes in refactoring process.
public partial class AppSetting {
	/// Name of sections in `appSetting.json` file.
	public const string SECTION_APP = "App";

	public bool debug { get; set; }

	/// Current environment
	public string environment { get; set; }
	public const string ENV_DEVELOPMENT = "development";
	public const string ENV_STAGING = "staging";
	public const string ENV_PRODUCTION = "production";

	/// Contains list of apiKey which be used by another services to call this server's apis.
	/// Note: To call service-apis of this server, each another service need provide an entry api_key at header.
	public string[] serviceApiKeys { get; set; }

	public Version version { get; set; }

	/// JWT Authentication
	public JwtSetting jwt { get; set; }

	/// Database connection strings
	public Database database { get; set; }

	/// To create SMTP credentials, see:
	/// https://ap-southeast-1.console.aws.amazon.com/ses/home?region=ap-southeast-1#/account
	/// https://stackoverflow.com/questions/57517582/authentication-required-smtpexception-trying-to-send-mail-from-ec2-instance
	public class Mail {
		public string fromEmail { get; set; }
		public string fromName { get; set; }
		public string smtpUsername { get; set; }
		public string smtpPassword { get; set; }
		public string region { get; set; }
	}

	/// Cardano Node server info
	public CardanoNode cardanoNode { get; set; }

	public Mail mail { get; set; }

	public Aws aws { get; set; }


	public class Version {
		public int code { get; set; }
		public string name { get; set; }
	}

	public class JwtSetting {
		public string key { get; set; }
		public string issuer { get; set; }
		public string audience { get; set; }
		public string subject { get; set; }
		/// JWT access token timeout
		public long expiresInSeconds { get; set; }
		/// JWT refresh access token timeout
		public long refreshExpiresInSeconds { get; set; }
	}

	public class Database {
		public string connection { get; set; }
	}

	public class CardanoNode {
		public string apiKey { get; set; }
		public string apiBaseUrl { get; set; }
	}

	public class Aws {
		public string s3BaseUrl { get; set; }
	}
}
