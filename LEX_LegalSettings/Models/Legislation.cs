using System.ComponentModel.DataAnnotations;

namespace LEX_LegalSettings.Models;

public class Legislation
{
    [Required]
    public int Id { get; set; }    
    [Key]
    [Required]
    public int InternalId { get; set; }
    [Required]
    public string ArticleNo { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Link { get; set; }
}