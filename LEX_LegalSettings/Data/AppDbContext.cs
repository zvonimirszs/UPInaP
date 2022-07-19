using LEX_LegalSettings.Models;
using Microsoft.EntityFrameworkCore;

namespace LEX_LegalSettings.Data;

public class AppDbContext : DbContext
{
    public AppDbContext (DbContextOptions<AppDbContext> opt) : base(opt)
    {

    }            
    public DbSet<RequestType> RequestTypes { get; set; }
    public DbSet<Definition> Definitions { get; set; }
    public DbSet<LawfulnessProcessing> LawfulnessProcessings { get; set; }
    public DbSet<SubjectData> SubjectDatas { get; set; }
    public DbSet<Legislation> Legislations { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder
        //      .Entity<Definition>()
        //      .HasNoKey();
        // modelBuilder
        //      .Entity<Legislation>()
        //      .HasNoKey();

    }
}
