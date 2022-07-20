using LEX_RequestProcessService.Models;

namespace LEX_RequestProcessService.Dtos;

public class RequestPublishedDto
{
    public int Id { get; set; }
    public string IdentificationString { get; set; }
    public string IdentificationKey { get; set; }
    public string SourceKey { get; set; }
    public DateTime StartDate { get; set; }
    public string Event { get; set; }
    public string RequestTypeName { get; set; }  
    public string ResponseTypeName { get; set; }  

    public ResponseType ResponseType { get; set; }

}