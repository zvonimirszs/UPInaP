using System.ComponentModel.DataAnnotations;

namespace LEX_SubscriptionService.Models;

public class Service
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }

    public ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
}