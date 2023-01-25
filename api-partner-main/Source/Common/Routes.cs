namespace App;

/// Api routes for web.
public partial class Routes {
	/// [Top]
	public const string top = "top"; // {"methods":["GET"]}

	/// [App]
	public const string app_info = "app/{os_id}/info"; // {"methods":["GET"]}

	/// [Auth]
	public const string auth_login = "auth/login"; // {"methods":["POST"]}
	public const string auth_silentLogin = "auth/silentLogin"; // {"methods":["POST"]}
	public const string auth_loginWithProvider = "auth/loginWithProvider"; // {"methods":["POST"]}
	public const string auth_logout = "auth/logout"; // {"methods":["POST"]}

	/// [Partner]
	public const string partner_register_attempt = "partner/register/attempt"; // {"methods":["POST"]}
	public const string partner_register_confirm = "partner/register/confirm"; // {"methods":["POST"]}
	public const string register_temp = "partner/register/temp"; // {"methods":["POST"]}
	public const string register_confirm = "partner/register/confirm"; // {"methods":["POST"]}
	public const string partner_profile = "partner/profile"; // {"methods":["GET"]}

	/// [Order]
	public const string order_create = "order/create"; // {"methods":["POST"]}
	public const string order_info = "order/{order_code}"; // {"methods":["GET"]}
	public const string order_pay = "order/pay"; // {"methods":["POST"]}
	public const string order_cancel = "order/{order_code}/cancel"; // {"methods":["POST"]}

	/// [Project]
	public const string project_create = "project/create"; // {"methods":["POST"]}
	public const string project_remove = "project/{id}/remove"; // {"methods":["POST"]}
	public const string project_list = "project/list"; // {"methods":["GET"]}
}
