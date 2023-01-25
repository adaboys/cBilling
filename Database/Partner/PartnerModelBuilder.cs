namespace App;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tool.Compet.Core;

public class PartnerModelBuilder {
	public static void OnModelCreating(ModelBuilder modelBuilder) {
		// Setup default datetime when create
		modelBuilder.Entity<PartnerModel>().Property(model => model.created_at).HasDefaultValueSql("getutcdate()");

		// // Seed
		// var passwordHasher = new PasswordHasher<PartnerModel>();

		// modelBuilder.Entity<PartnerModel>().ToTable(DbConst.table_partner).HasData(
		// 	new PartnerModel().AlsoDk(model => {
		// 		model.id = 1;
		// 		model.code = "darkcompet";
		// 		model.email = "darkcompet@gmail.com";
		// 		model.password = passwordHasher.HashPassword(model, "1234");
		// 		model.name = "darkcompet shop";
		// 	}),
		// 	new PartnerModel().AlsoDk(model => {
		// 		model.id = 2;
		// 		model.code = "thonyandersonan";
		// 		model.email = "thonyandersonan@gmail.com";
		// 		model.password = passwordHasher.HashPassword(model, "1234");
		// 		model.name = "thonyandersonan shop";
		// 	})
		// );
	}
}
