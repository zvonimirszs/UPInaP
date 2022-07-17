using LEX_RequestProcessService.Models;

namespace LEX_RequestProcessService.Dtos;

public class RequestReadDto
{
    public int Id { get; set; }
    public string IdentificationString { get; set; }
    public string IdentificationKey { get; set; }
    public string SourceKey { get; set; }
    public string ResponseTypeName { get; set; }  

    public ResponseTypeReadDto ResponseType { get; set; }

}