using System.ComponentModel.DataAnnotations;

namespace LEX_LegalSettings.Models;

public class SubjectData
{
    [Key]
    [Required]
    public int Id { get; set; }
    // voditelj obrade
    [Required]
    public string Controller { get; set; }
    // izvršitelj obrade
    public string Processor { get; set; }
    //Dpo
    public string DataProtectionOfficer { get; set; }

}