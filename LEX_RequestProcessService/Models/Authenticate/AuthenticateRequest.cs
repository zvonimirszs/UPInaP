using System.ComponentModel.DataAnnotations;

namespace LEX_RequestProcessService.Models.Authenticate;
public class AuthenticateRequest
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
}