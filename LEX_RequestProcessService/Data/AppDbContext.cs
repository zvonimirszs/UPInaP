using LEX_RequestProcessService.Models;
using Microsoft.EntityFrameworkCore;

namespace LEX_RequestProcessService.Data;

    public class AppDbContext : DbContext
    {
        public AppDbContext (DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }            
        public DbSet<Request> Requests { get; set; }
        public DbSet<ResponseType> ResponseTypes { get; set; }
        public DbSet<ProcessInfo> ProcessInfo { get; set; }
        public DbSet<Entity> Entitys { get; set; }

        public DbSet<Subscription> Subscriptions { get; set; }

        public DbSet<Source> Sources { get; set; }

    //     protected override void OnModelCreating(ModelBuilder modelBuilder)
    //     {
    //         // modelBuilder
    //         //     .Entity<Entity>()
    //         //     .HasOne(p=> p.Subscription!);
    //         modelBuilder
    //             .Entity<Service>()
    //             .HasMany(p => p.Subscriptions)
    //             .WithOne(p=> p.Service!)
    //             .HasForeignKey(p => p.ServiceId);

    //         modelBuilder
    //             .Entity<Subscription>()
    //             .HasOne(p => p.Service)
    //             .WithMany(p => p.Subscriptions)
    //             .HasForeignKey(p =>p.ServiceId);

    //             //         modelBuilder
    //             // .Entity<Command>()
    //             // .HasOne(p => p.Platform)
    //             // .WithMany(p => p.Commands)
    //             // .HasForeignKey(p =>p.PlatformId);
    //     }
    }
