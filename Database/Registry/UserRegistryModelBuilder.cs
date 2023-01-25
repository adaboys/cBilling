namespace App;

using Microsoft.EntityFrameworkCore;

public class UserRegistryModelBuilder {
	public static void OnModelCreating(ModelBuilder modelBuilder) {
		// Setup default datetime when create
		modelBuilder.Entity<UserRegistryModel>().Property(model => model.created_at).HasDefaultValueSql("getdate()");
	}
}
