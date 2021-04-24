using System;

namespace Synchronization.ESAS.DAL.Models
{
    public class EsasSendResult
    {
        public EsasOperationResultStatus SendToDestinationStatus { get; set; }
        public long? SendTimeMs { get; set; }
        public DateTime? SendStartTimeUTC { get; set; }
        public DateTime? SendEndTimeUTC { get; set; }
        public string SendDestinationStrategyName { get; set; }
    }
}
