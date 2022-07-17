using System.ComponentModel.DataAnnotations;

namespace LEX_LegalSettings.Models;

// Zakonitost obrade 
public class LawfulnessProcessing
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    public string LegalConnection { get; set; }
    
}