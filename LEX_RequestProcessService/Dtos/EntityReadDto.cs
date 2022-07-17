using LEX_RequestProcessService.Models;

namespace LEX_RequestProcessService.Dtos;
public class EntityReadDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string PostNo { get; set; }
    public string Description { get; set; }
    public ProcessInfo ProcessInfo { get; set; }
    public int SubscriptionId { get; set; }
    public Subscription Subscription { get; set; }
}