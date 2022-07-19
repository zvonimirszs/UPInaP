using System.ComponentModel.DataAnnotations;

namespace LEX_LegalSettings.Dtos;

public class DefinitionReadDto
{

    public int Id { get; set; }   
    public string Name { get; set; }
    public string Description { get; set; }
}