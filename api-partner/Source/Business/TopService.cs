namespace App;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

public class TopService : BaseService {
	public TopService(AppDbContext dbContext, IOptions<AppSetting> appSettingOpt) : base(dbContext, appSettingOpt) {
	}

	public async Task<object> Foo() {
		return $"[{this.appSetting.environment}-{this.appSetting.version.name}] Hellow, this is top page.";
	}
}
