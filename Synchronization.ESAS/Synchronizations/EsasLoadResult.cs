using System;
using System.ComponentModel.DataAnnotations;

namespace KP.Synchronization.ESAS.Synchronizations
{
    public class EsasLoadResult
    {
        [Key]
        [Required]
        public int Key { get; set; }

        public EsasLoadResultStatus LoadResultStatus { get; set; }
        public long? LoadTimeMs { get; internal set; }
        public DateTime StartTime { get; internal set; }
        public string Message { get; internal set; }
        public string LoaderStrategyName { get; internal set; }
    }

}

