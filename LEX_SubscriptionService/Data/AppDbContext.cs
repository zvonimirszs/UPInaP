using Microsoft.EntityFrameworkCore;
using LEX_SubscriptionService.Models;

namespace LEX_SubscriptionService.Data;
public class AppDbContext : DbContext
{
    public AppDbContext (DbContextOptions<AppDbContext> opt) : base(opt)
    {

    }
    public DbSet<Service> Services { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }        
    public DbSet<Source> Sources { get; set; }
    public DbSet<Entity> Entitys { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Service>()
            .HasMany(p => p.Subscriptions)
            .WithOne(p=> p.Service!);

        modelBuilder
            .Entity<Subscription>()
            .HasOne(p => p.Service)
            .WithMany(p => p.Subscriptions)
            .HasForeignKey(p =>p.ServiceId);
    }
}

