using System.IdentityModel.Tokens.Jwt;
using LEX_IdentityService.Models.Authenticate;

namespace LEX_IdentityService.IdentityServices;
public interface IJwtUtils
{
    public string GenerateJwtToken(User user);
    //public int? ValidateJwtToken(string token);
    public JwtSecurityToken ValidateJwtToken(string token);
}