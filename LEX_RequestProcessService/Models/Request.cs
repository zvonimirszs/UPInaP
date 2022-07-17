using System.ComponentModel.DataAnnotations;

namespace LEX_RequestProcessService.Models;

public class Request
{
    [Key]
    [Required]
    public int Id { get; set; }
    // identifikacijska riječ ili riječi
    [Required]
    public string IdentificationString { get; set; }
    // može biti po email adresi, po ključu korisničkom računu usluge, po korisničkom računu usluge         
    [Required]
    public string IdentificationKey { get; set; }
    // tko je izvor slanja upita  
    [Required]
    public string SourceKey { get; set; }
    public int ResponseTypeId { get; set; }
    public ResponseType ResponseType { get; set; }

}
