using LEX_IdentityService.Models.Authenticate;
using Microsoft.EntityFrameworkCore;

namespace LEX_IdentityService.Data;

public class AppDbContext : DbContext
{
    public AppDbContext (DbContextOptions<AppDbContext> opt) : base(opt)
    {

    }            
        

    public DbSet<User> Users { get; set; }

}