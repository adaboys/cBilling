namespace App;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tool.Compet.Core;

public class ProjectModelBuilder {
	public static void OnModelCreating(ModelBuilder modelBuilder) {
		// Setup default datetime when create
		modelBuilder.Entity<ProjectModel>().Property(model => model.created_at).HasDefaultValueSql("getutcdate()");
	}
}
