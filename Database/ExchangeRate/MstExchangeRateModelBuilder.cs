namespace App;

using Microsoft.EntityFrameworkCore;
using Tool.Compet.Core;

public class MstExchangeRateModelBuilder {
	public static void OnModelCreating(ModelBuilder modelBuilder) {
		// This is auto-increment PK which be configured (annotated with `Key`) at model.
		modelBuilder.Entity<MstExchangeRateModel>().Property(model => model.id);
		// Setup default datetime when create
		modelBuilder.Entity<MstExchangeRateModel>().Property(model => model.created_at).HasDefaultValueSql("getdate()");
	}

	public static void Seed(ModelBuilder modelBuilder) {
		modelBuilder.Entity<MstExchangeRateModel>().ToTable(DbConst.table_mst_exchange_rate).HasData(
			new MstExchangeRateModel().AlsoDk(model => {
				model.id = 1;
				model.from_currency = MstExchangeRateModelConst.ADA;
				model.to_currency = MstExchangeRateModelConst.USD;
				model.closing_rate = 0.443551m;
			}),
			new MstExchangeRateModel().AlsoDk(model => {
				model.id = 2;
				model.from_currency = MstExchangeRateModelConst.USD;
				model.to_currency = MstExchangeRateModelConst.ADA;
				model.closing_rate = 1m / 0.443551m;
			}),
			new MstExchangeRateModel().AlsoDk(model => {
				model.id = 3;
				model.from_currency = MstExchangeRateModelConst.ISKY;
				model.to_currency = MstExchangeRateModelConst.ADA;
				model.closing_rate = 0.25m;
			}),
			new MstExchangeRateModel().AlsoDk(model => {
				model.id = 4;
				model.from_currency = MstExchangeRateModelConst.ADA;
				model.to_currency = MstExchangeRateModelConst.ISKY;
				model.closing_rate = 1m / 0.25m;
			})
		);
	}
}
