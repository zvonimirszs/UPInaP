namespace LEX_RequestProcessService.Dtos;
public class EntityPublishedDto
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string SubscriptionKey { get; set; }  
    public string SubscriptionName { get; set; }  
    public string Event { get; set; }
    public string SourceKey { get; set; }
}
