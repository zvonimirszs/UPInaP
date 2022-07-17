namespace LEX_SubscriptionService.Dtos;

public class EntityReadDto
{
        public int Id { get; set; }
        public string FirstName { get; set; }   
        public string LastName { get; set; }        
        public string Email { get; set; }
        public string SubscriptionName { get; set; }
        public string SubscriptionKey { get; set; }
        public string SourceKey { get; set; } 
}