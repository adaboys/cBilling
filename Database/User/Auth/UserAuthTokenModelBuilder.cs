namespace App;

using Microsoft.EntityFrameworkCore;

public class UserAuthTokenModelBuilder {
	public static void OnModelCreating(ModelBuilder modelBuilder) {
		// Setup default datetime when create
		modelBuilder.Entity<UserAuthTokenModel>().Property(model => model.created_at).HasDefaultValueSql("getutcdate()");
	}
}
