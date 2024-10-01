namespace WebApi.Comuni.Models.Services.Infrastructure;

public partial class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
	public virtual DbSet<Location> Locations { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<Location>(entity =>
		{
			entity.ToTable("Comuni");
			entity.HasKey(e => e.ComuneId);
		});
	}
}