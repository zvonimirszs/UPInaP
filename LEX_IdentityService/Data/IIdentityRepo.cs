using LEX_IdentityService.Models.Authenticate;

namespace LEX_IdentityService.Data;
public interface IIdentityRepo
{
    bool SaveChanges();
    // User
    //IEnumerable<User> GetAllUsers();
    User GetUserByUserName(string userName);
    //void CreateUser(User user);
    //void UpdateUser(User user);
    //bool UserExists(int userId);

}