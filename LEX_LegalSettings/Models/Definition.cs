using System.ComponentModel.DataAnnotations;

namespace LEX_LegalSettings.Models;

public class Definition
{

    [Required]
    public int Id { get; set; }   
    [Key]
    [Required]
    public int InternalId { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
}