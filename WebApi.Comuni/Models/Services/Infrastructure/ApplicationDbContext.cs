using Microsoft.EntityFrameworkCore;
using WebApi.Comuni.Models.Entities;

#nullable disable

namespace WebApi.Comuni.Models.Services.Infrastructure;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    { }

    public virtual DbSet<Location> Locations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Location>(entity =>
        {
            entity.ToTable("Comuni");
            entity.HasKey(e => e.ComuneId);
            // entity.Property(e => e.Cap).HasColumnType("TEXT(10)");
            // entity.Property(e => e.Comune).HasColumnType("TEXT(200)");
            // entity.Property(e => e.Provincia).HasColumnType("TEXT(2)");
            // entity.Property(e => e.Regione).HasColumnType("TEXT(100)");
        });
    }
}