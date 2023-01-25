namespace App;

using Microsoft.EntityFrameworkCore;
using Tool.Compet.Core;

public class MstAppModelBuilder {
	public static void OnModelCreating(ModelBuilder modelBuilder) {
		// This is auto-increment PK which be configured (annotated with `Key`) at model.
		modelBuilder.Entity<MstAppModel>().Property(model => model.id);
		// Setup default datetime when create
		modelBuilder.Entity<MstAppModel>().Property(model => model.created_at).HasDefaultValueSql("getutcdate()");
	}

	public static void Seed(ModelBuilder modelBuilder) {
		modelBuilder.Entity<MstAppModel>().ToTable(DbConst.table_mst_app).HasData(
			new MstAppModel().AlsoDk(model => {
				model.id = 1;
				model.os_type = MstAppModelConst.OSTYPE_ANDROID;
				model.version = "1.0.0";
			}),
			new MstAppModel().AlsoDk(model => {
				model.id = 2;
				model.os_type = MstAppModelConst.OSTYPE_IOS;
				model.version = "1.0.0";
			}),
			new MstAppModel().AlsoDk(model => {
				model.id = 3;
				model.os_type = MstAppModelConst.OSTYPE_WEBGL;
				model.version = "1.0.0";
			}),
			new MstAppModel().AlsoDk(model => {
				model.id = 4;
				model.os_type = MstAppModelConst.OSTYPE_MACOS;
				model.version = "1.0.0";
			}),
			new MstAppModel().AlsoDk(model => {
				model.id = 5;
				model.os_type = MstAppModelConst.OSTYPE_WINDOWS;
				model.version = "1.0.0";
			})
		);
	}
}
