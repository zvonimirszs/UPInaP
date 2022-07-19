using System.ComponentModel.DataAnnotations;

namespace LEX_SubscriptionService.Models.Authenticate;
public class AuthenticateRequest
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
}