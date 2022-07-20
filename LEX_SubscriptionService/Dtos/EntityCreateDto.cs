using Microsoft.AspNetCore.Mvc;

namespace LEX_SubscriptionService.Dtos;

public class EntityCreateDto
{
  
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostNo { get; set; }
        public string Description { get; set; }
        [FromHeader]
        public string SourceKey { get; set; }
}