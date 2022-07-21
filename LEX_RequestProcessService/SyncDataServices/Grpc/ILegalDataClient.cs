using System.Collections.Generic;
using LEX_LegalSettings;
using LEX_RequestProcessService.Models;

namespace LEX_RequestProcessService.SyncDataServices.Grpc
{
    /// <summary>
    /// gRPC Client (klijent)
    /// </summary>
    public interface ILegalDataClient
    {
        SubjectDataResponse ReturnSubjectData();

        DefinitionResponse ReturnDefinitions();

        LegislationResponse ReturnLegislations(GrpcRequestLegalModel brojClanaka);
    }
}