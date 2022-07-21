using System.Collections.Generic;
using LEX_RequestProcessService.Models;

namespace LEX_RequestProcessService.SyncDataServices.Grpc
{
    /// <summary>
    /// gRPC Client (klijent)
    /// </summary>
    public interface ISubscriptionDataClient
    {
        /// <summary>
        /// dohvaća sve Subscription (pretplate) uz drugog servisa
        /// </summary>
        /// <returns>lista pretplata</returns>
        IEnumerable<Subscription> ReturnAllSubscriptions();
        /// <summary>
        /// dohvaća sve Entity-e (zahtjeve) uz drugog servisa
        /// </summary>
        /// <returns>lista zahtjeva</returns>
        IEnumerable<Entity> ReturnAllEntitys();
        /// <summary>
        /// dohvaća sve Entity-e (zahtjeve) uz drugog servisa
        /// </summary>
        /// <returns>lista zahtjeva</returns>
        IEnumerable<Entity> ReturnEntitysByIds(List<Entity> enityItems);
        /// <summary>
        /// dohvaća sve Sources (izvore) uz drugog servisa
        /// </summary>
        /// <returns>lista izvora</returns>
        IEnumerable<Source> ReturnAllSources();
    }
}