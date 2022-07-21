using LEX_LegalSettings;
using LEX_RequestProcessService.Data;
using LEX_RequestProcessService.Models;

namespace LEX_RequestProcessService.Helpers;

public class ModelHelper
{

    private readonly IRequestProcessRepo _repository;

    public ModelHelper(IRequestProcessRepo repository)
    {
         Console.WriteLine($"--> Calling ModelHelper");
        _repository = repository;
    }

    public  IEnumerable<ProcessInfo> GetProcessInfoForEntitys(IEnumerable<Entity> entitys, ResponseType responseType)
    { 
        Console.WriteLine($"--> Calling GetProcessInfoForEntitys in ModelHelper");          
        GrpcSubjectDataModel subject = new GrpcSubjectDataModel();
        if(responseType.Name == "complex")
        {
            var subjectData = _repository.GetLegalSubjectData();        
            subject = subjectData.Subject;               
        }

        return (from e in entitys
            select new ProcessInfo
            {
                EntityId = e.Id,
                SourceDescription = _repository.GetSourceByKey(e.SourceKey).Description,
                SourceLawfulnessofProcessing = _repository.GetSourceByKey(e.SourceKey).LawfulnessProcessing,
                SourceName = _repository.GetSourceByKey(e.SourceKey).Name,
                Controller = subject == null ? null : subject.Controller,
                Dpo = subject == null ? null : subject.Dpo
            }
        ).ToList();
    }

}