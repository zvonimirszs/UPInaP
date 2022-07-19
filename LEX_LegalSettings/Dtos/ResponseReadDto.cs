using System.ComponentModel.DataAnnotations;
using Google.Protobuf.Collections;

namespace LEX_LegalSettings.Dtos;

public class ResponseReadDto
{
    public SubjectDataReadDto subjectReadDto {get; set;}
    public RepeatedField<LegislationReadDto> legislationReadDto {get; set;} = new RepeatedField<LegislationReadDto>();
    public RepeatedField<DefinitionReadDto> definitionReadDto {get; set;} = new RepeatedField<DefinitionReadDto>();
}