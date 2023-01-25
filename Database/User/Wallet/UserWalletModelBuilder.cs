namespace App;

using Microsoft.EntityFrameworkCore;

public class UserWalletModelBuilder {
	public static void OnModelCreating(ModelBuilder modelBuilder) {
		// Setup default datetime when create
		modelBuilder.Entity<UserWalletModel>().Property(model => model.created_at).HasDefaultValueSql("getutcdate()");
	}
}
