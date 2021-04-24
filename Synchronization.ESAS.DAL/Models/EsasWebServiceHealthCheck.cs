using System;
using System.ComponentModel.DataAnnotations;

namespace Synchronization.ESAS.DAL.Models
{
    public class EsasWebServiceHealthCheck
    {
        [Key]
        [Required]
        public int Key { get; set; }

        public DateTime CheckTime { get; set; }

        public string HttpStatusCode { get; set; }

        public string Message { get; set; }

        public long? CheckTimeMs { get; set; }
    }
}
