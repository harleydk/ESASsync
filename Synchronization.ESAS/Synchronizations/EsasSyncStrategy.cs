using Synchronization.ESAS.DAL.Models;
using Synchronization.ESAS.Models;
using Synchronization.ESAS.SyncDestinations;
using Synchronization.ESAS.Synchronizations.EntityLoaderStrategies;
using Microsoft.Extensions.Logging;
using System;

namespace Synchronization.ESAS.Synchronizations
{

    public class EsasSyncStrategy : IEsasSyncStrategy
    {
        private readonly SyncStrategySettings _syncStrategySettings;
        private readonly IEsasEntitiesLoaderStrategy _esasEntitiesLoaderStrategy;
        private readonly IEsasSyncDestination _esasSyncDestination;
        private readonly ISyncResultsDestination _syncResultsDestination;
        private readonly ILogger _logger;

        public SyncStrategySettings SyncStrategySettings => _syncStrategySettings;

        public EsasSyncStrategy(SyncStrategySettings syncStrategySettings, IEsasEntitiesLoaderStrategy esasEntitiesLoaderStrategy, IEsasSyncDestination syncDestination, ISyncResultsDestination syncResultsDestination, ILogger logger)
        {
            _syncResultsDestination = syncResultsDestination;
            _logger = logger;
            _syncStrategySettings = syncStrategySettings;
            _esasEntitiesLoaderStrategy = esasEntitiesLoaderStrategy;
            _esasSyncDestination = syncDestination;
        }

        public override string ToString()
        {
            string representation = $"{this.GetType().Name}(entityLoadStrategy={_esasEntitiesLoaderStrategy.GetType().Name}|syncDestination={_esasSyncDestination.GetType().Name}";
            return representation;
        }

        public void ExecuteSyncStrategy()
        {
            _logger.LogInformation($"Executing sync-strategy with corresponding load-strategy {this._esasEntitiesLoaderStrategy.GetType().Name} and destination of {_syncResultsDestination.GetType().Name}");
            EsasSyncResult syncResult = new EsasSyncResult(); 
            try
            {
                syncResult.SyncStartTime = DateTime.Now;
                syncResult.SyncStrategyName = this._esasEntitiesLoaderStrategy.GetType().Name;
                _syncResultsDestination.PrepareSyncResult(syncResult);

                int numberOfRecordsReadForCurrentIteration = 0;
                int numberOfRecordsToReadPerIteration = 1000;
                int iterationCounter = 0;

                // TODO: foretag sammenligning med loadede id-værdier. Måske indeholder destinationen records som er hard-deleted fra kilden? burde ikke være tilfældet. Men...
                //List<string> loadedIdValues = new List<string>(); // Kan bruges til sammenligning med destinationen - måske indeholder destinationen records som er hard-slettet fra kilden.

                do
                {
                    _logger.LogInformation($"Henter data - {numberOfRecordsToReadPerIteration} objekter fra index {numberOfRecordsToReadPerIteration * iterationCounter}.");

                    // hent N antal objekter per iteration, og send dem kontinuérligt til destinationen.
                    var iterativeLoadResult = _esasEntitiesLoaderStrategy.Load(indexToStartLoadFrom: numberOfRecordsToReadPerIteration * iterationCounter, howManyRecordsToGet: numberOfRecordsToReadPerIteration);
                    if (iterativeLoadResult.esasLoadResult.EsasLoadStatus != EsasOperationResultStatus.OperationSuccesful)
                        throw new Exception($"Iterative load from the web-service reported a non-succesful operation - {iterativeLoadResult.esasLoadResult.EsasLoadStatus}");
                    
                    updateSyncLoadResult(syncResult: ref syncResult, iterativeLoadResult: iterativeLoadResult.esasLoadResult);

                    if (iterativeLoadResult.loadedObjects == null || iterativeLoadResult.loadedObjects.Length == 0)
                    {
                        string noObjectsLoadedMsg = $"No further objects were loaded for loader-strategy {_esasEntitiesLoaderStrategy.GetType().Name}";
                        _logger.LogInformation(noObjectsLoadedMsg);
                        syncResult.esasLoadResult.Message = noObjectsLoadedMsg;
                        break;
                    }

                    var iterativeDeliveryResult = _esasSyncDestination.Deliver(iterativeLoadResult.loadedObjects);
                    if (iterativeDeliveryResult.SendToDestinationStatus != EsasOperationResultStatus.OperationSuccesful)
                        throw new Exception($"Iterative send-to-destination reported a non-succesful operation - {iterativeDeliveryResult.SendToDestinationStatus}");
                    
                    updateSendLoadResult(syncResult: ref syncResult, iterativeDeliveryResult: iterativeDeliveryResult);

                    numberOfRecordsReadForCurrentIteration = iterativeLoadResult.loadedObjects.Length;
                    iterationCounter += 1;
                }
                while (!(numberOfRecordsToReadPerIteration > numberOfRecordsReadForCurrentIteration)); // stop når der ikke længere er flere records at hente.
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                _syncResultsDestination.UpdateResult(syncResult);
                throw ex;
            }

            _logger.LogInformation($"Executing sync-strategy with corresponding load-strategy {this._esasEntitiesLoaderStrategy.GetType().Name} - done!");
            _syncResultsDestination.UpdateResult(syncResult);
        }

        private void updateSendLoadResult(ref EsasSyncResult syncResult, EsasSendResult iterativeDeliveryResult)
        {
            if (syncResult.esasSendResult.SendStartTime == null)
            {
                syncResult.esasSendResult = new EsasSendResult()
                {
                    SendDestinationStrategyName = iterativeDeliveryResult.SendDestinationStrategyName,
                    SendStartTime = iterativeDeliveryResult.SendStartTime,
                    SendEndTime = iterativeDeliveryResult.SendEndTime,
                    SendTimeMs = iterativeDeliveryResult.SendTimeMs,
                    SendToDestinationStatus = iterativeDeliveryResult.SendToDestinationStatus
                };
            }
            else
            {
                syncResult.esasSendResult.SendEndTime = iterativeDeliveryResult.SendEndTime;
                syncResult.esasSendResult.SendTimeMs += iterativeDeliveryResult.SendTimeMs;
                syncResult.esasSendResult.SendToDestinationStatus = iterativeDeliveryResult.SendToDestinationStatus;
            }
        }

        /// <summary>
        /// Opdaterer det samlede sync-load resultat, på baggrund af et del-resultat (fordi vi henter i bidder a N records per iteration).
        /// </summary>
        /// <param name="fullLoadResult">Det samlede sync-load resultat</param>
        /// <param name="iterativeLoadResult"></param>
        private void updateSyncLoadResult(ref EsasSyncResult syncResult, EsasLoadResult iterativeLoadResult)
        {
            if (syncResult.esasLoadResult.LoadStartTime == null)
            {
                syncResult.esasLoadResult = new EsasLoadResult()
                {
                    ModifiedOnDateTimeValue = iterativeLoadResult.ModifiedOnDateTimeValue,
                    EsasLoadStatus = iterativeLoadResult.EsasLoadStatus,
                    LoadEndTime = iterativeLoadResult.LoadEndTime,
                    LoadStartTime = iterativeLoadResult.LoadStartTime,
                    LoaderStrategyName = iterativeLoadResult.LoaderStrategyName,
                    NumberOfObjectsLoaded = iterativeLoadResult.NumberOfObjectsLoaded,
                    LoadTimeMs = iterativeLoadResult.LoadTimeMs,
                    Message = iterativeLoadResult.Message
                };
            }
            else
            {
                syncResult.esasLoadResult.LoadTimeMs += iterativeLoadResult.LoadTimeMs;
                syncResult.esasLoadResult.LoadEndTime = iterativeLoadResult.LoadEndTime;
                syncResult.esasLoadResult.ModifiedOnDateTimeValue = iterativeLoadResult.ModifiedOnDateTimeValue;
                syncResult.esasLoadResult.EsasLoadStatus = iterativeLoadResult.EsasLoadStatus;
                syncResult.esasLoadResult.Message = iterativeLoadResult.Message;
                syncResult.esasLoadResult.NumberOfObjectsLoaded += iterativeLoadResult.NumberOfObjectsLoaded;
            }
        }
    }
}

