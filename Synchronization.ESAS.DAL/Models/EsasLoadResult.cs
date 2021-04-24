using System;

namespace Synchronization.ESAS.DAL.Models
{

    public class EsasLoadResult
    {
        public EsasOperationResultStatus EsasLoadStatus { get; set; }
        public long? LoadTimeMs { get; set; }
        public DateTime? LoadStartTime { get; set; }
        public DateTime? LoadEndTime { get; set; }
        public int? NumberOfObjectsLoaded { get; set; }
        public DateTime? ModifiedOnDateTimeValue { get; set; }
        public string LoaderStrategyName { get; set; }
        public string Message { get; set; }
    }
}
