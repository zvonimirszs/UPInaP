using LEX_RequestRecordsService.Models;
using Microsoft.EntityFrameworkCore;

namespace LEX_RequestRecordsService.Data;

    public class AppDbContext : DbContext
    {
        public AppDbContext (DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }            
        
        public DbSet<Request> Requests { get; set; }
        public DbSet<RequestType> RequestTypes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<RequestType>()
                .HasMany(p => p.Requests)
                .WithOne(p=> p.RequestType!)
                .HasForeignKey(p => p.RequestTypeId);

            modelBuilder
                .Entity<Request>()
                .HasOne(p => p.RequestType)
                .WithMany(p => p.Requests)
                .HasForeignKey(p =>p.RequestTypeId);
        }
    }