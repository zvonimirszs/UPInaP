using System.ComponentModel.DataAnnotations;

namespace LEX_SubscriptionService.Models;

public class Entity
{
    [Key]
    [Required]
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [Required]
    public string Email { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string PostNo { get; set; }
    public string Description { get; set; }
    public string SourceKey { get; set; }
    [Required]
    public int SubscriptionId { get; set; }
    [Required]
    public Subscription Subscription { get; set; }
}