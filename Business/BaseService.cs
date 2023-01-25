namespace App;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

public partial class BaseService {
	protected readonly AppDbContext dbContext;
	protected readonly AppSetting appSetting;

	public BaseService(AppDbContext dbContext, IOptions<AppSetting> appSettingOpt) {
		this.dbContext = dbContext;
		this.appSetting = appSettingOpt.Value;
	}

	protected async Task<(UserModel, UserWalletModel)> FindRootUserAsync() {
		var query =
			from _user in this.dbContext.users
			join _user_wallet in this.dbContext.userWallets on _user.id equals _user_wallet.user_id
			where _user.role == UserModelConst.Role.Root
			select new {
				_user,
				_user_wallet
			}
		;

		var result = await query.FirstAsync();

		return (result._user, result._user_wallet);
	}
}
