namespace App;

using Microsoft.Extensions.Options;

public class BaseRepo {
	protected readonly AppSetting appSetting;

	public BaseRepo(IOptions<AppSetting> appSettingOpt) {
		this.appSetting = appSettingOpt.Value;
	}
}
