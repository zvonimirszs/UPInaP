using System.ComponentModel.DataAnnotations;

namespace LEX_RequestProcessService.Models;

    public class Entity
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public int ExternalId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostNo { get; set; }
        public string Description { get; set; }
        public int SubscriptionId { get; set; }
        public string SourceKey { get; set; }
        public bool Connected { get; set; }
    }