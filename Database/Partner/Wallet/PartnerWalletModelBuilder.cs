namespace App;

using Microsoft.EntityFrameworkCore;

public class PartnerWalletModelBuilder {
	public static void OnModelCreating(ModelBuilder modelBuilder) {
		// Setup default datetime when create
		modelBuilder.Entity<PartnerWalletModel>().Property(model => model.created_at).HasDefaultValueSql("getutcdate()");
	}
}
