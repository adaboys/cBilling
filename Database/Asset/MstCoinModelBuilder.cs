namespace App;

using Microsoft.EntityFrameworkCore;
using Tool.Compet.Core;

public class MstCoinModelBuilder {
	public static void OnModelCreating(ModelBuilder modelBuilder) {
		// This is auto-increment PK which be configured (annotated with `Key`) at model.
		modelBuilder.Entity<MstCoinModel>().Property(model => model.id);
		// Setup default datetime when create
		modelBuilder.Entity<MstCoinModel>().Property(model => model.created_at).HasDefaultValueSql("getdate()");
	}

	public static void Seed(ModelBuilder modelBuilder) {
		modelBuilder.Entity<MstCoinModel>().ToTable(DbConst.table_mst_coin).HasData(
			new MstCoinModel().AlsoDk(model => {
				model.id = 1;
				model.coin_name = MstCoinModelConst.COIN_NAME_ADA;
				model.token_id = MstCoinModelConst.TOKEN_ID_ADA;
			})
		);
	}
}
