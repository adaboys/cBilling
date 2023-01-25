namespace App;

using Microsoft.EntityFrameworkCore;

public class TxDetailModelBuilder {
	public static void OnModelCreating(ModelBuilder modelBuilder) {
		// This is auto-increment PK which be configured (annotated with `Key`) at model.
		modelBuilder.Entity<TxDetailModel>().Property(model => model.id);
		// Setup default datetime when create
		modelBuilder.Entity<TxDetailModel>().Property(model => model.created_at).HasDefaultValueSql("getdate()");
	}
}
