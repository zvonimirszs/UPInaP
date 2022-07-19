using System.ComponentModel.DataAnnotations;

namespace LEX_LegalSettings.Models;

// tipovi upita tj. prava ispitanika
public class RequestType
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string LegalConnection { get; set; }
    
}