using System;

namespace Synchronization.ESAS.DAL.Models
{

    public class EsasLoadResult
    {
        public EsasOperationResultStatus EsasLoadStatus { get; set; }
        public long? LoadTimeMs { get; set; }
        public DateTime? LoadStartTimeUTC { get; set; }
        public DateTime? LoadEndTimeUTC { get; set; }
        public int? NumberOfObjectsLoaded { get; set; }
        public DateTime? ModifiedOnDateTimeUTC { get; set; }
        public string LoaderStrategyName { get; set; }
        public string Message { get; set; }
        
        /// <summary>
        /// Angiver om et kald til ESAS web-servicen indikerer at der er flere records, der er klar til at blive indlæst i.f.t. den sendte request.
        /// </summary>
        public bool HasMoreRecords { get; set; }
    }
}
