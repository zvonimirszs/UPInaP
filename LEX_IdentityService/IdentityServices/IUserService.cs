using System.IdentityModel.Tokens.Jwt;
using LEX_IdentityService.Models.Authenticate;

namespace LEX_IdentityService.IdentityServices;
public interface IUserService
{
    AuthenticateResponse Authenticate(AuthenticateRequest model);

    AuthenticateResponse ValidateToken(string token);
    IEnumerable<User> GetAll();
    User GetById(int id);
}