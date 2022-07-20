using System.ComponentModel.DataAnnotations;

namespace LEX_SubscriptionService.Models;

public class Source
{
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string SourceKey { get; set; }
        public string Description { get; set; }
        [Required]
        public string LawfulnessProcessing { get; set; }
}