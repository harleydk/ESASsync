namespace Synchronization.ESAS.Models
{
    public class SyncStrategySettings
    {

        /// <summary>
        /// Angiver sync-prioritets-nummeret, dvs. hvornår en strategi afvikles frem for en anden. Dette fordi visse strategier er afhængige af andre, for at
        /// de kan køre.
        /// </summary>
        public int SyncPriorityNumber { get; set; }


        
    }
}
