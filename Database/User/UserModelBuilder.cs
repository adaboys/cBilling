namespace App;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tool.Compet.Core;

/// Configure model for table `user`.

public class UserModelBuilder {
	/// To override auto-generation property, the app just need to set
	/// any value that is not default (null for string, 0 for int, Guid.Empty for Guid,...)
	/// value on the field.
	/// Modeling: https://docs.microsoft.com/en-us/ef/core/modeling/generated-properties?tabs=data-annotations

	/// Seeding data:
	/// Note that: we are using `ModelBuilder` for seeding data.
	/// We must manually generate PK for each model since this approach uses migration scripts
	/// to calculate next script, so it does not interact with the database.
	/// Data seeding: https://docs.microsoft.com/en-us/ef/core/modeling/data-seeding
	/// Data seeding: https://github.com/dotnet/EntityFramework.Docs/blob/main/entity-framework/core/modeling/data-seeding.md
	public static void OnModelCreating(ModelBuilder modelBuilder) {
		// Setup default unique identifier when create
		// By default, EF will use `newsequentialid()` PK for better performance.
		// We should not set default value since it will be auto handled by framework.
		// modelBuilder.Entity<UserModel>().Property(model => model.id);

		// Setup default datetime when add.
		// Note that: we should NOT use `ValueGeneratedOnAdd()` since it is no effect for DateTime type on MS SQL.
		// -> Use `HasDefaultValueSql()` instead.
		// See at warning section: https://docs.microsoft.com/en-us/ef/core/modeling/generated-properties?tabs=fluent-api#datetime-value-generation
		modelBuilder.Entity<UserModel>().Property(model => model.created_at).HasDefaultValueSql("getutcdate()");

		// Seed
		// var passwordHasher = new PasswordHasher<UserEntity>();

		// modelBuilder.Entity<UserEntity>().ToTable(DbConst.table_user).HasData(
		// 	new UserEntity().AlsoDk(model => {
		// 		model.id = Guid.NewGuid();
		// 		model.code = "darkcompet";
		// 		model.email = "darkcompet@gmail.com";
		// 		model.password = passwordHasher.HashPassword(model, "1234");
		// 		model.role = UserTableConst.ROLE_ROOT;
		// 		model.gender = UserTableConst.GENDER_MALE;
		// 	}),
		// 	new UserEntity().AlsoDk(model => {
		// 		model.id = Guid.NewGuid();
		// 		model.code = "lightcompet";
		// 		model.email = "lightcompet@gmail.com";
		// 		model.password = passwordHasher.HashPassword(model, "1234");
		// 		model.role = UserTableConst.ROLE_ADMIN;
		// 		model.gender = UserTableConst.GENDER_FEMALE;
		// 	}),
		// 	new UserEntity().AlsoDk(model => {
		// 		model.id = Guid.NewGuid();
		// 		model.code = "yellowcompet";
		// 		model.email = "yellowcompet@gmail.com";
		// 		model.password = passwordHasher.HashPassword(model, "1234");
		// 		model.role = UserTableConst.ROLE_USER;
		// 		model.gender = UserTableConst.GENDER_OTHER;
		// 	})
		// );
	}
}
