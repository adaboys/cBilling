namespace App;

public partial class AppConst {
	public class jwt {
		/// Common
		public const string claim_type_client_type = "_ctp";

		/// For user
		public const string claim_type_user_id = "_uid";
		public const string claim_type_user_role = "_url";

		/// For partner
		public const string claim_type_partner_id = "_pid";
		public const string claim_type_partner_role = "_prl";
	}

	public const string CORS_POLICY_ALLOW_ANY = "AllowAny";
}
