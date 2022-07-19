using System.ComponentModel.DataAnnotations;

namespace LEX_RequestRecordsService.Models;    
   // tip upita: pravo na pristup, pravo na ispravak, pravo na brisanje, pravo na ograničenje obrade, pravo na prenos podataka, pravo na prigovor
public class RequestType
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required]
    public int ExternalId { get; set; }
    [Required]
    public string Name { get; set; }        
    public ICollection<Request> Requests { get; set; } = new List<Request>();
}