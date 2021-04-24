using System;

namespace Synchronization.ESAS.DAL.Models
{
    public class EsasSendResult
    {
        public EsasOperationResultStatus SendToDestinationStatus { get; set; }
        public long? SendTimeMs { get; set; }
        public DateTime? SendStartTime { get; set; }
        public DateTime? SendEndTime { get; set; }
        public string SendDestinationStrategyName { get; set; }
    }
}
