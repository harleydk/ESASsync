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
        private readonly IEsasSyncDestination _esasSyncDestination;
        private readonly ISyncResultsDestination _syncResultsDestination;
        private readonly ILogger _logger;

        public SyncStrategySettings SyncStrategySettings { get; }

        public IEsasEntitiesLoaderStrategy EsasEntitiesLoaderStrategy { get; }

        public EsasSyncStrategy(SyncStrategySettings syncStrategySettings, IEsasEntitiesLoaderStrategy esasEntitiesLoaderStrategy, IEsasSyncDestination syncDestination, ISyncResultsDestination syncResultsDestination, ILogger logger)
        {
            _syncResultsDestination = syncResultsDestination;
            _logger = logger;
            SyncStrategySettings = syncStrategySettings;
            EsasEntitiesLoaderStrategy = esasEntitiesLoaderStrategy;
            _esasSyncDestination = syncDestination;
        }

        public override string ToString()
        {
            string representation = $"{this.GetType().Name}(entityLoadStrategy={EsasEntitiesLoaderStrategy.GetType().Name}|syncDestination={_esasSyncDestination.GetType().Name}";
            return representation;
        }
        
        public void ExecuteSyncStrategy()
        {
            _logger.LogInformation($"Executing sync-strategy with corresponding load-strategy {this.EsasEntitiesLoaderStrategy.GetType().Name} and destination of {_syncResultsDestination.GetType().Name}");
            EsasSyncResult syncResult = new EsasSyncResult() { SyncStartTimeUTC = DateTime.UtcNow };
            _syncResultsDestination.PrepareSyncResult(syncResult);

            try
            {
                syncResult.SyncStartTimeUTC = DateTime.UtcNow;
                syncResult.SyncStrategyName = this.EsasEntitiesLoaderStrategy.GetType().Name;

                string syncStratMessage = $"Sync-strategy: loading {this.EsasEntitiesLoaderStrategy.GetType().Name} - start";
                _logger.LogInformation(syncStratMessage);

                var esasLoad = EsasEntitiesLoaderStrategy.Load();
                if (esasLoad.esasLoadResult.EsasLoadStatus != EsasOperationResultStatus.OperationSuccesful)
                    throw new Exception($"Iterative load from the web-service reported a non-succesful operation - {esasLoad.esasLoadResult.EsasLoadStatus}");

                syncStratMessage = $"Sync-strategy: loading {this.EsasEntitiesLoaderStrategy.GetType().Name} - end";
                _logger.LogInformation(syncStratMessage);

                updateSyncLoadResult(syncResult: ref syncResult, iterativeLoadResult: esasLoad.esasLoadResult);

                if (esasLoad.loadedObjects == null || esasLoad.loadedObjects.Length == 0)
                {
                    string noObjectsLoadedMsg = $"No further objects were loaded for loader-strategy {EsasEntitiesLoaderStrategy.GetType().Name}";
                    _logger.LogInformation(noObjectsLoadedMsg);
                    syncResult.esasLoadResult.Message = noObjectsLoadedMsg;
                }
                else
                {
                    try
                    {
                        syncStratMessage = $"Sync-strategy: delivering {_esasSyncDestination.GetType().Name} from loaded {this.EsasEntitiesLoaderStrategy.GetType().Name} - start";
                        _logger.LogInformation(syncStratMessage);

                        var deliveryResult = _esasSyncDestination.Deliver(esasLoad.loadedObjects);
                        if (deliveryResult.SendToDestinationStatus != EsasOperationResultStatus.OperationSuccesful)
                            throw new Exception($"send-to-destination reported a non-succesful operation - {deliveryResult.SendToDestinationStatus}");

                        syncStratMessage = $"Sync-strategy: delivering {_esasSyncDestination.GetType().Name} from loaded {this.EsasEntitiesLoaderStrategy.GetType().Name} - end";
                        _logger.LogInformation(syncStratMessage);

                        updateSendLoadResult(syncResult: ref syncResult, iterativeDeliveryResult: deliveryResult);
                    }
                    catch (Exception ex)
                    {
                        // TODO: log and recover
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                _syncResultsDestination.UpdateResult(syncResult);
                throw ex;
            }

            _logger.LogInformation($"Executing sync-strategy with corresponding load-strategy {this.EsasEntitiesLoaderStrategy.GetType().Name} - done!");
            _syncResultsDestination.UpdateResult(syncResult);
            _logger.LogInformation($"Sync result update");
        }

        private void updateSendLoadResult(ref EsasSyncResult syncResult, EsasSendResult iterativeDeliveryResult)
        {
            if (syncResult.esasSendResult.SendStartTimeUTC == null)
            {
                syncResult.esasSendResult = new EsasSendResult()
                {
                    SendDestinationStrategyName = iterativeDeliveryResult.SendDestinationStrategyName,
                    SendStartTimeUTC = iterativeDeliveryResult.SendStartTimeUTC,
                    SendEndTimeUTC = iterativeDeliveryResult.SendEndTimeUTC,
                    SendTimeMs = iterativeDeliveryResult.SendTimeMs,
                    SendToDestinationStatus = iterativeDeliveryResult.SendToDestinationStatus
                };
            }
            else
            {
                syncResult.esasSendResult.SendEndTimeUTC = iterativeDeliveryResult.SendEndTimeUTC;
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
            if (syncResult.esasLoadResult.LoadStartTimeUTC == null)
            {
                syncResult.esasLoadResult = new EsasLoadResult()
                {
                    ModifiedOnDateTimeUTC = iterativeLoadResult.ModifiedOnDateTimeUTC,
                    EsasLoadStatus = iterativeLoadResult.EsasLoadStatus,
                    LoadEndTimeUTC = iterativeLoadResult.LoadEndTimeUTC,
                    LoadStartTimeUTC = iterativeLoadResult.LoadStartTimeUTC,
                    LoaderStrategyName = iterativeLoadResult.LoaderStrategyName,
                    NumberOfObjectsLoaded = iterativeLoadResult.NumberOfObjectsLoaded,
                    LoadTimeMs = iterativeLoadResult.LoadTimeMs,
                    Message = iterativeLoadResult.Message
                };
            }
            else
            {
                syncResult.esasLoadResult.LoadTimeMs += iterativeLoadResult.LoadTimeMs;
                syncResult.esasLoadResult.LoadEndTimeUTC = iterativeLoadResult.LoadEndTimeUTC;
                syncResult.esasLoadResult.ModifiedOnDateTimeUTC = iterativeLoadResult.ModifiedOnDateTimeUTC;
                syncResult.esasLoadResult.EsasLoadStatus = iterativeLoadResult.EsasLoadStatus;
                syncResult.esasLoadResult.Message = iterativeLoadResult.Message;
                syncResult.esasLoadResult.NumberOfObjectsLoaded += iterativeLoadResult.NumberOfObjectsLoaded;
            }
        }
    }
}

