using System.Collections.Generic;
using LEX_RequestRecordsService.Models;

namespace LEX_RequestRecordsService.SyncDataServices.Grpc
{
    /// <summary>
    /// gRPC Client (klijent)
    /// </summary>
    public interface ILegalSettingsDataClient
    {
        /// <summary>
        /// dohvaća sve RequestType (tipove) uz drugog servisa
        /// </summary>
        /// <returns>lista tipova</returns>
        IEnumerable<RequestType> ReturnAllRequestTypes();  
    }
}