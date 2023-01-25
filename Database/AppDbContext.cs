namespace App;

using Microsoft.EntityFrameworkCore;

/// Database management for the app.
public class AppDbContext : DbContext {
	/// We need this constructor for configuration via `appsetting.json`
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

	public DbSet<UserModel> users { get; set; }
	public DbSet<UserRegistryModel> userRegistries { get; set; }
	public DbSet<UserAuthTokenModel> userAuthTokens { get; set; }
	public DbSet<UserWalletModel> userWallets { get; set; }

	public DbSet<PartnerModel> partners { get; set; }
	public DbSet<PartnerWalletModel> partnerWallets { get; set; }
	public DbSet<ProjectModel> projects { get; set; }
	public DbSet<PartnerAuthTokenModel> partnerAuthTokens { get; set; }

	public DbSet<OrderModel> orders { get; set; }

	/// Construct model + Seeding data
	protected override void OnModelCreating(ModelBuilder modelBuilder) {
		UserModelBuilder.OnModelCreating(modelBuilder);
		UserWalletModelBuilder.OnModelCreating(modelBuilder);
		UserRegistryModelBuilder.OnModelCreating(modelBuilder);
		UserAuthTokenModelBuilder.OnModelCreating(modelBuilder);

		PartnerModelBuilder.OnModelCreating(modelBuilder);
		PartnerWalletModelBuilder.OnModelCreating(modelBuilder);
		ProjectModelBuilder.OnModelCreating(modelBuilder);
		PartnerAuthTokenModelBuilder.OnModelCreating(modelBuilder);

		OrderModelBuilder.OnModelCreating(modelBuilder);
	}

	public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) {
		var modifiedEntries = this.ChangeTracker.Entries()
			.Where(x => x.State == EntityState.Modified)
			.Select(x => x.Entity)
		;
		foreach (var modifiedEntry in modifiedEntries) {
			var entity = modifiedEntry as AutoGenerateUpdateTime;
			if (entity != null) {
				entity.updated_at = DateTime.UtcNow;
			}
		}

		return base.SaveChangesAsync(cancellationToken);
	}
}
