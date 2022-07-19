using System.ComponentModel.DataAnnotations;

namespace LEX_LegalSettings.Dtos;

public class LegislationReadDto
{
    public int Id { get; set; }    
    public string ArticleNo { get; set; }
    public string Name { get; set; }
    public string Link { get; set; }
}