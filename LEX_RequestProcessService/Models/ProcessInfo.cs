// sadrži podatke koja su definirana člankom 15, 12, 13 i 14
using System.ComponentModel.DataAnnotations;

namespace LEX_RequestProcessService.Models;

// podaci u odgovoru: svrha obrade, 
// vezano uz voditelja obrade, svrhu, pravni temelj
public class ProcessInfo
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required]
    public int EntityId { get; set; }
    // u Source
    public string SourceName { get; set; }
    // u Source
    public string SourceDescription { get; set; }
    // u Source
    public string SourceLawfulnessofProcessing { get; set; }
    // u SubjectData
    public string Controller { get; set; }
    // u SubjectData
    public string Dpo { get; set; }
}
