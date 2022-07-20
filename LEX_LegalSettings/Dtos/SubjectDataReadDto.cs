using System.ComponentModel.DataAnnotations;

namespace LEX_LegalSettings.Dtos;

public class SubjectDataReadDto
{
    public int Id { get; set; }
    // voditelj obrade
    public string Controller { get; set; }
    // izvršitelj obrade
    public string Processor { get; set; }
    //Dpo
    public string DataProtectionOfficer { get; set; }

}