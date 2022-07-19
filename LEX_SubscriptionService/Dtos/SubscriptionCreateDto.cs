using System;
using System.ComponentModel.DataAnnotations;
using LEX_SubscriptionService.Models;

namespace LEX_SubscriptionService.Dtos;

public class SubscriptionCreateDto
{
        [Required]
        public string Key { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Purpose { get; set; }
        public string Description { get; set; }
}