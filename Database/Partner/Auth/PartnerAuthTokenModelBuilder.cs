namespace App;

using Microsoft.EntityFrameworkCore;

public class PartnerAuthTokenModelBuilder {
	public static void OnModelCreating(ModelBuilder modelBuilder) {
		// Setup default datetime when create
		modelBuilder.Entity<PartnerAuthTokenModel>().Property(model => model.created_at).HasDefaultValueSql("getutcdate()");
	}
}
