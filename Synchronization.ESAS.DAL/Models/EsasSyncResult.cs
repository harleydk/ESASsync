using System;
using System.ComponentModel.DataAnnotations;

namespace Synchronization.ESAS.DAL.Models
{
    public class EsasSyncResult
    {
        public EsasSyncResult()
        {
            esasLoadResult = new EsasLoadResult();
            esasSendResult = new EsasSendResult();
        }

        [Key]
        [Required]
        public int Id { get; set; }
        public DateTime SyncStartTimeUTC { get; set; }

        public EsasLoadResult esasLoadResult { get; set; }

        public EsasSendResult esasSendResult { get; set; }

        public string Message { get; set; }
        public string SyncStrategyName { get; set; }
    }
}
