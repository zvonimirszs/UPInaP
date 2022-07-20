namespace LEX_IdentityService.Data;
using LEX_IdentityService.Models.Authenticate;
public class IdentityRepo : IIdentityRepo
{
        private readonly AppDbContext _context;
        //private readonly IIdentityDataClient _identityservice;

        public IdentityRepo(AppDbContext context)
        {
            _context = context;
        }
        public User GetUserByUserName(string userName)
        {            
            return(_context.Users.SingleOrDefault(x => x.Username == userName));
        }
        public bool SaveChanges()
        {            
            return(_context.SaveChanges() >= 0);
        }

}