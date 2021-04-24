using Default; // Unchase OData connected service 'default' container name
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Diagnostics;
using Newtonsoft.Json;
using esas.Dynamics.Models.Contracts;
using Synchronization.ESAS.DAL;

/*
	Disse klasser er auto-genererede, på basis af deres korresponderende T4-template. 
	Hvis der skal rettes i klasserne, skal rettelserne foregå i template'n!
*/
namespace Synchronization.ESAS.Synchronizations.EntityLoaderStrategies
{
	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class AdgangskravLoadStrategy : BaseLoadStrategy
    {
         public AdgangskravLoadStrategy(EsasWsContextFactory esasWebServiceContextFactory, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasWebServiceContextFactory, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff)
        {
            var esasContainer = _esasWebServiceContextFactory.Create();
            // design continuation here
            List<Adgangskrav> loadedObjectsListe = new List<Adgangskrav>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Adgangskrav> token = null;

            string datetimeoffSetFromLoadtimeCutOff = loadTimeCutoff.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var response = esasContainer.Adgangskrav.AddQueryOption("$filter", $"ModifiedOn gt {datetimeoffSetFromLoadtimeCutOff}").Execute() as Microsoft.OData.Client.QueryOperationResponse<Adgangskrav>;

            loadedObjectsListe.AddRange(response.ToList());
            if ( ! loadedObjectsListe.Any())
                return null;
            
            while ((token = response.GetContinuation()) != null)
            {
                System.Diagnostics.Debug.WriteLine("Continuation-token available, retrieving next batch.");
                esasContainer = _esasWebServiceContextFactory.Create();
                response = esasContainer.Execute<Adgangskrav>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Adgangskrav>;
                loadedObjectsListe.AddRange(response.ToList());
            }

            return loadedObjectsListe.ToArray();
        }

        /// <summary>
        /// This method is only used for initial load of ESAS data - will become useless once we're in production.
        /// </summary>
        protected override object[] LoadObjectsAndPersistToDisk()
        {
            int i = 0; int j = 0;
            var esasContainer = _esasWebServiceContextFactory.Create();

            List<Adgangskrav> loadedObjectsListe = new List<Adgangskrav>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Adgangskrav> token = null;

            var response = esasContainer.Adgangskrav.Execute() as Microsoft.OData.Client.QueryOperationResponse<Adgangskrav>;
            loadedObjectsListe.AddRange(response.ToList());
            if ( loadedObjectsListe.Any())
            {
                j = loadedObjectsListe.Count();
                PersistToFile(i,j,ref loadedObjectsListe);
            }
            else
                return null;
            
            System.Diagnostics.Debug.WriteLine(loadedObjectsListe.Count());
         
            while ((token = response.GetContinuation()) != null)
            {
               Stopwatch st = new Stopwatch();
                st.Start();
                       esasContainer = _esasWebServiceContextFactory.Create();
                List<Adgangskrav> loadedObjectsListen = new List<Adgangskrav>();
                response = esasContainer.Execute<Adgangskrav>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Adgangskrav>;
                loadedObjectsListen.AddRange(response.ToList());
                if ( loadedObjectsListen.Any())
            {
                i = j;
                j = j + loadedObjectsListen.Count();
                PersistToFile(i,j,ref loadedObjectsListen);
                loadedObjectsListen = null;
            }


                // loadedObjectsListe.AddRange(response.ToList()); - only persist to disc, for now.
               st.Stop();
               System.Diagnostics.Debug.WriteLine(j + "-" + st.Elapsed.Seconds);
            }
            
             System.Diagnostics.Debug.WriteLine($"*** total objects written to disk was {j} ***");
            //return loadedObjectsListe.ToArray();
            return null;
        }

        
        private void PersistToFile(int indexToStartLoadFrom, int howManyRecordsToGet, ref List<Adgangskrav> loadedObjectsListe)
        {
             string entityName = "Adgangskrav";
              if (!Directory.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}"))
                Directory.CreateDirectory($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}");

            string objectsJson = JsonConvert.SerializeObject(loadedObjectsListe, Formatting.Indented);
            if ( !File.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json"))
                File.WriteAllText($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json", objectsJson );

        }


    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class AfdelingLoadStrategy : BaseLoadStrategy
    {
         public AfdelingLoadStrategy(EsasWsContextFactory esasWebServiceContextFactory, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasWebServiceContextFactory, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff)
        {
            var esasContainer = _esasWebServiceContextFactory.Create();
            // design continuation here
            List<Afdeling> loadedObjectsListe = new List<Afdeling>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Afdeling> token = null;

            string datetimeoffSetFromLoadtimeCutOff = loadTimeCutoff.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var response = esasContainer.Afdeling.AddQueryOption("$filter", $"ModifiedOn gt {datetimeoffSetFromLoadtimeCutOff}").Execute() as Microsoft.OData.Client.QueryOperationResponse<Afdeling>;

            loadedObjectsListe.AddRange(response.ToList());
            if ( ! loadedObjectsListe.Any())
                return null;
            
            while ((token = response.GetContinuation()) != null)
            {
                System.Diagnostics.Debug.WriteLine("Continuation-token available, retrieving next batch.");
                esasContainer = _esasWebServiceContextFactory.Create();
                response = esasContainer.Execute<Afdeling>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Afdeling>;
                loadedObjectsListe.AddRange(response.ToList());
            }

            return loadedObjectsListe.ToArray();
        }

        /// <summary>
        /// This method is only used for initial load of ESAS data - will become useless once we're in production.
        /// </summary>
        protected override object[] LoadObjectsAndPersistToDisk()
        {
            int i = 0; int j = 0;
            var esasContainer = _esasWebServiceContextFactory.Create();

            List<Afdeling> loadedObjectsListe = new List<Afdeling>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Afdeling> token = null;

            var response = esasContainer.Afdeling.Execute() as Microsoft.OData.Client.QueryOperationResponse<Afdeling>;
            loadedObjectsListe.AddRange(response.ToList());
            if ( loadedObjectsListe.Any())
            {
                j = loadedObjectsListe.Count();
                PersistToFile(i,j,ref loadedObjectsListe);
            }
            else
                return null;
            
            System.Diagnostics.Debug.WriteLine(loadedObjectsListe.Count());
         
            while ((token = response.GetContinuation()) != null)
            {
               Stopwatch st = new Stopwatch();
                st.Start();
                       esasContainer = _esasWebServiceContextFactory.Create();
                List<Afdeling> loadedObjectsListen = new List<Afdeling>();
                response = esasContainer.Execute<Afdeling>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Afdeling>;
                loadedObjectsListen.AddRange(response.ToList());
                if ( loadedObjectsListen.Any())
            {
                i = j;
                j = j + loadedObjectsListen.Count();
                PersistToFile(i,j,ref loadedObjectsListen);
                loadedObjectsListen = null;
            }


                // loadedObjectsListe.AddRange(response.ToList()); - only persist to disc, for now.
               st.Stop();
               System.Diagnostics.Debug.WriteLine(j + "-" + st.Elapsed.Seconds);
            }
            
             System.Diagnostics.Debug.WriteLine($"*** total objects written to disk was {j} ***");
            //return loadedObjectsListe.ToArray();
            return null;
        }

        
        private void PersistToFile(int indexToStartLoadFrom, int howManyRecordsToGet, ref List<Afdeling> loadedObjectsListe)
        {
             string entityName = "Afdeling";
              if (!Directory.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}"))
                Directory.CreateDirectory($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}");

            string objectsJson = JsonConvert.SerializeObject(loadedObjectsListe, Formatting.Indented);
            if ( !File.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json"))
                File.WriteAllText($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json", objectsJson );

        }


    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class AfdelingsniveauLoadStrategy : BaseLoadStrategy
    {
         public AfdelingsniveauLoadStrategy(EsasWsContextFactory esasWebServiceContextFactory, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasWebServiceContextFactory, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff)
        {
            var esasContainer = _esasWebServiceContextFactory.Create();
            // design continuation here
            List<Afdelingsniveau> loadedObjectsListe = new List<Afdelingsniveau>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Afdelingsniveau> token = null;

            string datetimeoffSetFromLoadtimeCutOff = loadTimeCutoff.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var response = esasContainer.Afdelingsniveau.AddQueryOption("$filter", $"ModifiedOn gt {datetimeoffSetFromLoadtimeCutOff}").Execute() as Microsoft.OData.Client.QueryOperationResponse<Afdelingsniveau>;

            loadedObjectsListe.AddRange(response.ToList());
            if ( ! loadedObjectsListe.Any())
                return null;
            
            while ((token = response.GetContinuation()) != null)
            {
                System.Diagnostics.Debug.WriteLine("Continuation-token available, retrieving next batch.");
                esasContainer = _esasWebServiceContextFactory.Create();
                response = esasContainer.Execute<Afdelingsniveau>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Afdelingsniveau>;
                loadedObjectsListe.AddRange(response.ToList());
            }

            return loadedObjectsListe.ToArray();
        }

        /// <summary>
        /// This method is only used for initial load of ESAS data - will become useless once we're in production.
        /// </summary>
        protected override object[] LoadObjectsAndPersistToDisk()
        {
            int i = 0; int j = 0;
            var esasContainer = _esasWebServiceContextFactory.Create();

            List<Afdelingsniveau> loadedObjectsListe = new List<Afdelingsniveau>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Afdelingsniveau> token = null;

            var response = esasContainer.Afdelingsniveau.Execute() as Microsoft.OData.Client.QueryOperationResponse<Afdelingsniveau>;
            loadedObjectsListe.AddRange(response.ToList());
            if ( loadedObjectsListe.Any())
            {
                j = loadedObjectsListe.Count();
                PersistToFile(i,j,ref loadedObjectsListe);
            }
            else
                return null;
            
            System.Diagnostics.Debug.WriteLine(loadedObjectsListe.Count());
         
            while ((token = response.GetContinuation()) != null)
            {
               Stopwatch st = new Stopwatch();
                st.Start();
                       esasContainer = _esasWebServiceContextFactory.Create();
                List<Afdelingsniveau> loadedObjectsListen = new List<Afdelingsniveau>();
                response = esasContainer.Execute<Afdelingsniveau>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Afdelingsniveau>;
                loadedObjectsListen.AddRange(response.ToList());
                if ( loadedObjectsListen.Any())
            {
                i = j;
                j = j + loadedObjectsListen.Count();
                PersistToFile(i,j,ref loadedObjectsListen);
                loadedObjectsListen = null;
            }


                // loadedObjectsListe.AddRange(response.ToList()); - only persist to disc, for now.
               st.Stop();
               System.Diagnostics.Debug.WriteLine(j + "-" + st.Elapsed.Seconds);
            }
            
             System.Diagnostics.Debug.WriteLine($"*** total objects written to disk was {j} ***");
            //return loadedObjectsListe.ToArray();
            return null;
        }

        
        private void PersistToFile(int indexToStartLoadFrom, int howManyRecordsToGet, ref List<Afdelingsniveau> loadedObjectsListe)
        {
             string entityName = "Afdelingsniveau";
              if (!Directory.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}"))
                Directory.CreateDirectory($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}");

            string objectsJson = JsonConvert.SerializeObject(loadedObjectsListe, Formatting.Indented);
            if ( !File.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json"))
                File.WriteAllText($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json", objectsJson );

        }


    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class AfslagsbegrundelseLoadStrategy : BaseLoadStrategy
    {
         public AfslagsbegrundelseLoadStrategy(EsasWsContextFactory esasWebServiceContextFactory, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasWebServiceContextFactory, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff)
        {
            var esasContainer = _esasWebServiceContextFactory.Create();
            // design continuation here
            List<Afslagsbegrundelse> loadedObjectsListe = new List<Afslagsbegrundelse>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Afslagsbegrundelse> token = null;

            string datetimeoffSetFromLoadtimeCutOff = loadTimeCutoff.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var response = esasContainer.Afslagsbegrundelse.AddQueryOption("$filter", $"ModifiedOn gt {datetimeoffSetFromLoadtimeCutOff}").Execute() as Microsoft.OData.Client.QueryOperationResponse<Afslagsbegrundelse>;

            loadedObjectsListe.AddRange(response.ToList());
            if ( ! loadedObjectsListe.Any())
                return null;
            
            while ((token = response.GetContinuation()) != null)
            {
                System.Diagnostics.Debug.WriteLine("Continuation-token available, retrieving next batch.");
                esasContainer = _esasWebServiceContextFactory.Create();
                response = esasContainer.Execute<Afslagsbegrundelse>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Afslagsbegrundelse>;
                loadedObjectsListe.AddRange(response.ToList());
            }

            return loadedObjectsListe.ToArray();
        }

        /// <summary>
        /// This method is only used for initial load of ESAS data - will become useless once we're in production.
        /// </summary>
        protected override object[] LoadObjectsAndPersistToDisk()
        {
            int i = 0; int j = 0;
            var esasContainer = _esasWebServiceContextFactory.Create();

            List<Afslagsbegrundelse> loadedObjectsListe = new List<Afslagsbegrundelse>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Afslagsbegrundelse> token = null;

            var response = esasContainer.Afslagsbegrundelse.Execute() as Microsoft.OData.Client.QueryOperationResponse<Afslagsbegrundelse>;
            loadedObjectsListe.AddRange(response.ToList());
            if ( loadedObjectsListe.Any())
            {
                j = loadedObjectsListe.Count();
                PersistToFile(i,j,ref loadedObjectsListe);
            }
            else
                return null;
            
            System.Diagnostics.Debug.WriteLine(loadedObjectsListe.Count());
         
            while ((token = response.GetContinuation()) != null)
            {
               Stopwatch st = new Stopwatch();
                st.Start();
                       esasContainer = _esasWebServiceContextFactory.Create();
                List<Afslagsbegrundelse> loadedObjectsListen = new List<Afslagsbegrundelse>();
                response = esasContainer.Execute<Afslagsbegrundelse>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Afslagsbegrundelse>;
                loadedObjectsListen.AddRange(response.ToList());
                if ( loadedObjectsListen.Any())
            {
                i = j;
                j = j + loadedObjectsListen.Count();
                PersistToFile(i,j,ref loadedObjectsListen);
                loadedObjectsListen = null;
            }


                // loadedObjectsListe.AddRange(response.ToList()); - only persist to disc, for now.
               st.Stop();
               System.Diagnostics.Debug.WriteLine(j + "-" + st.Elapsed.Seconds);
            }
            
             System.Diagnostics.Debug.WriteLine($"*** total objects written to disk was {j} ***");
            //return loadedObjectsListe.ToArray();
            return null;
        }

        
        private void PersistToFile(int indexToStartLoadFrom, int howManyRecordsToGet, ref List<Afslagsbegrundelse> loadedObjectsListe)
        {
             string entityName = "Afslagsbegrundelse";
              if (!Directory.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}"))
                Directory.CreateDirectory($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}");

            string objectsJson = JsonConvert.SerializeObject(loadedObjectsListe, Formatting.Indented);
            if ( !File.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json"))
                File.WriteAllText($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json", objectsJson );

        }


    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class AktivitetsudbudLoadStrategy : BaseLoadStrategy
    {
         public AktivitetsudbudLoadStrategy(EsasWsContextFactory esasWebServiceContextFactory, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasWebServiceContextFactory, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff)
        {
            var esasContainer = _esasWebServiceContextFactory.Create();
            // design continuation here
            List<Aktivitetsudbud> loadedObjectsListe = new List<Aktivitetsudbud>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Aktivitetsudbud> token = null;

            string datetimeoffSetFromLoadtimeCutOff = loadTimeCutoff.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var response = esasContainer.Aktivitetsudbud.AddQueryOption("$filter", $"ModifiedOn gt {datetimeoffSetFromLoadtimeCutOff}").Execute() as Microsoft.OData.Client.QueryOperationResponse<Aktivitetsudbud>;

            loadedObjectsListe.AddRange(response.ToList());
            if ( ! loadedObjectsListe.Any())
                return null;
            
            while ((token = response.GetContinuation()) != null)
            {
                System.Diagnostics.Debug.WriteLine("Continuation-token available, retrieving next batch.");
                esasContainer = _esasWebServiceContextFactory.Create();
                response = esasContainer.Execute<Aktivitetsudbud>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Aktivitetsudbud>;
                loadedObjectsListe.AddRange(response.ToList());
            }

            return loadedObjectsListe.ToArray();
        }

        /// <summary>
        /// This method is only used for initial load of ESAS data - will become useless once we're in production.
        /// </summary>
        protected override object[] LoadObjectsAndPersistToDisk()
        {
            int i = 0; int j = 0;
            var esasContainer = _esasWebServiceContextFactory.Create();

            List<Aktivitetsudbud> loadedObjectsListe = new List<Aktivitetsudbud>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Aktivitetsudbud> token = null;

            var response = esasContainer.Aktivitetsudbud.Execute() as Microsoft.OData.Client.QueryOperationResponse<Aktivitetsudbud>;
            loadedObjectsListe.AddRange(response.ToList());
            if ( loadedObjectsListe.Any())
            {
                j = loadedObjectsListe.Count();
                PersistToFile(i,j,ref loadedObjectsListe);
            }
            else
                return null;
            
            System.Diagnostics.Debug.WriteLine(loadedObjectsListe.Count());
         
            while ((token = response.GetContinuation()) != null)
            {
               Stopwatch st = new Stopwatch();
                st.Start();
                       esasContainer = _esasWebServiceContextFactory.Create();
                List<Aktivitetsudbud> loadedObjectsListen = new List<Aktivitetsudbud>();
                response = esasContainer.Execute<Aktivitetsudbud>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Aktivitetsudbud>;
                loadedObjectsListen.AddRange(response.ToList());
                if ( loadedObjectsListen.Any())
            {
                i = j;
                j = j + loadedObjectsListen.Count();
                PersistToFile(i,j,ref loadedObjectsListen);
                loadedObjectsListen = null;
            }


                // loadedObjectsListe.AddRange(response.ToList()); - only persist to disc, for now.
               st.Stop();
               System.Diagnostics.Debug.WriteLine(j + "-" + st.Elapsed.Seconds);
            }
            
             System.Diagnostics.Debug.WriteLine($"*** total objects written to disk was {j} ***");
            //return loadedObjectsListe.ToArray();
            return null;
        }

        
        private void PersistToFile(int indexToStartLoadFrom, int howManyRecordsToGet, ref List<Aktivitetsudbud> loadedObjectsListe)
        {
             string entityName = "Aktivitetsudbud";
              if (!Directory.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}"))
                Directory.CreateDirectory($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}");

            string objectsJson = JsonConvert.SerializeObject(loadedObjectsListe, Formatting.Indented);
            if ( !File.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json"))
                File.WriteAllText($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json", objectsJson );

        }


    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class AndenAktivitetLoadStrategy : BaseLoadStrategy
    {
         public AndenAktivitetLoadStrategy(EsasWsContextFactory esasWebServiceContextFactory, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasWebServiceContextFactory, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff)
        {
            var esasContainer = _esasWebServiceContextFactory.Create();
            // design continuation here
            List<AndenAktivitet> loadedObjectsListe = new List<AndenAktivitet>();
            Microsoft.OData.Client.DataServiceQueryContinuation<AndenAktivitet> token = null;

            string datetimeoffSetFromLoadtimeCutOff = loadTimeCutoff.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var response = esasContainer.AndenAktivitet.AddQueryOption("$filter", $"ModifiedOn gt {datetimeoffSetFromLoadtimeCutOff}").Execute() as Microsoft.OData.Client.QueryOperationResponse<AndenAktivitet>;

            loadedObjectsListe.AddRange(response.ToList());
            if ( ! loadedObjectsListe.Any())
                return null;
            
            while ((token = response.GetContinuation()) != null)
            {
                System.Diagnostics.Debug.WriteLine("Continuation-token available, retrieving next batch.");
                esasContainer = _esasWebServiceContextFactory.Create();
                response = esasContainer.Execute<AndenAktivitet>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<AndenAktivitet>;
                loadedObjectsListe.AddRange(response.ToList());
            }

            return loadedObjectsListe.ToArray();
        }

        /// <summary>
        /// This method is only used for initial load of ESAS data - will become useless once we're in production.
        /// </summary>
        protected override object[] LoadObjectsAndPersistToDisk()
        {
            int i = 0; int j = 0;
            var esasContainer = _esasWebServiceContextFactory.Create();

            List<AndenAktivitet> loadedObjectsListe = new List<AndenAktivitet>();
            Microsoft.OData.Client.DataServiceQueryContinuation<AndenAktivitet> token = null;

            var response = esasContainer.AndenAktivitet.Execute() as Microsoft.OData.Client.QueryOperationResponse<AndenAktivitet>;
            loadedObjectsListe.AddRange(response.ToList());
            if ( loadedObjectsListe.Any())
            {
                j = loadedObjectsListe.Count();
                PersistToFile(i,j,ref loadedObjectsListe);
            }
            else
                return null;
            
            System.Diagnostics.Debug.WriteLine(loadedObjectsListe.Count());
         
            while ((token = response.GetContinuation()) != null)
            {
               Stopwatch st = new Stopwatch();
                st.Start();
                       esasContainer = _esasWebServiceContextFactory.Create();
                List<AndenAktivitet> loadedObjectsListen = new List<AndenAktivitet>();
                response = esasContainer.Execute<AndenAktivitet>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<AndenAktivitet>;
                loadedObjectsListen.AddRange(response.ToList());
                if ( loadedObjectsListen.Any())
            {
                i = j;
                j = j + loadedObjectsListen.Count();
                PersistToFile(i,j,ref loadedObjectsListen);
                loadedObjectsListen = null;
            }


                // loadedObjectsListe.AddRange(response.ToList()); - only persist to disc, for now.
               st.Stop();
               System.Diagnostics.Debug.WriteLine(j + "-" + st.Elapsed.Seconds);
            }
            
             System.Diagnostics.Debug.WriteLine($"*** total objects written to disk was {j} ***");
            //return loadedObjectsListe.ToArray();
            return null;
        }

        
        private void PersistToFile(int indexToStartLoadFrom, int howManyRecordsToGet, ref List<AndenAktivitet> loadedObjectsListe)
        {
             string entityName = "AndenAktivitet";
              if (!Directory.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}"))
                Directory.CreateDirectory($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}");

            string objectsJson = JsonConvert.SerializeObject(loadedObjectsListe, Formatting.Indented);
            if ( !File.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json"))
                File.WriteAllText($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json", objectsJson );

        }


    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class AnsoegerLoadStrategy : BaseLoadStrategy
    {
         public AnsoegerLoadStrategy(EsasWsContextFactory esasWebServiceContextFactory, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasWebServiceContextFactory, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff)
        {
            var esasContainer = _esasWebServiceContextFactory.Create();
            // design continuation here
            List<Ansoeger> loadedObjectsListe = new List<Ansoeger>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Ansoeger> token = null;

            string datetimeoffSetFromLoadtimeCutOff = loadTimeCutoff.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var response = esasContainer.Ansoeger.AddQueryOption("$filter", $"ModifiedOn gt {datetimeoffSetFromLoadtimeCutOff}").Execute() as Microsoft.OData.Client.QueryOperationResponse<Ansoeger>;

            loadedObjectsListe.AddRange(response.ToList());
            if ( ! loadedObjectsListe.Any())
                return null;
            
            while ((token = response.GetContinuation()) != null)
            {
                System.Diagnostics.Debug.WriteLine("Continuation-token available, retrieving next batch.");
                esasContainer = _esasWebServiceContextFactory.Create();
                response = esasContainer.Execute<Ansoeger>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Ansoeger>;
                loadedObjectsListe.AddRange(response.ToList());
            }

            return loadedObjectsListe.ToArray();
        }

        /// <summary>
        /// This method is only used for initial load of ESAS data - will become useless once we're in production.
        /// </summary>
        protected override object[] LoadObjectsAndPersistToDisk()
        {
            int i = 0; int j = 0;
            var esasContainer = _esasWebServiceContextFactory.Create();

            List<Ansoeger> loadedObjectsListe = new List<Ansoeger>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Ansoeger> token = null;

            var response = esasContainer.Ansoeger.Execute() as Microsoft.OData.Client.QueryOperationResponse<Ansoeger>;
            loadedObjectsListe.AddRange(response.ToList());
            if ( loadedObjectsListe.Any())
            {
                j = loadedObjectsListe.Count();
                PersistToFile(i,j,ref loadedObjectsListe);
            }
            else
                return null;
            
            System.Diagnostics.Debug.WriteLine(loadedObjectsListe.Count());
         
            while ((token = response.GetContinuation()) != null)
            {
               Stopwatch st = new Stopwatch();
                st.Start();
                       esasContainer = _esasWebServiceContextFactory.Create();
                List<Ansoeger> loadedObjectsListen = new List<Ansoeger>();
                response = esasContainer.Execute<Ansoeger>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Ansoeger>;
                loadedObjectsListen.AddRange(response.ToList());
                if ( loadedObjectsListen.Any())
            {
                i = j;
                j = j + loadedObjectsListen.Count();
                PersistToFile(i,j,ref loadedObjectsListen);
                loadedObjectsListen = null;
            }


                // loadedObjectsListe.AddRange(response.ToList()); - only persist to disc, for now.
               st.Stop();
               System.Diagnostics.Debug.WriteLine(j + "-" + st.Elapsed.Seconds);
            }
            
             System.Diagnostics.Debug.WriteLine($"*** total objects written to disk was {j} ***");
            //return loadedObjectsListe.ToArray();
            return null;
        }

        
        private void PersistToFile(int indexToStartLoadFrom, int howManyRecordsToGet, ref List<Ansoeger> loadedObjectsListe)
        {
             string entityName = "Ansoeger";
              if (!Directory.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}"))
                Directory.CreateDirectory($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}");

            string objectsJson = JsonConvert.SerializeObject(loadedObjectsListe, Formatting.Indented);
            if ( !File.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json"))
                File.WriteAllText($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json", objectsJson );

        }


    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class AnsoegningLoadStrategy : BaseLoadStrategy
    {
         public AnsoegningLoadStrategy(EsasWsContextFactory esasWebServiceContextFactory, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasWebServiceContextFactory, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff)
        {
            var esasContainer = _esasWebServiceContextFactory.Create();
            // design continuation here
            List<Ansoegning> loadedObjectsListe = new List<Ansoegning>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Ansoegning> token = null;

            string datetimeoffSetFromLoadtimeCutOff = loadTimeCutoff.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var response = esasContainer.Ansoegning.AddQueryOption("$filter", $"ModifiedOn gt {datetimeoffSetFromLoadtimeCutOff}").Execute() as Microsoft.OData.Client.QueryOperationResponse<Ansoegning>;

            loadedObjectsListe.AddRange(response.ToList());
            if ( ! loadedObjectsListe.Any())
                return null;
            
            while ((token = response.GetContinuation()) != null)
            {
                System.Diagnostics.Debug.WriteLine("Continuation-token available, retrieving next batch.");
                esasContainer = _esasWebServiceContextFactory.Create();
                response = esasContainer.Execute<Ansoegning>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Ansoegning>;
                loadedObjectsListe.AddRange(response.ToList());
            }

            return loadedObjectsListe.ToArray();
        }

        /// <summary>
        /// This method is only used for initial load of ESAS data - will become useless once we're in production.
        /// </summary>
        protected override object[] LoadObjectsAndPersistToDisk()
        {
            int i = 0; int j = 0;
            var esasContainer = _esasWebServiceContextFactory.Create();

            List<Ansoegning> loadedObjectsListe = new List<Ansoegning>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Ansoegning> token = null;

            var response = esasContainer.Ansoegning.Execute() as Microsoft.OData.Client.QueryOperationResponse<Ansoegning>;
            loadedObjectsListe.AddRange(response.ToList());
            if ( loadedObjectsListe.Any())
            {
                j = loadedObjectsListe.Count();
                PersistToFile(i,j,ref loadedObjectsListe);
            }
            else
                return null;
            
            System.Diagnostics.Debug.WriteLine(loadedObjectsListe.Count());
         
            while ((token = response.GetContinuation()) != null)
            {
               Stopwatch st = new Stopwatch();
                st.Start();
                       esasContainer = _esasWebServiceContextFactory.Create();
                List<Ansoegning> loadedObjectsListen = new List<Ansoegning>();
                response = esasContainer.Execute<Ansoegning>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Ansoegning>;
                loadedObjectsListen.AddRange(response.ToList());
                if ( loadedObjectsListen.Any())
            {
                i = j;
                j = j + loadedObjectsListen.Count();
                PersistToFile(i,j,ref loadedObjectsListen);
                loadedObjectsListen = null;
            }


                // loadedObjectsListe.AddRange(response.ToList()); - only persist to disc, for now.
               st.Stop();
               System.Diagnostics.Debug.WriteLine(j + "-" + st.Elapsed.Seconds);
            }
            
             System.Diagnostics.Debug.WriteLine($"*** total objects written to disk was {j} ***");
            //return loadedObjectsListe.ToArray();
            return null;
        }

        
        private void PersistToFile(int indexToStartLoadFrom, int howManyRecordsToGet, ref List<Ansoegning> loadedObjectsListe)
        {
             string entityName = "Ansoegning";
              if (!Directory.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}"))
                Directory.CreateDirectory($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}");

            string objectsJson = JsonConvert.SerializeObject(loadedObjectsListe, Formatting.Indented);
            if ( !File.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json"))
                File.WriteAllText($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json", objectsJson );

        }


    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class AnsoegningshandlingLoadStrategy : BaseLoadStrategy
    {
         public AnsoegningshandlingLoadStrategy(EsasWsContextFactory esasWebServiceContextFactory, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasWebServiceContextFactory, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff)
        {
            var esasContainer = _esasWebServiceContextFactory.Create();
            // design continuation here
            List<Ansoegningshandling> loadedObjectsListe = new List<Ansoegningshandling>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Ansoegningshandling> token = null;

            string datetimeoffSetFromLoadtimeCutOff = loadTimeCutoff.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var response = esasContainer.Ansoegningshandling.AddQueryOption("$filter", $"ModifiedOn gt {datetimeoffSetFromLoadtimeCutOff}").Execute() as Microsoft.OData.Client.QueryOperationResponse<Ansoegningshandling>;

            loadedObjectsListe.AddRange(response.ToList());
            if ( ! loadedObjectsListe.Any())
                return null;
            
            while ((token = response.GetContinuation()) != null)
            {
                System.Diagnostics.Debug.WriteLine("Continuation-token available, retrieving next batch.");
                esasContainer = _esasWebServiceContextFactory.Create();
                response = esasContainer.Execute<Ansoegningshandling>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Ansoegningshandling>;
                loadedObjectsListe.AddRange(response.ToList());
            }

            return loadedObjectsListe.ToArray();
        }

        /// <summary>
        /// This method is only used for initial load of ESAS data - will become useless once we're in production.
        /// </summary>
        protected override object[] LoadObjectsAndPersistToDisk()
        {
            int i = 0; int j = 0;
            var esasContainer = _esasWebServiceContextFactory.Create();

            List<Ansoegningshandling> loadedObjectsListe = new List<Ansoegningshandling>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Ansoegningshandling> token = null;

            var response = esasContainer.Ansoegningshandling.Execute() as Microsoft.OData.Client.QueryOperationResponse<Ansoegningshandling>;
            loadedObjectsListe.AddRange(response.ToList());
            if ( loadedObjectsListe.Any())
            {
                j = loadedObjectsListe.Count();
                PersistToFile(i,j,ref loadedObjectsListe);
            }
            else
                return null;
            
            System.Diagnostics.Debug.WriteLine(loadedObjectsListe.Count());
         
            while ((token = response.GetContinuation()) != null)
            {
               Stopwatch st = new Stopwatch();
                st.Start();
                       esasContainer = _esasWebServiceContextFactory.Create();
                List<Ansoegningshandling> loadedObjectsListen = new List<Ansoegningshandling>();
                response = esasContainer.Execute<Ansoegningshandling>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Ansoegningshandling>;
                loadedObjectsListen.AddRange(response.ToList());
                if ( loadedObjectsListen.Any())
            {
                i = j;
                j = j + loadedObjectsListen.Count();
                PersistToFile(i,j,ref loadedObjectsListen);
                loadedObjectsListen = null;
            }


                // loadedObjectsListe.AddRange(response.ToList()); - only persist to disc, for now.
               st.Stop();
               System.Diagnostics.Debug.WriteLine(j + "-" + st.Elapsed.Seconds);
            }
            
             System.Diagnostics.Debug.WriteLine($"*** total objects written to disk was {j} ***");
            //return loadedObjectsListe.ToArray();
            return null;
        }

        
        private void PersistToFile(int indexToStartLoadFrom, int howManyRecordsToGet, ref List<Ansoegningshandling> loadedObjectsListe)
        {
             string entityName = "Ansoegningshandling";
              if (!Directory.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}"))
                Directory.CreateDirectory($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}");

            string objectsJson = JsonConvert.SerializeObject(loadedObjectsListe, Formatting.Indented);
            if ( !File.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json"))
                File.WriteAllText($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json", objectsJson );

        }


    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class AnsoegningskortLoadStrategy : BaseLoadStrategy
    {
         public AnsoegningskortLoadStrategy(EsasWsContextFactory esasWebServiceContextFactory, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasWebServiceContextFactory, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff)
        {
            var esasContainer = _esasWebServiceContextFactory.Create();
            // design continuation here
            List<Ansoegningskort> loadedObjectsListe = new List<Ansoegningskort>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Ansoegningskort> token = null;

            string datetimeoffSetFromLoadtimeCutOff = loadTimeCutoff.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var response = esasContainer.Ansoegningskort.AddQueryOption("$filter", $"ModifiedOn gt {datetimeoffSetFromLoadtimeCutOff}").Execute() as Microsoft.OData.Client.QueryOperationResponse<Ansoegningskort>;

            loadedObjectsListe.AddRange(response.ToList());
            if ( ! loadedObjectsListe.Any())
                return null;
            
            while ((token = response.GetContinuation()) != null)
            {
                System.Diagnostics.Debug.WriteLine("Continuation-token available, retrieving next batch.");
                esasContainer = _esasWebServiceContextFactory.Create();
                response = esasContainer.Execute<Ansoegningskort>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Ansoegningskort>;
                loadedObjectsListe.AddRange(response.ToList());
            }

            return loadedObjectsListe.ToArray();
        }

        /// <summary>
        /// This method is only used for initial load of ESAS data - will become useless once we're in production.
        /// </summary>
        protected override object[] LoadObjectsAndPersistToDisk()
        {
            int i = 0; int j = 0;
            var esasContainer = _esasWebServiceContextFactory.Create();

            List<Ansoegningskort> loadedObjectsListe = new List<Ansoegningskort>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Ansoegningskort> token = null;

            var response = esasContainer.Ansoegningskort.Execute() as Microsoft.OData.Client.QueryOperationResponse<Ansoegningskort>;
            loadedObjectsListe.AddRange(response.ToList());
            if ( loadedObjectsListe.Any())
            {
                j = loadedObjectsListe.Count();
                PersistToFile(i,j,ref loadedObjectsListe);
            }
            else
                return null;
            
            System.Diagnostics.Debug.WriteLine(loadedObjectsListe.Count());
         
            while ((token = response.GetContinuation()) != null)
            {
               Stopwatch st = new Stopwatch();
                st.Start();
                       esasContainer = _esasWebServiceContextFactory.Create();
                List<Ansoegningskort> loadedObjectsListen = new List<Ansoegningskort>();
                response = esasContainer.Execute<Ansoegningskort>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Ansoegningskort>;
                loadedObjectsListen.AddRange(response.ToList());
                if ( loadedObjectsListen.Any())
            {
                i = j;
                j = j + loadedObjectsListen.Count();
                PersistToFile(i,j,ref loadedObjectsListen);
                loadedObjectsListen = null;
            }


                // loadedObjectsListe.AddRange(response.ToList()); - only persist to disc, for now.
               st.Stop();
               System.Diagnostics.Debug.WriteLine(j + "-" + st.Elapsed.Seconds);
            }
            
             System.Diagnostics.Debug.WriteLine($"*** total objects written to disk was {j} ***");
            //return loadedObjectsListe.ToArray();
            return null;
        }

        
        private void PersistToFile(int indexToStartLoadFrom, int howManyRecordsToGet, ref List<Ansoegningskort> loadedObjectsListe)
        {
             string entityName = "Ansoegningskort";
              if (!Directory.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}"))
                Directory.CreateDirectory($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}");

            string objectsJson = JsonConvert.SerializeObject(loadedObjectsListe, Formatting.Indented);
            if ( !File.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json"))
                File.WriteAllText($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json", objectsJson );

        }


    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class AnsoegningskortOpsaetningLoadStrategy : BaseLoadStrategy
    {
         public AnsoegningskortOpsaetningLoadStrategy(EsasWsContextFactory esasWebServiceContextFactory, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasWebServiceContextFactory, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff)
        {
            var esasContainer = _esasWebServiceContextFactory.Create();
            // design continuation here
            List<AnsoegningskortOpsaetning> loadedObjectsListe = new List<AnsoegningskortOpsaetning>();
            Microsoft.OData.Client.DataServiceQueryContinuation<AnsoegningskortOpsaetning> token = null;

            string datetimeoffSetFromLoadtimeCutOff = loadTimeCutoff.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var response = esasContainer.AnsoegningskortOpsaetning.AddQueryOption("$filter", $"ModifiedOn gt {datetimeoffSetFromLoadtimeCutOff}").Execute() as Microsoft.OData.Client.QueryOperationResponse<AnsoegningskortOpsaetning>;

            loadedObjectsListe.AddRange(response.ToList());
            if ( ! loadedObjectsListe.Any())
                return null;
            
            while ((token = response.GetContinuation()) != null)
            {
                System.Diagnostics.Debug.WriteLine("Continuation-token available, retrieving next batch.");
                esasContainer = _esasWebServiceContextFactory.Create();
                response = esasContainer.Execute<AnsoegningskortOpsaetning>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<AnsoegningskortOpsaetning>;
                loadedObjectsListe.AddRange(response.ToList());
            }

            return loadedObjectsListe.ToArray();
        }

        /// <summary>
        /// This method is only used for initial load of ESAS data - will become useless once we're in production.
        /// </summary>
        protected override object[] LoadObjectsAndPersistToDisk()
        {
            int i = 0; int j = 0;
            var esasContainer = _esasWebServiceContextFactory.Create();

            List<AnsoegningskortOpsaetning> loadedObjectsListe = new List<AnsoegningskortOpsaetning>();
            Microsoft.OData.Client.DataServiceQueryContinuation<AnsoegningskortOpsaetning> token = null;

            var response = esasContainer.AnsoegningskortOpsaetning.Execute() as Microsoft.OData.Client.QueryOperationResponse<AnsoegningskortOpsaetning>;
            loadedObjectsListe.AddRange(response.ToList());
            if ( loadedObjectsListe.Any())
            {
                j = loadedObjectsListe.Count();
                PersistToFile(i,j,ref loadedObjectsListe);
            }
            else
                return null;
            
            System.Diagnostics.Debug.WriteLine(loadedObjectsListe.Count());
         
            while ((token = response.GetContinuation()) != null)
            {
               Stopwatch st = new Stopwatch();
                st.Start();
                       esasContainer = _esasWebServiceContextFactory.Create();
                List<AnsoegningskortOpsaetning> loadedObjectsListen = new List<AnsoegningskortOpsaetning>();
                response = esasContainer.Execute<AnsoegningskortOpsaetning>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<AnsoegningskortOpsaetning>;
                loadedObjectsListen.AddRange(response.ToList());
                if ( loadedObjectsListen.Any())
            {
                i = j;
                j = j + loadedObjectsListen.Count();
                PersistToFile(i,j,ref loadedObjectsListen);
                loadedObjectsListen = null;
            }


                // loadedObjectsListe.AddRange(response.ToList()); - only persist to disc, for now.
               st.Stop();
               System.Diagnostics.Debug.WriteLine(j + "-" + st.Elapsed.Seconds);
            }
            
             System.Diagnostics.Debug.WriteLine($"*** total objects written to disk was {j} ***");
            //return loadedObjectsListe.ToArray();
            return null;
        }

        
        private void PersistToFile(int indexToStartLoadFrom, int howManyRecordsToGet, ref List<AnsoegningskortOpsaetning> loadedObjectsListe)
        {
             string entityName = "AnsoegningskortOpsaetning";
              if (!Directory.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}"))
                Directory.CreateDirectory($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}");

            string objectsJson = JsonConvert.SerializeObject(loadedObjectsListe, Formatting.Indented);
            if ( !File.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json"))
                File.WriteAllText($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json", objectsJson );

        }


    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class AnsoegningskortTekstLoadStrategy : BaseLoadStrategy
    {
         public AnsoegningskortTekstLoadStrategy(EsasWsContextFactory esasWebServiceContextFactory, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasWebServiceContextFactory, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff)
        {
            var esasContainer = _esasWebServiceContextFactory.Create();
            // design continuation here
            List<AnsoegningskortTekst> loadedObjectsListe = new List<AnsoegningskortTekst>();
            Microsoft.OData.Client.DataServiceQueryContinuation<AnsoegningskortTekst> token = null;

            string datetimeoffSetFromLoadtimeCutOff = loadTimeCutoff.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var response = esasContainer.AnsoegningskortTekst.AddQueryOption("$filter", $"ModifiedOn gt {datetimeoffSetFromLoadtimeCutOff}").Execute() as Microsoft.OData.Client.QueryOperationResponse<AnsoegningskortTekst>;

            loadedObjectsListe.AddRange(response.ToList());
            if ( ! loadedObjectsListe.Any())
                return null;
            
            while ((token = response.GetContinuation()) != null)
            {
                System.Diagnostics.Debug.WriteLine("Continuation-token available, retrieving next batch.");
                esasContainer = _esasWebServiceContextFactory.Create();
                response = esasContainer.Execute<AnsoegningskortTekst>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<AnsoegningskortTekst>;
                loadedObjectsListe.AddRange(response.ToList());
            }

            return loadedObjectsListe.ToArray();
        }

        /// <summary>
        /// This method is only used for initial load of ESAS data - will become useless once we're in production.
        /// </summary>
        protected override object[] LoadObjectsAndPersistToDisk()
        {
            int i = 0; int j = 0;
            var esasContainer = _esasWebServiceContextFactory.Create();

            List<AnsoegningskortTekst> loadedObjectsListe = new List<AnsoegningskortTekst>();
            Microsoft.OData.Client.DataServiceQueryContinuation<AnsoegningskortTekst> token = null;

            var response = esasContainer.AnsoegningskortTekst.Execute() as Microsoft.OData.Client.QueryOperationResponse<AnsoegningskortTekst>;
            loadedObjectsListe.AddRange(response.ToList());
            if ( loadedObjectsListe.Any())
            {
                j = loadedObjectsListe.Count();
                PersistToFile(i,j,ref loadedObjectsListe);
            }
            else
                return null;
            
            System.Diagnostics.Debug.WriteLine(loadedObjectsListe.Count());
         
            while ((token = response.GetContinuation()) != null)
            {
               Stopwatch st = new Stopwatch();
                st.Start();
                       esasContainer = _esasWebServiceContextFactory.Create();
                List<AnsoegningskortTekst> loadedObjectsListen = new List<AnsoegningskortTekst>();
                response = esasContainer.Execute<AnsoegningskortTekst>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<AnsoegningskortTekst>;
                loadedObjectsListen.AddRange(response.ToList());
                if ( loadedObjectsListen.Any())
            {
                i = j;
                j = j + loadedObjectsListen.Count();
                PersistToFile(i,j,ref loadedObjectsListen);
                loadedObjectsListen = null;
            }


                // loadedObjectsListe.AddRange(response.ToList()); - only persist to disc, for now.
               st.Stop();
               System.Diagnostics.Debug.WriteLine(j + "-" + st.Elapsed.Seconds);
            }
            
             System.Diagnostics.Debug.WriteLine($"*** total objects written to disk was {j} ***");
            //return loadedObjectsListe.ToArray();
            return null;
        }

        
        private void PersistToFile(int indexToStartLoadFrom, int howManyRecordsToGet, ref List<AnsoegningskortTekst> loadedObjectsListe)
        {
             string entityName = "AnsoegningskortTekst";
              if (!Directory.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}"))
                Directory.CreateDirectory($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}");

            string objectsJson = JsonConvert.SerializeObject(loadedObjectsListe, Formatting.Indented);
            if ( !File.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json"))
                File.WriteAllText($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json", objectsJson );

        }


    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class AnsoegningsopsaetningLoadStrategy : BaseLoadStrategy
    {
         public AnsoegningsopsaetningLoadStrategy(EsasWsContextFactory esasWebServiceContextFactory, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasWebServiceContextFactory, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff)
        {
            var esasContainer = _esasWebServiceContextFactory.Create();
            // design continuation here
            List<Ansoegningsopsaetning> loadedObjectsListe = new List<Ansoegningsopsaetning>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Ansoegningsopsaetning> token = null;

            string datetimeoffSetFromLoadtimeCutOff = loadTimeCutoff.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var response = esasContainer.Ansoegningsopsaetning.AddQueryOption("$filter", $"ModifiedOn gt {datetimeoffSetFromLoadtimeCutOff}").Execute() as Microsoft.OData.Client.QueryOperationResponse<Ansoegningsopsaetning>;

            loadedObjectsListe.AddRange(response.ToList());
            if ( ! loadedObjectsListe.Any())
                return null;
            
            while ((token = response.GetContinuation()) != null)
            {
                System.Diagnostics.Debug.WriteLine("Continuation-token available, retrieving next batch.");
                esasContainer = _esasWebServiceContextFactory.Create();
                response = esasContainer.Execute<Ansoegningsopsaetning>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Ansoegningsopsaetning>;
                loadedObjectsListe.AddRange(response.ToList());
            }

            return loadedObjectsListe.ToArray();
        }

        /// <summary>
        /// This method is only used for initial load of ESAS data - will become useless once we're in production.
        /// </summary>
        protected override object[] LoadObjectsAndPersistToDisk()
        {
            int i = 0; int j = 0;
            var esasContainer = _esasWebServiceContextFactory.Create();

            List<Ansoegningsopsaetning> loadedObjectsListe = new List<Ansoegningsopsaetning>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Ansoegningsopsaetning> token = null;

            var response = esasContainer.Ansoegningsopsaetning.Execute() as Microsoft.OData.Client.QueryOperationResponse<Ansoegningsopsaetning>;
            loadedObjectsListe.AddRange(response.ToList());
            if ( loadedObjectsListe.Any())
            {
                j = loadedObjectsListe.Count();
                PersistToFile(i,j,ref loadedObjectsListe);
            }
            else
                return null;
            
            System.Diagnostics.Debug.WriteLine(loadedObjectsListe.Count());
         
            while ((token = response.GetContinuation()) != null)
            {
               Stopwatch st = new Stopwatch();
                st.Start();
                       esasContainer = _esasWebServiceContextFactory.Create();
                List<Ansoegningsopsaetning> loadedObjectsListen = new List<Ansoegningsopsaetning>();
                response = esasContainer.Execute<Ansoegningsopsaetning>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Ansoegningsopsaetning>;
                loadedObjectsListen.AddRange(response.ToList());
                if ( loadedObjectsListen.Any())
            {
                i = j;
                j = j + loadedObjectsListen.Count();
                PersistToFile(i,j,ref loadedObjectsListen);
                loadedObjectsListen = null;
            }


                // loadedObjectsListe.AddRange(response.ToList()); - only persist to disc, for now.
               st.Stop();
               System.Diagnostics.Debug.WriteLine(j + "-" + st.Elapsed.Seconds);
            }
            
             System.Diagnostics.Debug.WriteLine($"*** total objects written to disk was {j} ***");
            //return loadedObjectsListe.ToArray();
            return null;
        }

        
        private void PersistToFile(int indexToStartLoadFrom, int howManyRecordsToGet, ref List<Ansoegningsopsaetning> loadedObjectsListe)
        {
             string entityName = "Ansoegningsopsaetning";
              if (!Directory.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}"))
                Directory.CreateDirectory($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}");

            string objectsJson = JsonConvert.SerializeObject(loadedObjectsListe, Formatting.Indented);
            if ( !File.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json"))
                File.WriteAllText($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json", objectsJson );

        }


    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class BedoemmelseLoadStrategy : BaseLoadStrategy
    {
         public BedoemmelseLoadStrategy(EsasWsContextFactory esasWebServiceContextFactory, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasWebServiceContextFactory, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff)
        {
            var esasContainer = _esasWebServiceContextFactory.Create();
            // design continuation here
            List<Bedoemmelse> loadedObjectsListe = new List<Bedoemmelse>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Bedoemmelse> token = null;

            string datetimeoffSetFromLoadtimeCutOff = loadTimeCutoff.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var response = esasContainer.Bedoemmelse.AddQueryOption("$filter", $"ModifiedOn gt {datetimeoffSetFromLoadtimeCutOff}").Execute() as Microsoft.OData.Client.QueryOperationResponse<Bedoemmelse>;

            loadedObjectsListe.AddRange(response.ToList());
            if ( ! loadedObjectsListe.Any())
                return null;
            
            while ((token = response.GetContinuation()) != null)
            {
                System.Diagnostics.Debug.WriteLine("Continuation-token available, retrieving next batch.");
                esasContainer = _esasWebServiceContextFactory.Create();
                response = esasContainer.Execute<Bedoemmelse>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Bedoemmelse>;
                loadedObjectsListe.AddRange(response.ToList());
            }

            return loadedObjectsListe.ToArray();
        }

        /// <summary>
        /// This method is only used for initial load of ESAS data - will become useless once we're in production.
        /// </summary>
        protected override object[] LoadObjectsAndPersistToDisk()
        {
            int i = 0; int j = 0;
            var esasContainer = _esasWebServiceContextFactory.Create();

            List<Bedoemmelse> loadedObjectsListe = new List<Bedoemmelse>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Bedoemmelse> token = null;

            var response = esasContainer.Bedoemmelse.Execute() as Microsoft.OData.Client.QueryOperationResponse<Bedoemmelse>;
            loadedObjectsListe.AddRange(response.ToList());
            if ( loadedObjectsListe.Any())
            {
                j = loadedObjectsListe.Count();
                PersistToFile(i,j,ref loadedObjectsListe);
            }
            else
                return null;
            
            System.Diagnostics.Debug.WriteLine(loadedObjectsListe.Count());
         
            while ((token = response.GetContinuation()) != null)
            {
               Stopwatch st = new Stopwatch();
                st.Start();
                       esasContainer = _esasWebServiceContextFactory.Create();
                List<Bedoemmelse> loadedObjectsListen = new List<Bedoemmelse>();
                response = esasContainer.Execute<Bedoemmelse>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Bedoemmelse>;
                loadedObjectsListen.AddRange(response.ToList());
                if ( loadedObjectsListen.Any())
            {
                i = j;
                j = j + loadedObjectsListen.Count();
                PersistToFile(i,j,ref loadedObjectsListen);
                loadedObjectsListen = null;
            }


                // loadedObjectsListe.AddRange(response.ToList()); - only persist to disc, for now.
               st.Stop();
               System.Diagnostics.Debug.WriteLine(j + "-" + st.Elapsed.Seconds);
            }
            
             System.Diagnostics.Debug.WriteLine($"*** total objects written to disk was {j} ***");
            //return loadedObjectsListe.ToArray();
            return null;
        }

        
        private void PersistToFile(int indexToStartLoadFrom, int howManyRecordsToGet, ref List<Bedoemmelse> loadedObjectsListe)
        {
             string entityName = "Bedoemmelse";
              if (!Directory.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}"))
                Directory.CreateDirectory($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}");

            string objectsJson = JsonConvert.SerializeObject(loadedObjectsListe, Formatting.Indented);
            if ( !File.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json"))
                File.WriteAllText($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json", objectsJson );

        }


    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class BedoemmelsesrundeLoadStrategy : BaseLoadStrategy
    {
         public BedoemmelsesrundeLoadStrategy(EsasWsContextFactory esasWebServiceContextFactory, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasWebServiceContextFactory, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff)
        {
            var esasContainer = _esasWebServiceContextFactory.Create();
            // design continuation here
            List<Bedoemmelsesrunde> loadedObjectsListe = new List<Bedoemmelsesrunde>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Bedoemmelsesrunde> token = null;

            string datetimeoffSetFromLoadtimeCutOff = loadTimeCutoff.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var response = esasContainer.Bedoemmelsesrunde.AddQueryOption("$filter", $"ModifiedOn gt {datetimeoffSetFromLoadtimeCutOff}").Execute() as Microsoft.OData.Client.QueryOperationResponse<Bedoemmelsesrunde>;

            loadedObjectsListe.AddRange(response.ToList());
            if ( ! loadedObjectsListe.Any())
                return null;
            
            while ((token = response.GetContinuation()) != null)
            {
                System.Diagnostics.Debug.WriteLine("Continuation-token available, retrieving next batch.");
                esasContainer = _esasWebServiceContextFactory.Create();
                response = esasContainer.Execute<Bedoemmelsesrunde>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Bedoemmelsesrunde>;
                loadedObjectsListe.AddRange(response.ToList());
            }

            return loadedObjectsListe.ToArray();
        }

        /// <summary>
        /// This method is only used for initial load of ESAS data - will become useless once we're in production.
        /// </summary>
        protected override object[] LoadObjectsAndPersistToDisk()
        {
            int i = 0; int j = 0;
            var esasContainer = _esasWebServiceContextFactory.Create();

            List<Bedoemmelsesrunde> loadedObjectsListe = new List<Bedoemmelsesrunde>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Bedoemmelsesrunde> token = null;

            var response = esasContainer.Bedoemmelsesrunde.Execute() as Microsoft.OData.Client.QueryOperationResponse<Bedoemmelsesrunde>;
            loadedObjectsListe.AddRange(response.ToList());
            if ( loadedObjectsListe.Any())
            {
                j = loadedObjectsListe.Count();
                PersistToFile(i,j,ref loadedObjectsListe);
            }
            else
                return null;
            
            System.Diagnostics.Debug.WriteLine(loadedObjectsListe.Count());
         
            while ((token = response.GetContinuation()) != null)
            {
               Stopwatch st = new Stopwatch();
                st.Start();
                       esasContainer = _esasWebServiceContextFactory.Create();
                List<Bedoemmelsesrunde> loadedObjectsListen = new List<Bedoemmelsesrunde>();
                response = esasContainer.Execute<Bedoemmelsesrunde>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Bedoemmelsesrunde>;
                loadedObjectsListen.AddRange(response.ToList());
                if ( loadedObjectsListen.Any())
            {
                i = j;
                j = j + loadedObjectsListen.Count();
                PersistToFile(i,j,ref loadedObjectsListen);
                loadedObjectsListen = null;
            }


                // loadedObjectsListe.AddRange(response.ToList()); - only persist to disc, for now.
               st.Stop();
               System.Diagnostics.Debug.WriteLine(j + "-" + st.Elapsed.Seconds);
            }
            
             System.Diagnostics.Debug.WriteLine($"*** total objects written to disk was {j} ***");
            //return loadedObjectsListe.ToArray();
            return null;
        }

        
        private void PersistToFile(int indexToStartLoadFrom, int howManyRecordsToGet, ref List<Bedoemmelsesrunde> loadedObjectsListe)
        {
             string entityName = "Bedoemmelsesrunde";
              if (!Directory.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}"))
                Directory.CreateDirectory($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}");

            string objectsJson = JsonConvert.SerializeObject(loadedObjectsListe, Formatting.Indented);
            if ( !File.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json"))
                File.WriteAllText($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json", objectsJson );

        }


    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class BevisgrundlagLoadStrategy : BaseLoadStrategy
    {
         public BevisgrundlagLoadStrategy(EsasWsContextFactory esasWebServiceContextFactory, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasWebServiceContextFactory, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff)
        {
            var esasContainer = _esasWebServiceContextFactory.Create();
            // design continuation here
            List<Bevisgrundlag> loadedObjectsListe = new List<Bevisgrundlag>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Bevisgrundlag> token = null;

            string datetimeoffSetFromLoadtimeCutOff = loadTimeCutoff.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var response = esasContainer.Bevisgrundlag.AddQueryOption("$filter", $"ModifiedOn gt {datetimeoffSetFromLoadtimeCutOff}").Execute() as Microsoft.OData.Client.QueryOperationResponse<Bevisgrundlag>;

            loadedObjectsListe.AddRange(response.ToList());
            if ( ! loadedObjectsListe.Any())
                return null;
            
            while ((token = response.GetContinuation()) != null)
            {
                System.Diagnostics.Debug.WriteLine("Continuation-token available, retrieving next batch.");
                esasContainer = _esasWebServiceContextFactory.Create();
                response = esasContainer.Execute<Bevisgrundlag>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Bevisgrundlag>;
                loadedObjectsListe.AddRange(response.ToList());
            }

            return loadedObjectsListe.ToArray();
        }

        /// <summary>
        /// This method is only used for initial load of ESAS data - will become useless once we're in production.
        /// </summary>
        protected override object[] LoadObjectsAndPersistToDisk()
        {
            int i = 0; int j = 0;
            var esasContainer = _esasWebServiceContextFactory.Create();

            List<Bevisgrundlag> loadedObjectsListe = new List<Bevisgrundlag>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Bevisgrundlag> token = null;

            var response = esasContainer.Bevisgrundlag.Execute() as Microsoft.OData.Client.QueryOperationResponse<Bevisgrundlag>;
            loadedObjectsListe.AddRange(response.ToList());
            if ( loadedObjectsListe.Any())
            {
                j = loadedObjectsListe.Count();
                PersistToFile(i,j,ref loadedObjectsListe);
            }
            else
                return null;
            
            System.Diagnostics.Debug.WriteLine(loadedObjectsListe.Count());
         
            while ((token = response.GetContinuation()) != null)
            {
               Stopwatch st = new Stopwatch();
                st.Start();
                       esasContainer = _esasWebServiceContextFactory.Create();
                List<Bevisgrundlag> loadedObjectsListen = new List<Bevisgrundlag>();
                response = esasContainer.Execute<Bevisgrundlag>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Bevisgrundlag>;
                loadedObjectsListen.AddRange(response.ToList());
                if ( loadedObjectsListen.Any())
            {
                i = j;
                j = j + loadedObjectsListen.Count();
                PersistToFile(i,j,ref loadedObjectsListen);
                loadedObjectsListen = null;
            }


                // loadedObjectsListe.AddRange(response.ToList()); - only persist to disc, for now.
               st.Stop();
               System.Diagnostics.Debug.WriteLine(j + "-" + st.Elapsed.Seconds);
            }
            
             System.Diagnostics.Debug.WriteLine($"*** total objects written to disk was {j} ***");
            //return loadedObjectsListe.ToArray();
            return null;
        }

        
        private void PersistToFile(int indexToStartLoadFrom, int howManyRecordsToGet, ref List<Bevisgrundlag> loadedObjectsListe)
        {
             string entityName = "Bevisgrundlag";
              if (!Directory.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}"))
                Directory.CreateDirectory($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}");

            string objectsJson = JsonConvert.SerializeObject(loadedObjectsListe, Formatting.Indented);
            if ( !File.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json"))
                File.WriteAllText($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json", objectsJson );

        }


    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class BilagLoadStrategy : BaseLoadStrategy
    {
         public BilagLoadStrategy(EsasWsContextFactory esasWebServiceContextFactory, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasWebServiceContextFactory, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff)
        {
            var esasContainer = _esasWebServiceContextFactory.Create();
            // design continuation here
            List<Bilag> loadedObjectsListe = new List<Bilag>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Bilag> token = null;

            string datetimeoffSetFromLoadtimeCutOff = loadTimeCutoff.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var response = esasContainer.Bilag.AddQueryOption("$filter", $"ModifiedOn gt {datetimeoffSetFromLoadtimeCutOff}").Execute() as Microsoft.OData.Client.QueryOperationResponse<Bilag>;

            loadedObjectsListe.AddRange(response.ToList());
            if ( ! loadedObjectsListe.Any())
                return null;
            
            while ((token = response.GetContinuation()) != null)
            {
                System.Diagnostics.Debug.WriteLine("Continuation-token available, retrieving next batch.");
                esasContainer = _esasWebServiceContextFactory.Create();
                response = esasContainer.Execute<Bilag>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Bilag>;
                loadedObjectsListe.AddRange(response.ToList());
            }

            return loadedObjectsListe.ToArray();
        }

        /// <summary>
        /// This method is only used for initial load of ESAS data - will become useless once we're in production.
        /// </summary>
        protected override object[] LoadObjectsAndPersistToDisk()
        {
            int i = 0; int j = 0;
            var esasContainer = _esasWebServiceContextFactory.Create();

            List<Bilag> loadedObjectsListe = new List<Bilag>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Bilag> token = null;

            var response = esasContainer.Bilag.Execute() as Microsoft.OData.Client.QueryOperationResponse<Bilag>;
            loadedObjectsListe.AddRange(response.ToList());
            if ( loadedObjectsListe.Any())
            {
                j = loadedObjectsListe.Count();
                PersistToFile(i,j,ref loadedObjectsListe);
            }
            else
                return null;
            
            System.Diagnostics.Debug.WriteLine(loadedObjectsListe.Count());
         
            while ((token = response.GetContinuation()) != null)
            {
               Stopwatch st = new Stopwatch();
                st.Start();
                       esasContainer = _esasWebServiceContextFactory.Create();
                List<Bilag> loadedObjectsListen = new List<Bilag>();
                response = esasContainer.Execute<Bilag>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Bilag>;
                loadedObjectsListen.AddRange(response.ToList());
                if ( loadedObjectsListen.Any())
            {
                i = j;
                j = j + loadedObjectsListen.Count();
                PersistToFile(i,j,ref loadedObjectsListen);
                loadedObjectsListen = null;
            }


                // loadedObjectsListe.AddRange(response.ToList()); - only persist to disc, for now.
               st.Stop();
               System.Diagnostics.Debug.WriteLine(j + "-" + st.Elapsed.Seconds);
            }
            
             System.Diagnostics.Debug.WriteLine($"*** total objects written to disk was {j} ***");
            //return loadedObjectsListe.ToArray();
            return null;
        }

        
        private void PersistToFile(int indexToStartLoadFrom, int howManyRecordsToGet, ref List<Bilag> loadedObjectsListe)
        {
             string entityName = "Bilag";
              if (!Directory.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}"))
                Directory.CreateDirectory($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}");

            string objectsJson = JsonConvert.SerializeObject(loadedObjectsListe, Formatting.Indented);
            if ( !File.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json"))
                File.WriteAllText($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json", objectsJson );

        }


    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class BrancheLoadStrategy : BaseLoadStrategy
    {
         public BrancheLoadStrategy(EsasWsContextFactory esasWebServiceContextFactory, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasWebServiceContextFactory, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff)
        {
            var esasContainer = _esasWebServiceContextFactory.Create();
            // design continuation here
            List<Branche> loadedObjectsListe = new List<Branche>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Branche> token = null;

            string datetimeoffSetFromLoadtimeCutOff = loadTimeCutoff.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var response = esasContainer.Branche.AddQueryOption("$filter", $"ModifiedOn gt {datetimeoffSetFromLoadtimeCutOff}").Execute() as Microsoft.OData.Client.QueryOperationResponse<Branche>;

            loadedObjectsListe.AddRange(response.ToList());
            if ( ! loadedObjectsListe.Any())
                return null;
            
            while ((token = response.GetContinuation()) != null)
            {
                System.Diagnostics.Debug.WriteLine("Continuation-token available, retrieving next batch.");
                esasContainer = _esasWebServiceContextFactory.Create();
                response = esasContainer.Execute<Branche>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Branche>;
                loadedObjectsListe.AddRange(response.ToList());
            }

            return loadedObjectsListe.ToArray();
        }

        /// <summary>
        /// This method is only used for initial load of ESAS data - will become useless once we're in production.
        /// </summary>
        protected override object[] LoadObjectsAndPersistToDisk()
        {
            int i = 0; int j = 0;
            var esasContainer = _esasWebServiceContextFactory.Create();

            List<Branche> loadedObjectsListe = new List<Branche>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Branche> token = null;

            var response = esasContainer.Branche.Execute() as Microsoft.OData.Client.QueryOperationResponse<Branche>;
            loadedObjectsListe.AddRange(response.ToList());
            if ( loadedObjectsListe.Any())
            {
                j = loadedObjectsListe.Count();
                PersistToFile(i,j,ref loadedObjectsListe);
            }
            else
                return null;
            
            System.Diagnostics.Debug.WriteLine(loadedObjectsListe.Count());
         
            while ((token = response.GetContinuation()) != null)
            {
               Stopwatch st = new Stopwatch();
                st.Start();
                       esasContainer = _esasWebServiceContextFactory.Create();
                List<Branche> loadedObjectsListen = new List<Branche>();
                response = esasContainer.Execute<Branche>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Branche>;
                loadedObjectsListen.AddRange(response.ToList());
                if ( loadedObjectsListen.Any())
            {
                i = j;
                j = j + loadedObjectsListen.Count();
                PersistToFile(i,j,ref loadedObjectsListen);
                loadedObjectsListen = null;
            }


                // loadedObjectsListe.AddRange(response.ToList()); - only persist to disc, for now.
               st.Stop();
               System.Diagnostics.Debug.WriteLine(j + "-" + st.Elapsed.Seconds);
            }
            
             System.Diagnostics.Debug.WriteLine($"*** total objects written to disk was {j} ***");
            //return loadedObjectsListe.ToArray();
            return null;
        }

        
        private void PersistToFile(int indexToStartLoadFrom, int howManyRecordsToGet, ref List<Branche> loadedObjectsListe)
        {
             string entityName = "Branche";
              if (!Directory.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}"))
                Directory.CreateDirectory($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}");

            string objectsJson = JsonConvert.SerializeObject(loadedObjectsListe, Formatting.Indented);
            if ( !File.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json"))
                File.WriteAllText($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json", objectsJson );

        }


    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class EksamenstypeLoadStrategy : BaseLoadStrategy
    {
         public EksamenstypeLoadStrategy(EsasWsContextFactory esasWebServiceContextFactory, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasWebServiceContextFactory, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff)
        {
            var esasContainer = _esasWebServiceContextFactory.Create();
            // design continuation here
            List<Eksamenstype> loadedObjectsListe = new List<Eksamenstype>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Eksamenstype> token = null;

            string datetimeoffSetFromLoadtimeCutOff = loadTimeCutoff.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var response = esasContainer.Eksamenstype.AddQueryOption("$filter", $"ModifiedOn gt {datetimeoffSetFromLoadtimeCutOff}").Execute() as Microsoft.OData.Client.QueryOperationResponse<Eksamenstype>;

            loadedObjectsListe.AddRange(response.ToList());
            if ( ! loadedObjectsListe.Any())
                return null;
            
            while ((token = response.GetContinuation()) != null)
            {
                System.Diagnostics.Debug.WriteLine("Continuation-token available, retrieving next batch.");
                esasContainer = _esasWebServiceContextFactory.Create();
                response = esasContainer.Execute<Eksamenstype>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Eksamenstype>;
                loadedObjectsListe.AddRange(response.ToList());
            }

            return loadedObjectsListe.ToArray();
        }

        /// <summary>
        /// This method is only used for initial load of ESAS data - will become useless once we're in production.
        /// </summary>
        protected override object[] LoadObjectsAndPersistToDisk()
        {
            int i = 0; int j = 0;
            var esasContainer = _esasWebServiceContextFactory.Create();

            List<Eksamenstype> loadedObjectsListe = new List<Eksamenstype>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Eksamenstype> token = null;

            var response = esasContainer.Eksamenstype.Execute() as Microsoft.OData.Client.QueryOperationResponse<Eksamenstype>;
            loadedObjectsListe.AddRange(response.ToList());
            if ( loadedObjectsListe.Any())
            {
                j = loadedObjectsListe.Count();
                PersistToFile(i,j,ref loadedObjectsListe);
            }
            else
                return null;
            
            System.Diagnostics.Debug.WriteLine(loadedObjectsListe.Count());
         
            while ((token = response.GetContinuation()) != null)
            {
               Stopwatch st = new Stopwatch();
                st.Start();
                       esasContainer = _esasWebServiceContextFactory.Create();
                List<Eksamenstype> loadedObjectsListen = new List<Eksamenstype>();
                response = esasContainer.Execute<Eksamenstype>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Eksamenstype>;
                loadedObjectsListen.AddRange(response.ToList());
                if ( loadedObjectsListen.Any())
            {
                i = j;
                j = j + loadedObjectsListen.Count();
                PersistToFile(i,j,ref loadedObjectsListen);
                loadedObjectsListen = null;
            }


                // loadedObjectsListe.AddRange(response.ToList()); - only persist to disc, for now.
               st.Stop();
               System.Diagnostics.Debug.WriteLine(j + "-" + st.Elapsed.Seconds);
            }
            
             System.Diagnostics.Debug.WriteLine($"*** total objects written to disk was {j} ***");
            //return loadedObjectsListe.ToArray();
            return null;
        }

        
        private void PersistToFile(int indexToStartLoadFrom, int howManyRecordsToGet, ref List<Eksamenstype> loadedObjectsListe)
        {
             string entityName = "Eksamenstype";
              if (!Directory.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}"))
                Directory.CreateDirectory($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}");

            string objectsJson = JsonConvert.SerializeObject(loadedObjectsListe, Formatting.Indented);
            if ( !File.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json"))
                File.WriteAllText($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json", objectsJson );

        }


    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class EnkeltfagLoadStrategy : BaseLoadStrategy
    {
         public EnkeltfagLoadStrategy(EsasWsContextFactory esasWebServiceContextFactory, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasWebServiceContextFactory, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff)
        {
            var esasContainer = _esasWebServiceContextFactory.Create();
            // design continuation here
            List<Enkeltfag> loadedObjectsListe = new List<Enkeltfag>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Enkeltfag> token = null;

            string datetimeoffSetFromLoadtimeCutOff = loadTimeCutoff.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var response = esasContainer.Enkeltfag.AddQueryOption("$filter", $"ModifiedOn gt {datetimeoffSetFromLoadtimeCutOff}").Execute() as Microsoft.OData.Client.QueryOperationResponse<Enkeltfag>;

            loadedObjectsListe.AddRange(response.ToList());
            if ( ! loadedObjectsListe.Any())
                return null;
            
            while ((token = response.GetContinuation()) != null)
            {
                System.Diagnostics.Debug.WriteLine("Continuation-token available, retrieving next batch.");
                esasContainer = _esasWebServiceContextFactory.Create();
                response = esasContainer.Execute<Enkeltfag>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Enkeltfag>;
                loadedObjectsListe.AddRange(response.ToList());
            }

            return loadedObjectsListe.ToArray();
        }

        /// <summary>
        /// This method is only used for initial load of ESAS data - will become useless once we're in production.
        /// </summary>
        protected override object[] LoadObjectsAndPersistToDisk()
        {
            int i = 0; int j = 0;
            var esasContainer = _esasWebServiceContextFactory.Create();

            List<Enkeltfag> loadedObjectsListe = new List<Enkeltfag>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Enkeltfag> token = null;

            var response = esasContainer.Enkeltfag.Execute() as Microsoft.OData.Client.QueryOperationResponse<Enkeltfag>;
            loadedObjectsListe.AddRange(response.ToList());
            if ( loadedObjectsListe.Any())
            {
                j = loadedObjectsListe.Count();
                PersistToFile(i,j,ref loadedObjectsListe);
            }
            else
                return null;
            
            System.Diagnostics.Debug.WriteLine(loadedObjectsListe.Count());
         
            while ((token = response.GetContinuation()) != null)
            {
               Stopwatch st = new Stopwatch();
                st.Start();
                       esasContainer = _esasWebServiceContextFactory.Create();
                List<Enkeltfag> loadedObjectsListen = new List<Enkeltfag>();
                response = esasContainer.Execute<Enkeltfag>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Enkeltfag>;
                loadedObjectsListen.AddRange(response.ToList());
                if ( loadedObjectsListen.Any())
            {
                i = j;
                j = j + loadedObjectsListen.Count();
                PersistToFile(i,j,ref loadedObjectsListen);
                loadedObjectsListen = null;
            }


                // loadedObjectsListe.AddRange(response.ToList()); - only persist to disc, for now.
               st.Stop();
               System.Diagnostics.Debug.WriteLine(j + "-" + st.Elapsed.Seconds);
            }
            
             System.Diagnostics.Debug.WriteLine($"*** total objects written to disk was {j} ***");
            //return loadedObjectsListe.ToArray();
            return null;
        }

        
        private void PersistToFile(int indexToStartLoadFrom, int howManyRecordsToGet, ref List<Enkeltfag> loadedObjectsListe)
        {
             string entityName = "Enkeltfag";
              if (!Directory.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}"))
                Directory.CreateDirectory($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}");

            string objectsJson = JsonConvert.SerializeObject(loadedObjectsListe, Formatting.Indented);
            if ( !File.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json"))
                File.WriteAllText($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json", objectsJson );

        }


    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class ErfaringerLoadStrategy : BaseLoadStrategy
    {
         public ErfaringerLoadStrategy(EsasWsContextFactory esasWebServiceContextFactory, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasWebServiceContextFactory, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff)
        {
            var esasContainer = _esasWebServiceContextFactory.Create();
            // design continuation here
            List<Erfaringer> loadedObjectsListe = new List<Erfaringer>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Erfaringer> token = null;

            string datetimeoffSetFromLoadtimeCutOff = loadTimeCutoff.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var response = esasContainer.Erfaringer.AddQueryOption("$filter", $"ModifiedOn gt {datetimeoffSetFromLoadtimeCutOff}").Execute() as Microsoft.OData.Client.QueryOperationResponse<Erfaringer>;

            loadedObjectsListe.AddRange(response.ToList());
            if ( ! loadedObjectsListe.Any())
                return null;
            
            while ((token = response.GetContinuation()) != null)
            {
                System.Diagnostics.Debug.WriteLine("Continuation-token available, retrieving next batch.");
                esasContainer = _esasWebServiceContextFactory.Create();
                response = esasContainer.Execute<Erfaringer>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Erfaringer>;
                loadedObjectsListe.AddRange(response.ToList());
            }

            return loadedObjectsListe.ToArray();
        }

        /// <summary>
        /// This method is only used for initial load of ESAS data - will become useless once we're in production.
        /// </summary>
        protected override object[] LoadObjectsAndPersistToDisk()
        {
            int i = 0; int j = 0;
            var esasContainer = _esasWebServiceContextFactory.Create();

            List<Erfaringer> loadedObjectsListe = new List<Erfaringer>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Erfaringer> token = null;

            var response = esasContainer.Erfaringer.Execute() as Microsoft.OData.Client.QueryOperationResponse<Erfaringer>;
            loadedObjectsListe.AddRange(response.ToList());
            if ( loadedObjectsListe.Any())
            {
                j = loadedObjectsListe.Count();
                PersistToFile(i,j,ref loadedObjectsListe);
            }
            else
                return null;
            
            System.Diagnostics.Debug.WriteLine(loadedObjectsListe.Count());
         
            while ((token = response.GetContinuation()) != null)
            {
               Stopwatch st = new Stopwatch();
                st.Start();
                       esasContainer = _esasWebServiceContextFactory.Create();
                List<Erfaringer> loadedObjectsListen = new List<Erfaringer>();
                response = esasContainer.Execute<Erfaringer>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Erfaringer>;
                loadedObjectsListen.AddRange(response.ToList());
                if ( loadedObjectsListen.Any())
            {
                i = j;
                j = j + loadedObjectsListen.Count();
                PersistToFile(i,j,ref loadedObjectsListen);
                loadedObjectsListen = null;
            }


                // loadedObjectsListe.AddRange(response.ToList()); - only persist to disc, for now.
               st.Stop();
               System.Diagnostics.Debug.WriteLine(j + "-" + st.Elapsed.Seconds);
            }
            
             System.Diagnostics.Debug.WriteLine($"*** total objects written to disk was {j} ***");
            //return loadedObjectsListe.ToArray();
            return null;
        }

        
        private void PersistToFile(int indexToStartLoadFrom, int howManyRecordsToGet, ref List<Erfaringer> loadedObjectsListe)
        {
             string entityName = "Erfaringer";
              if (!Directory.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}"))
                Directory.CreateDirectory($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}");

            string objectsJson = JsonConvert.SerializeObject(loadedObjectsListe, Formatting.Indented);
            if ( !File.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json"))
                File.WriteAllText($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json", objectsJson );

        }


    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class FagpersonsrelationLoadStrategy : BaseLoadStrategy
    {
         public FagpersonsrelationLoadStrategy(EsasWsContextFactory esasWebServiceContextFactory, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasWebServiceContextFactory, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff)
        {
            var esasContainer = _esasWebServiceContextFactory.Create();
            // design continuation here
            List<Fagpersonsrelation> loadedObjectsListe = new List<Fagpersonsrelation>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Fagpersonsrelation> token = null;

            string datetimeoffSetFromLoadtimeCutOff = loadTimeCutoff.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var response = esasContainer.Fagpersonsrelation.AddQueryOption("$filter", $"ModifiedOn gt {datetimeoffSetFromLoadtimeCutOff}").Execute() as Microsoft.OData.Client.QueryOperationResponse<Fagpersonsrelation>;

            loadedObjectsListe.AddRange(response.ToList());
            if ( ! loadedObjectsListe.Any())
                return null;
            
            while ((token = response.GetContinuation()) != null)
            {
                System.Diagnostics.Debug.WriteLine("Continuation-token available, retrieving next batch.");
                esasContainer = _esasWebServiceContextFactory.Create();
                response = esasContainer.Execute<Fagpersonsrelation>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Fagpersonsrelation>;
                loadedObjectsListe.AddRange(response.ToList());
            }

            return loadedObjectsListe.ToArray();
        }

        /// <summary>
        /// This method is only used for initial load of ESAS data - will become useless once we're in production.
        /// </summary>
        protected override object[] LoadObjectsAndPersistToDisk()
        {
            int i = 0; int j = 0;
            var esasContainer = _esasWebServiceContextFactory.Create();

            List<Fagpersonsrelation> loadedObjectsListe = new List<Fagpersonsrelation>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Fagpersonsrelation> token = null;

            var response = esasContainer.Fagpersonsrelation.Execute() as Microsoft.OData.Client.QueryOperationResponse<Fagpersonsrelation>;
            loadedObjectsListe.AddRange(response.ToList());
            if ( loadedObjectsListe.Any())
            {
                j = loadedObjectsListe.Count();
                PersistToFile(i,j,ref loadedObjectsListe);
            }
            else
                return null;
            
            System.Diagnostics.Debug.WriteLine(loadedObjectsListe.Count());
         
            while ((token = response.GetContinuation()) != null)
            {
               Stopwatch st = new Stopwatch();
                st.Start();
                       esasContainer = _esasWebServiceContextFactory.Create();
                List<Fagpersonsrelation> loadedObjectsListen = new List<Fagpersonsrelation>();
                response = esasContainer.Execute<Fagpersonsrelation>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Fagpersonsrelation>;
                loadedObjectsListen.AddRange(response.ToList());
                if ( loadedObjectsListen.Any())
            {
                i = j;
                j = j + loadedObjectsListen.Count();
                PersistToFile(i,j,ref loadedObjectsListen);
                loadedObjectsListen = null;
            }


                // loadedObjectsListe.AddRange(response.ToList()); - only persist to disc, for now.
               st.Stop();
               System.Diagnostics.Debug.WriteLine(j + "-" + st.Elapsed.Seconds);
            }
            
             System.Diagnostics.Debug.WriteLine($"*** total objects written to disk was {j} ***");
            //return loadedObjectsListe.ToArray();
            return null;
        }

        
        private void PersistToFile(int indexToStartLoadFrom, int howManyRecordsToGet, ref List<Fagpersonsrelation> loadedObjectsListe)
        {
             string entityName = "Fagpersonsrelation";
              if (!Directory.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}"))
                Directory.CreateDirectory($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}");

            string objectsJson = JsonConvert.SerializeObject(loadedObjectsListe, Formatting.Indented);
            if ( !File.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json"))
                File.WriteAllText($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json", objectsJson );

        }


    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class FravaersaarsagLoadStrategy : BaseLoadStrategy
    {
         public FravaersaarsagLoadStrategy(EsasWsContextFactory esasWebServiceContextFactory, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasWebServiceContextFactory, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff)
        {
            var esasContainer = _esasWebServiceContextFactory.Create();
            // design continuation here
            List<Fravaersaarsag> loadedObjectsListe = new List<Fravaersaarsag>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Fravaersaarsag> token = null;

            string datetimeoffSetFromLoadtimeCutOff = loadTimeCutoff.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var response = esasContainer.Fravaersaarsag.AddQueryOption("$filter", $"ModifiedOn gt {datetimeoffSetFromLoadtimeCutOff}").Execute() as Microsoft.OData.Client.QueryOperationResponse<Fravaersaarsag>;

            loadedObjectsListe.AddRange(response.ToList());
            if ( ! loadedObjectsListe.Any())
                return null;
            
            while ((token = response.GetContinuation()) != null)
            {
                System.Diagnostics.Debug.WriteLine("Continuation-token available, retrieving next batch.");
                esasContainer = _esasWebServiceContextFactory.Create();
                response = esasContainer.Execute<Fravaersaarsag>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Fravaersaarsag>;
                loadedObjectsListe.AddRange(response.ToList());
            }

            return loadedObjectsListe.ToArray();
        }

        /// <summary>
        /// This method is only used for initial load of ESAS data - will become useless once we're in production.
        /// </summary>
        protected override object[] LoadObjectsAndPersistToDisk()
        {
            int i = 0; int j = 0;
            var esasContainer = _esasWebServiceContextFactory.Create();

            List<Fravaersaarsag> loadedObjectsListe = new List<Fravaersaarsag>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Fravaersaarsag> token = null;

            var response = esasContainer.Fravaersaarsag.Execute() as Microsoft.OData.Client.QueryOperationResponse<Fravaersaarsag>;
            loadedObjectsListe.AddRange(response.ToList());
            if ( loadedObjectsListe.Any())
            {
                j = loadedObjectsListe.Count();
                PersistToFile(i,j,ref loadedObjectsListe);
            }
            else
                return null;
            
            System.Diagnostics.Debug.WriteLine(loadedObjectsListe.Count());
         
            while ((token = response.GetContinuation()) != null)
            {
               Stopwatch st = new Stopwatch();
                st.Start();
                       esasContainer = _esasWebServiceContextFactory.Create();
                List<Fravaersaarsag> loadedObjectsListen = new List<Fravaersaarsag>();
                response = esasContainer.Execute<Fravaersaarsag>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Fravaersaarsag>;
                loadedObjectsListen.AddRange(response.ToList());
                if ( loadedObjectsListen.Any())
            {
                i = j;
                j = j + loadedObjectsListen.Count();
                PersistToFile(i,j,ref loadedObjectsListen);
                loadedObjectsListen = null;
            }


                // loadedObjectsListe.AddRange(response.ToList()); - only persist to disc, for now.
               st.Stop();
               System.Diagnostics.Debug.WriteLine(j + "-" + st.Elapsed.Seconds);
            }
            
             System.Diagnostics.Debug.WriteLine($"*** total objects written to disk was {j} ***");
            //return loadedObjectsListe.ToArray();
            return null;
        }

        
        private void PersistToFile(int indexToStartLoadFrom, int howManyRecordsToGet, ref List<Fravaersaarsag> loadedObjectsListe)
        {
             string entityName = "Fravaersaarsag";
              if (!Directory.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}"))
                Directory.CreateDirectory($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}");

            string objectsJson = JsonConvert.SerializeObject(loadedObjectsListe, Formatting.Indented);
            if ( !File.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json"))
                File.WriteAllText($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json", objectsJson );

        }


    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class GebyrtypeLoadStrategy : BaseLoadStrategy
    {
         public GebyrtypeLoadStrategy(EsasWsContextFactory esasWebServiceContextFactory, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasWebServiceContextFactory, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff)
        {
            var esasContainer = _esasWebServiceContextFactory.Create();
            // design continuation here
            List<Gebyrtype> loadedObjectsListe = new List<Gebyrtype>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Gebyrtype> token = null;

            string datetimeoffSetFromLoadtimeCutOff = loadTimeCutoff.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var response = esasContainer.Gebyrtype.AddQueryOption("$filter", $"ModifiedOn gt {datetimeoffSetFromLoadtimeCutOff}").Execute() as Microsoft.OData.Client.QueryOperationResponse<Gebyrtype>;

            loadedObjectsListe.AddRange(response.ToList());
            if ( ! loadedObjectsListe.Any())
                return null;
            
            while ((token = response.GetContinuation()) != null)
            {
                System.Diagnostics.Debug.WriteLine("Continuation-token available, retrieving next batch.");
                esasContainer = _esasWebServiceContextFactory.Create();
                response = esasContainer.Execute<Gebyrtype>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Gebyrtype>;
                loadedObjectsListe.AddRange(response.ToList());
            }

            return loadedObjectsListe.ToArray();
        }

        /// <summary>
        /// This method is only used for initial load of ESAS data - will become useless once we're in production.
        /// </summary>
        protected override object[] LoadObjectsAndPersistToDisk()
        {
            int i = 0; int j = 0;
            var esasContainer = _esasWebServiceContextFactory.Create();

            List<Gebyrtype> loadedObjectsListe = new List<Gebyrtype>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Gebyrtype> token = null;

            var response = esasContainer.Gebyrtype.Execute() as Microsoft.OData.Client.QueryOperationResponse<Gebyrtype>;
            loadedObjectsListe.AddRange(response.ToList());
            if ( loadedObjectsListe.Any())
            {
                j = loadedObjectsListe.Count();
                PersistToFile(i,j,ref loadedObjectsListe);
            }
            else
                return null;
            
            System.Diagnostics.Debug.WriteLine(loadedObjectsListe.Count());
         
            while ((token = response.GetContinuation()) != null)
            {
               Stopwatch st = new Stopwatch();
                st.Start();
                       esasContainer = _esasWebServiceContextFactory.Create();
                List<Gebyrtype> loadedObjectsListen = new List<Gebyrtype>();
                response = esasContainer.Execute<Gebyrtype>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Gebyrtype>;
                loadedObjectsListen.AddRange(response.ToList());
                if ( loadedObjectsListen.Any())
            {
                i = j;
                j = j + loadedObjectsListen.Count();
                PersistToFile(i,j,ref loadedObjectsListen);
                loadedObjectsListen = null;
            }


                // loadedObjectsListe.AddRange(response.ToList()); - only persist to disc, for now.
               st.Stop();
               System.Diagnostics.Debug.WriteLine(j + "-" + st.Elapsed.Seconds);
            }
            
             System.Diagnostics.Debug.WriteLine($"*** total objects written to disk was {j} ***");
            //return loadedObjectsListe.ToArray();
            return null;
        }

        
        private void PersistToFile(int indexToStartLoadFrom, int howManyRecordsToGet, ref List<Gebyrtype> loadedObjectsListe)
        {
             string entityName = "Gebyrtype";
              if (!Directory.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}"))
                Directory.CreateDirectory($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}");

            string objectsJson = JsonConvert.SerializeObject(loadedObjectsListe, Formatting.Indented);
            if ( !File.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json"))
                File.WriteAllText($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json", objectsJson );

        }


    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class GennemfoerelsesUddannelseselementLoadStrategy : BaseLoadStrategy
    {
         public GennemfoerelsesUddannelseselementLoadStrategy(EsasWsContextFactory esasWebServiceContextFactory, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasWebServiceContextFactory, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff)
        {
            var esasContainer = _esasWebServiceContextFactory.Create();
            // design continuation here
            List<GennemfoerelsesUddannelseselement> loadedObjectsListe = new List<GennemfoerelsesUddannelseselement>();
            Microsoft.OData.Client.DataServiceQueryContinuation<GennemfoerelsesUddannelseselement> token = null;

            string datetimeoffSetFromLoadtimeCutOff = loadTimeCutoff.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var response = esasContainer.GennemfoerelsesUddannelseselement.AddQueryOption("$filter", $"ModifiedOn gt {datetimeoffSetFromLoadtimeCutOff}").Execute() as Microsoft.OData.Client.QueryOperationResponse<GennemfoerelsesUddannelseselement>;

            loadedObjectsListe.AddRange(response.ToList());
            if ( ! loadedObjectsListe.Any())
                return null;
            
            while ((token = response.GetContinuation()) != null)
            {
                System.Diagnostics.Debug.WriteLine("Continuation-token available, retrieving next batch.");
                esasContainer = _esasWebServiceContextFactory.Create();
                response = esasContainer.Execute<GennemfoerelsesUddannelseselement>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<GennemfoerelsesUddannelseselement>;
                loadedObjectsListe.AddRange(response.ToList());
            }

            return loadedObjectsListe.ToArray();
        }

        /// <summary>
        /// This method is only used for initial load of ESAS data - will become useless once we're in production.
        /// </summary>
        protected override object[] LoadObjectsAndPersistToDisk()
        {
            int i = 0; int j = 0;
            var esasContainer = _esasWebServiceContextFactory.Create();

            List<GennemfoerelsesUddannelseselement> loadedObjectsListe = new List<GennemfoerelsesUddannelseselement>();
            Microsoft.OData.Client.DataServiceQueryContinuation<GennemfoerelsesUddannelseselement> token = null;

            var response = esasContainer.GennemfoerelsesUddannelseselement.Execute() as Microsoft.OData.Client.QueryOperationResponse<GennemfoerelsesUddannelseselement>;
            loadedObjectsListe.AddRange(response.ToList());
            if ( loadedObjectsListe.Any())
            {
                j = loadedObjectsListe.Count();
                PersistToFile(i,j,ref loadedObjectsListe);
            }
            else
                return null;
            
            System.Diagnostics.Debug.WriteLine(loadedObjectsListe.Count());
         
            while ((token = response.GetContinuation()) != null)
            {
               Stopwatch st = new Stopwatch();
                st.Start();
                       esasContainer = _esasWebServiceContextFactory.Create();
                List<GennemfoerelsesUddannelseselement> loadedObjectsListen = new List<GennemfoerelsesUddannelseselement>();
                response = esasContainer.Execute<GennemfoerelsesUddannelseselement>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<GennemfoerelsesUddannelseselement>;
                loadedObjectsListen.AddRange(response.ToList());
                if ( loadedObjectsListen.Any())
            {
                i = j;
                j = j + loadedObjectsListen.Count();
                PersistToFile(i,j,ref loadedObjectsListen);
                loadedObjectsListen = null;
            }


                // loadedObjectsListe.AddRange(response.ToList()); - only persist to disc, for now.
               st.Stop();
               System.Diagnostics.Debug.WriteLine(j + "-" + st.Elapsed.Seconds);
            }
            
             System.Diagnostics.Debug.WriteLine($"*** total objects written to disk was {j} ***");
            //return loadedObjectsListe.ToArray();
            return null;
        }

        
        private void PersistToFile(int indexToStartLoadFrom, int howManyRecordsToGet, ref List<GennemfoerelsesUddannelseselement> loadedObjectsListe)
        {
             string entityName = "GennemfoerelsesUddannelseselement";
              if (!Directory.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}"))
                Directory.CreateDirectory($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}");

            string objectsJson = JsonConvert.SerializeObject(loadedObjectsListe, Formatting.Indented);
            if ( !File.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json"))
                File.WriteAllText($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json", objectsJson );

        }


    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class GymnasielleFagkravLoadStrategy : BaseLoadStrategy
    {
         public GymnasielleFagkravLoadStrategy(EsasWsContextFactory esasWebServiceContextFactory, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasWebServiceContextFactory, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff)
        {
            var esasContainer = _esasWebServiceContextFactory.Create();
            // design continuation here
            List<GymnasielleFagkrav> loadedObjectsListe = new List<GymnasielleFagkrav>();
            Microsoft.OData.Client.DataServiceQueryContinuation<GymnasielleFagkrav> token = null;

            string datetimeoffSetFromLoadtimeCutOff = loadTimeCutoff.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var response = esasContainer.GymnasielleFagkrav.AddQueryOption("$filter", $"ModifiedOn gt {datetimeoffSetFromLoadtimeCutOff}").Execute() as Microsoft.OData.Client.QueryOperationResponse<GymnasielleFagkrav>;

            loadedObjectsListe.AddRange(response.ToList());
            if ( ! loadedObjectsListe.Any())
                return null;
            
            while ((token = response.GetContinuation()) != null)
            {
                System.Diagnostics.Debug.WriteLine("Continuation-token available, retrieving next batch.");
                esasContainer = _esasWebServiceContextFactory.Create();
                response = esasContainer.Execute<GymnasielleFagkrav>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<GymnasielleFagkrav>;
                loadedObjectsListe.AddRange(response.ToList());
            }

            return loadedObjectsListe.ToArray();
        }

        /// <summary>
        /// This method is only used for initial load of ESAS data - will become useless once we're in production.
        /// </summary>
        protected override object[] LoadObjectsAndPersistToDisk()
        {
            int i = 0; int j = 0;
            var esasContainer = _esasWebServiceContextFactory.Create();

            List<GymnasielleFagkrav> loadedObjectsListe = new List<GymnasielleFagkrav>();
            Microsoft.OData.Client.DataServiceQueryContinuation<GymnasielleFagkrav> token = null;

            var response = esasContainer.GymnasielleFagkrav.Execute() as Microsoft.OData.Client.QueryOperationResponse<GymnasielleFagkrav>;
            loadedObjectsListe.AddRange(response.ToList());
            if ( loadedObjectsListe.Any())
            {
                j = loadedObjectsListe.Count();
                PersistToFile(i,j,ref loadedObjectsListe);
            }
            else
                return null;
            
            System.Diagnostics.Debug.WriteLine(loadedObjectsListe.Count());
         
            while ((token = response.GetContinuation()) != null)
            {
               Stopwatch st = new Stopwatch();
                st.Start();
                       esasContainer = _esasWebServiceContextFactory.Create();
                List<GymnasielleFagkrav> loadedObjectsListen = new List<GymnasielleFagkrav>();
                response = esasContainer.Execute<GymnasielleFagkrav>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<GymnasielleFagkrav>;
                loadedObjectsListen.AddRange(response.ToList());
                if ( loadedObjectsListen.Any())
            {
                i = j;
                j = j + loadedObjectsListen.Count();
                PersistToFile(i,j,ref loadedObjectsListen);
                loadedObjectsListen = null;
            }


                // loadedObjectsListe.AddRange(response.ToList()); - only persist to disc, for now.
               st.Stop();
               System.Diagnostics.Debug.WriteLine(j + "-" + st.Elapsed.Seconds);
            }
            
             System.Diagnostics.Debug.WriteLine($"*** total objects written to disk was {j} ***");
            //return loadedObjectsListe.ToArray();
            return null;
        }

        
        private void PersistToFile(int indexToStartLoadFrom, int howManyRecordsToGet, ref List<GymnasielleFagkrav> loadedObjectsListe)
        {
             string entityName = "GymnasielleFagkrav";
              if (!Directory.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}"))
                Directory.CreateDirectory($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}");

            string objectsJson = JsonConvert.SerializeObject(loadedObjectsListe, Formatting.Indented);
            if ( !File.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json"))
                File.WriteAllText($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json", objectsJson );

        }


    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class GymnasielleKarakterkravLoadStrategy : BaseLoadStrategy
    {
         public GymnasielleKarakterkravLoadStrategy(EsasWsContextFactory esasWebServiceContextFactory, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasWebServiceContextFactory, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff)
        {
            var esasContainer = _esasWebServiceContextFactory.Create();
            // design continuation here
            List<GymnasielleKarakterkrav> loadedObjectsListe = new List<GymnasielleKarakterkrav>();
            Microsoft.OData.Client.DataServiceQueryContinuation<GymnasielleKarakterkrav> token = null;

            string datetimeoffSetFromLoadtimeCutOff = loadTimeCutoff.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var response = esasContainer.GymnasielleKarakterkrav.AddQueryOption("$filter", $"ModifiedOn gt {datetimeoffSetFromLoadtimeCutOff}").Execute() as Microsoft.OData.Client.QueryOperationResponse<GymnasielleKarakterkrav>;

            loadedObjectsListe.AddRange(response.ToList());
            if ( ! loadedObjectsListe.Any())
                return null;
            
            while ((token = response.GetContinuation()) != null)
            {
                System.Diagnostics.Debug.WriteLine("Continuation-token available, retrieving next batch.");
                esasContainer = _esasWebServiceContextFactory.Create();
                response = esasContainer.Execute<GymnasielleKarakterkrav>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<GymnasielleKarakterkrav>;
                loadedObjectsListe.AddRange(response.ToList());
            }

            return loadedObjectsListe.ToArray();
        }

        /// <summary>
        /// This method is only used for initial load of ESAS data - will become useless once we're in production.
        /// </summary>
        protected override object[] LoadObjectsAndPersistToDisk()
        {
            int i = 0; int j = 0;
            var esasContainer = _esasWebServiceContextFactory.Create();

            List<GymnasielleKarakterkrav> loadedObjectsListe = new List<GymnasielleKarakterkrav>();
            Microsoft.OData.Client.DataServiceQueryContinuation<GymnasielleKarakterkrav> token = null;

            var response = esasContainer.GymnasielleKarakterkrav.Execute() as Microsoft.OData.Client.QueryOperationResponse<GymnasielleKarakterkrav>;
            loadedObjectsListe.AddRange(response.ToList());
            if ( loadedObjectsListe.Any())
            {
                j = loadedObjectsListe.Count();
                PersistToFile(i,j,ref loadedObjectsListe);
            }
            else
                return null;
            
            System.Diagnostics.Debug.WriteLine(loadedObjectsListe.Count());
         
            while ((token = response.GetContinuation()) != null)
            {
               Stopwatch st = new Stopwatch();
                st.Start();
                       esasContainer = _esasWebServiceContextFactory.Create();
                List<GymnasielleKarakterkrav> loadedObjectsListen = new List<GymnasielleKarakterkrav>();
                response = esasContainer.Execute<GymnasielleKarakterkrav>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<GymnasielleKarakterkrav>;
                loadedObjectsListen.AddRange(response.ToList());
                if ( loadedObjectsListen.Any())
            {
                i = j;
                j = j + loadedObjectsListen.Count();
                PersistToFile(i,j,ref loadedObjectsListen);
                loadedObjectsListen = null;
            }


                // loadedObjectsListe.AddRange(response.ToList()); - only persist to disc, for now.
               st.Stop();
               System.Diagnostics.Debug.WriteLine(j + "-" + st.Elapsed.Seconds);
            }
            
             System.Diagnostics.Debug.WriteLine($"*** total objects written to disk was {j} ***");
            //return loadedObjectsListe.ToArray();
            return null;
        }

        
        private void PersistToFile(int indexToStartLoadFrom, int howManyRecordsToGet, ref List<GymnasielleKarakterkrav> loadedObjectsListe)
        {
             string entityName = "GymnasielleKarakterkrav";
              if (!Directory.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}"))
                Directory.CreateDirectory($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}");

            string objectsJson = JsonConvert.SerializeObject(loadedObjectsListe, Formatting.Indented);
            if ( !File.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json"))
                File.WriteAllText($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json", objectsJson );

        }


    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class HoldLoadStrategy : BaseLoadStrategy
    {
         public HoldLoadStrategy(EsasWsContextFactory esasWebServiceContextFactory, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasWebServiceContextFactory, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff)
        {
            var esasContainer = _esasWebServiceContextFactory.Create();
            // design continuation here
            List<Hold> loadedObjectsListe = new List<Hold>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Hold> token = null;

            string datetimeoffSetFromLoadtimeCutOff = loadTimeCutoff.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var response = esasContainer.Hold.AddQueryOption("$filter", $"ModifiedOn gt {datetimeoffSetFromLoadtimeCutOff}").Execute() as Microsoft.OData.Client.QueryOperationResponse<Hold>;

            loadedObjectsListe.AddRange(response.ToList());
            if ( ! loadedObjectsListe.Any())
                return null;
            
            while ((token = response.GetContinuation()) != null)
            {
                System.Diagnostics.Debug.WriteLine("Continuation-token available, retrieving next batch.");
                esasContainer = _esasWebServiceContextFactory.Create();
                response = esasContainer.Execute<Hold>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Hold>;
                loadedObjectsListe.AddRange(response.ToList());
            }

            return loadedObjectsListe.ToArray();
        }

        /// <summary>
        /// This method is only used for initial load of ESAS data - will become useless once we're in production.
        /// </summary>
        protected override object[] LoadObjectsAndPersistToDisk()
        {
            int i = 0; int j = 0;
            var esasContainer = _esasWebServiceContextFactory.Create();

            List<Hold> loadedObjectsListe = new List<Hold>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Hold> token = null;

            var response = esasContainer.Hold.Execute() as Microsoft.OData.Client.QueryOperationResponse<Hold>;
            loadedObjectsListe.AddRange(response.ToList());
            if ( loadedObjectsListe.Any())
            {
                j = loadedObjectsListe.Count();
                PersistToFile(i,j,ref loadedObjectsListe);
            }
            else
                return null;
            
            System.Diagnostics.Debug.WriteLine(loadedObjectsListe.Count());
         
            while ((token = response.GetContinuation()) != null)
            {
               Stopwatch st = new Stopwatch();
                st.Start();
                       esasContainer = _esasWebServiceContextFactory.Create();
                List<Hold> loadedObjectsListen = new List<Hold>();
                response = esasContainer.Execute<Hold>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Hold>;
                loadedObjectsListen.AddRange(response.ToList());
                if ( loadedObjectsListen.Any())
            {
                i = j;
                j = j + loadedObjectsListen.Count();
                PersistToFile(i,j,ref loadedObjectsListen);
                loadedObjectsListen = null;
            }


                // loadedObjectsListe.AddRange(response.ToList()); - only persist to disc, for now.
               st.Stop();
               System.Diagnostics.Debug.WriteLine(j + "-" + st.Elapsed.Seconds);
            }
            
             System.Diagnostics.Debug.WriteLine($"*** total objects written to disk was {j} ***");
            //return loadedObjectsListe.ToArray();
            return null;
        }

        
        private void PersistToFile(int indexToStartLoadFrom, int howManyRecordsToGet, ref List<Hold> loadedObjectsListe)
        {
             string entityName = "Hold";
              if (!Directory.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}"))
                Directory.CreateDirectory($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}");

            string objectsJson = JsonConvert.SerializeObject(loadedObjectsListe, Formatting.Indented);
            if ( !File.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json"))
                File.WriteAllText($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json", objectsJson );

        }


    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class IndskrivningsformLoadStrategy : BaseLoadStrategy
    {
         public IndskrivningsformLoadStrategy(EsasWsContextFactory esasWebServiceContextFactory, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasWebServiceContextFactory, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff)
        {
            var esasContainer = _esasWebServiceContextFactory.Create();
            // design continuation here
            List<Indskrivningsform> loadedObjectsListe = new List<Indskrivningsform>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Indskrivningsform> token = null;

            string datetimeoffSetFromLoadtimeCutOff = loadTimeCutoff.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var response = esasContainer.Indskrivningsform.AddQueryOption("$filter", $"ModifiedOn gt {datetimeoffSetFromLoadtimeCutOff}").Execute() as Microsoft.OData.Client.QueryOperationResponse<Indskrivningsform>;

            loadedObjectsListe.AddRange(response.ToList());
            if ( ! loadedObjectsListe.Any())
                return null;
            
            while ((token = response.GetContinuation()) != null)
            {
                System.Diagnostics.Debug.WriteLine("Continuation-token available, retrieving next batch.");
                esasContainer = _esasWebServiceContextFactory.Create();
                response = esasContainer.Execute<Indskrivningsform>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Indskrivningsform>;
                loadedObjectsListe.AddRange(response.ToList());
            }

            return loadedObjectsListe.ToArray();
        }

        /// <summary>
        /// This method is only used for initial load of ESAS data - will become useless once we're in production.
        /// </summary>
        protected override object[] LoadObjectsAndPersistToDisk()
        {
            int i = 0; int j = 0;
            var esasContainer = _esasWebServiceContextFactory.Create();

            List<Indskrivningsform> loadedObjectsListe = new List<Indskrivningsform>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Indskrivningsform> token = null;

            var response = esasContainer.Indskrivningsform.Execute() as Microsoft.OData.Client.QueryOperationResponse<Indskrivningsform>;
            loadedObjectsListe.AddRange(response.ToList());
            if ( loadedObjectsListe.Any())
            {
                j = loadedObjectsListe.Count();
                PersistToFile(i,j,ref loadedObjectsListe);
            }
            else
                return null;
            
            System.Diagnostics.Debug.WriteLine(loadedObjectsListe.Count());
         
            while ((token = response.GetContinuation()) != null)
            {
               Stopwatch st = new Stopwatch();
                st.Start();
                       esasContainer = _esasWebServiceContextFactory.Create();
                List<Indskrivningsform> loadedObjectsListen = new List<Indskrivningsform>();
                response = esasContainer.Execute<Indskrivningsform>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Indskrivningsform>;
                loadedObjectsListen.AddRange(response.ToList());
                if ( loadedObjectsListen.Any())
            {
                i = j;
                j = j + loadedObjectsListen.Count();
                PersistToFile(i,j,ref loadedObjectsListen);
                loadedObjectsListen = null;
            }


                // loadedObjectsListe.AddRange(response.ToList()); - only persist to disc, for now.
               st.Stop();
               System.Diagnostics.Debug.WriteLine(j + "-" + st.Elapsed.Seconds);
            }
            
             System.Diagnostics.Debug.WriteLine($"*** total objects written to disk was {j} ***");
            //return loadedObjectsListe.ToArray();
            return null;
        }

        
        private void PersistToFile(int indexToStartLoadFrom, int howManyRecordsToGet, ref List<Indskrivningsform> loadedObjectsListe)
        {
             string entityName = "Indskrivningsform";
              if (!Directory.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}"))
                Directory.CreateDirectory($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}");

            string objectsJson = JsonConvert.SerializeObject(loadedObjectsListe, Formatting.Indented);
            if ( !File.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json"))
                File.WriteAllText($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json", objectsJson );

        }


    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class InstitutionVirksomhedLoadStrategy : BaseLoadStrategy
    {
         public InstitutionVirksomhedLoadStrategy(EsasWsContextFactory esasWebServiceContextFactory, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasWebServiceContextFactory, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff)
        {
            var esasContainer = _esasWebServiceContextFactory.Create();
            // design continuation here
            List<InstitutionVirksomhed> loadedObjectsListe = new List<InstitutionVirksomhed>();
            Microsoft.OData.Client.DataServiceQueryContinuation<InstitutionVirksomhed> token = null;

            string datetimeoffSetFromLoadtimeCutOff = loadTimeCutoff.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var response = esasContainer.InstitutionVirksomhed.AddQueryOption("$filter", $"ModifiedOn gt {datetimeoffSetFromLoadtimeCutOff}").Execute() as Microsoft.OData.Client.QueryOperationResponse<InstitutionVirksomhed>;

            loadedObjectsListe.AddRange(response.ToList());
            if ( ! loadedObjectsListe.Any())
                return null;
            
            while ((token = response.GetContinuation()) != null)
            {
                System.Diagnostics.Debug.WriteLine("Continuation-token available, retrieving next batch.");
                esasContainer = _esasWebServiceContextFactory.Create();
                response = esasContainer.Execute<InstitutionVirksomhed>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<InstitutionVirksomhed>;
                loadedObjectsListe.AddRange(response.ToList());
            }

            return loadedObjectsListe.ToArray();
        }

        /// <summary>
        /// This method is only used for initial load of ESAS data - will become useless once we're in production.
        /// </summary>
        protected override object[] LoadObjectsAndPersistToDisk()
        {
            int i = 0; int j = 0;
            var esasContainer = _esasWebServiceContextFactory.Create();

            List<InstitutionVirksomhed> loadedObjectsListe = new List<InstitutionVirksomhed>();
            Microsoft.OData.Client.DataServiceQueryContinuation<InstitutionVirksomhed> token = null;

            var response = esasContainer.InstitutionVirksomhed.Execute() as Microsoft.OData.Client.QueryOperationResponse<InstitutionVirksomhed>;
            loadedObjectsListe.AddRange(response.ToList());
            if ( loadedObjectsListe.Any())
            {
                j = loadedObjectsListe.Count();
                PersistToFile(i,j,ref loadedObjectsListe);
            }
            else
                return null;
            
            System.Diagnostics.Debug.WriteLine(loadedObjectsListe.Count());
         
            while ((token = response.GetContinuation()) != null)
            {
               Stopwatch st = new Stopwatch();
                st.Start();
                       esasContainer = _esasWebServiceContextFactory.Create();
                List<InstitutionVirksomhed> loadedObjectsListen = new List<InstitutionVirksomhed>();
                response = esasContainer.Execute<InstitutionVirksomhed>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<InstitutionVirksomhed>;
                loadedObjectsListen.AddRange(response.ToList());
                if ( loadedObjectsListen.Any())
            {
                i = j;
                j = j + loadedObjectsListen.Count();
                PersistToFile(i,j,ref loadedObjectsListen);
                loadedObjectsListen = null;
            }


                // loadedObjectsListe.AddRange(response.ToList()); - only persist to disc, for now.
               st.Stop();
               System.Diagnostics.Debug.WriteLine(j + "-" + st.Elapsed.Seconds);
            }
            
             System.Diagnostics.Debug.WriteLine($"*** total objects written to disk was {j} ***");
            //return loadedObjectsListe.ToArray();
            return null;
        }

        
        private void PersistToFile(int indexToStartLoadFrom, int howManyRecordsToGet, ref List<InstitutionVirksomhed> loadedObjectsListe)
        {
             string entityName = "InstitutionVirksomhed";
              if (!Directory.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}"))
                Directory.CreateDirectory($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}");

            string objectsJson = JsonConvert.SerializeObject(loadedObjectsListe, Formatting.Indented);
            if ( !File.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json"))
                File.WriteAllText($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json", objectsJson );

        }


    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class InstitutionsoplysningerLoadStrategy : BaseLoadStrategy
    {
         public InstitutionsoplysningerLoadStrategy(EsasWsContextFactory esasWebServiceContextFactory, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasWebServiceContextFactory, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff)
        {
            var esasContainer = _esasWebServiceContextFactory.Create();
            // design continuation here
            List<Institutionsoplysninger> loadedObjectsListe = new List<Institutionsoplysninger>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Institutionsoplysninger> token = null;

            string datetimeoffSetFromLoadtimeCutOff = loadTimeCutoff.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var response = esasContainer.Institutionsoplysninger.AddQueryOption("$filter", $"ModifiedOn gt {datetimeoffSetFromLoadtimeCutOff}").Execute() as Microsoft.OData.Client.QueryOperationResponse<Institutionsoplysninger>;

            loadedObjectsListe.AddRange(response.ToList());
            if ( ! loadedObjectsListe.Any())
                return null;
            
            while ((token = response.GetContinuation()) != null)
            {
                System.Diagnostics.Debug.WriteLine("Continuation-token available, retrieving next batch.");
                esasContainer = _esasWebServiceContextFactory.Create();
                response = esasContainer.Execute<Institutionsoplysninger>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Institutionsoplysninger>;
                loadedObjectsListe.AddRange(response.ToList());
            }

            return loadedObjectsListe.ToArray();
        }

        /// <summary>
        /// This method is only used for initial load of ESAS data - will become useless once we're in production.
        /// </summary>
        protected override object[] LoadObjectsAndPersistToDisk()
        {
            int i = 0; int j = 0;
            var esasContainer = _esasWebServiceContextFactory.Create();

            List<Institutionsoplysninger> loadedObjectsListe = new List<Institutionsoplysninger>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Institutionsoplysninger> token = null;

            var response = esasContainer.Institutionsoplysninger.Execute() as Microsoft.OData.Client.QueryOperationResponse<Institutionsoplysninger>;
            loadedObjectsListe.AddRange(response.ToList());
            if ( loadedObjectsListe.Any())
            {
                j = loadedObjectsListe.Count();
                PersistToFile(i,j,ref loadedObjectsListe);
            }
            else
                return null;
            
            System.Diagnostics.Debug.WriteLine(loadedObjectsListe.Count());
         
            while ((token = response.GetContinuation()) != null)
            {
               Stopwatch st = new Stopwatch();
                st.Start();
                       esasContainer = _esasWebServiceContextFactory.Create();
                List<Institutionsoplysninger> loadedObjectsListen = new List<Institutionsoplysninger>();
                response = esasContainer.Execute<Institutionsoplysninger>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Institutionsoplysninger>;
                loadedObjectsListen.AddRange(response.ToList());
                if ( loadedObjectsListen.Any())
            {
                i = j;
                j = j + loadedObjectsListen.Count();
                PersistToFile(i,j,ref loadedObjectsListen);
                loadedObjectsListen = null;
            }


                // loadedObjectsListe.AddRange(response.ToList()); - only persist to disc, for now.
               st.Stop();
               System.Diagnostics.Debug.WriteLine(j + "-" + st.Elapsed.Seconds);
            }
            
             System.Diagnostics.Debug.WriteLine($"*** total objects written to disk was {j} ***");
            //return loadedObjectsListe.ToArray();
            return null;
        }

        
        private void PersistToFile(int indexToStartLoadFrom, int howManyRecordsToGet, ref List<Institutionsoplysninger> loadedObjectsListe)
        {
             string entityName = "Institutionsoplysninger";
              if (!Directory.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}"))
                Directory.CreateDirectory($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}");

            string objectsJson = JsonConvert.SerializeObject(loadedObjectsListe, Formatting.Indented);
            if ( !File.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json"))
                File.WriteAllText($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json", objectsJson );

        }


    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class InternationaliseringLoadStrategy : BaseLoadStrategy
    {
         public InternationaliseringLoadStrategy(EsasWsContextFactory esasWebServiceContextFactory, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasWebServiceContextFactory, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff)
        {
            var esasContainer = _esasWebServiceContextFactory.Create();
            // design continuation here
            List<Internationalisering> loadedObjectsListe = new List<Internationalisering>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Internationalisering> token = null;

            string datetimeoffSetFromLoadtimeCutOff = loadTimeCutoff.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var response = esasContainer.Internationalisering.AddQueryOption("$filter", $"ModifiedOn gt {datetimeoffSetFromLoadtimeCutOff}").Execute() as Microsoft.OData.Client.QueryOperationResponse<Internationalisering>;

            loadedObjectsListe.AddRange(response.ToList());
            if ( ! loadedObjectsListe.Any())
                return null;
            
            while ((token = response.GetContinuation()) != null)
            {
                System.Diagnostics.Debug.WriteLine("Continuation-token available, retrieving next batch.");
                esasContainer = _esasWebServiceContextFactory.Create();
                response = esasContainer.Execute<Internationalisering>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Internationalisering>;
                loadedObjectsListe.AddRange(response.ToList());
            }

            return loadedObjectsListe.ToArray();
        }

        /// <summary>
        /// This method is only used for initial load of ESAS data - will become useless once we're in production.
        /// </summary>
        protected override object[] LoadObjectsAndPersistToDisk()
        {
            int i = 0; int j = 0;
            var esasContainer = _esasWebServiceContextFactory.Create();

            List<Internationalisering> loadedObjectsListe = new List<Internationalisering>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Internationalisering> token = null;

            var response = esasContainer.Internationalisering.Execute() as Microsoft.OData.Client.QueryOperationResponse<Internationalisering>;
            loadedObjectsListe.AddRange(response.ToList());
            if ( loadedObjectsListe.Any())
            {
                j = loadedObjectsListe.Count();
                PersistToFile(i,j,ref loadedObjectsListe);
            }
            else
                return null;
            
            System.Diagnostics.Debug.WriteLine(loadedObjectsListe.Count());
         
            while ((token = response.GetContinuation()) != null)
            {
               Stopwatch st = new Stopwatch();
                st.Start();
                       esasContainer = _esasWebServiceContextFactory.Create();
                List<Internationalisering> loadedObjectsListen = new List<Internationalisering>();
                response = esasContainer.Execute<Internationalisering>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Internationalisering>;
                loadedObjectsListen.AddRange(response.ToList());
                if ( loadedObjectsListen.Any())
            {
                i = j;
                j = j + loadedObjectsListen.Count();
                PersistToFile(i,j,ref loadedObjectsListen);
                loadedObjectsListen = null;
            }


                // loadedObjectsListe.AddRange(response.ToList()); - only persist to disc, for now.
               st.Stop();
               System.Diagnostics.Debug.WriteLine(j + "-" + st.Elapsed.Seconds);
            }
            
             System.Diagnostics.Debug.WriteLine($"*** total objects written to disk was {j} ***");
            //return loadedObjectsListe.ToArray();
            return null;
        }

        
        private void PersistToFile(int indexToStartLoadFrom, int howManyRecordsToGet, ref List<Internationalisering> loadedObjectsListe)
        {
             string entityName = "Internationalisering";
              if (!Directory.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}"))
                Directory.CreateDirectory($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}");

            string objectsJson = JsonConvert.SerializeObject(loadedObjectsListe, Formatting.Indented);
            if ( !File.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json"))
                File.WriteAllText($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json", objectsJson );

        }


    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class KOTGruppeLoadStrategy : BaseLoadStrategy
    {
         public KOTGruppeLoadStrategy(EsasWsContextFactory esasWebServiceContextFactory, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasWebServiceContextFactory, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff)
        {
            var esasContainer = _esasWebServiceContextFactory.Create();
            // design continuation here
            List<KOTGruppe> loadedObjectsListe = new List<KOTGruppe>();
            Microsoft.OData.Client.DataServiceQueryContinuation<KOTGruppe> token = null;

            string datetimeoffSetFromLoadtimeCutOff = loadTimeCutoff.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var response = esasContainer.KOTGruppe.AddQueryOption("$filter", $"ModifiedOn gt {datetimeoffSetFromLoadtimeCutOff}").Execute() as Microsoft.OData.Client.QueryOperationResponse<KOTGruppe>;

            loadedObjectsListe.AddRange(response.ToList());
            if ( ! loadedObjectsListe.Any())
                return null;
            
            while ((token = response.GetContinuation()) != null)
            {
                System.Diagnostics.Debug.WriteLine("Continuation-token available, retrieving next batch.");
                esasContainer = _esasWebServiceContextFactory.Create();
                response = esasContainer.Execute<KOTGruppe>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<KOTGruppe>;
                loadedObjectsListe.AddRange(response.ToList());
            }

            return loadedObjectsListe.ToArray();
        }

        /// <summary>
        /// This method is only used for initial load of ESAS data - will become useless once we're in production.
        /// </summary>
        protected override object[] LoadObjectsAndPersistToDisk()
        {
            int i = 0; int j = 0;
            var esasContainer = _esasWebServiceContextFactory.Create();

            List<KOTGruppe> loadedObjectsListe = new List<KOTGruppe>();
            Microsoft.OData.Client.DataServiceQueryContinuation<KOTGruppe> token = null;

            var response = esasContainer.KOTGruppe.Execute() as Microsoft.OData.Client.QueryOperationResponse<KOTGruppe>;
            loadedObjectsListe.AddRange(response.ToList());
            if ( loadedObjectsListe.Any())
            {
                j = loadedObjectsListe.Count();
                PersistToFile(i,j,ref loadedObjectsListe);
            }
            else
                return null;
            
            System.Diagnostics.Debug.WriteLine(loadedObjectsListe.Count());
         
            while ((token = response.GetContinuation()) != null)
            {
               Stopwatch st = new Stopwatch();
                st.Start();
                       esasContainer = _esasWebServiceContextFactory.Create();
                List<KOTGruppe> loadedObjectsListen = new List<KOTGruppe>();
                response = esasContainer.Execute<KOTGruppe>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<KOTGruppe>;
                loadedObjectsListen.AddRange(response.ToList());
                if ( loadedObjectsListen.Any())
            {
                i = j;
                j = j + loadedObjectsListen.Count();
                PersistToFile(i,j,ref loadedObjectsListen);
                loadedObjectsListen = null;
            }


                // loadedObjectsListe.AddRange(response.ToList()); - only persist to disc, for now.
               st.Stop();
               System.Diagnostics.Debug.WriteLine(j + "-" + st.Elapsed.Seconds);
            }
            
             System.Diagnostics.Debug.WriteLine($"*** total objects written to disk was {j} ***");
            //return loadedObjectsListe.ToArray();
            return null;
        }

        
        private void PersistToFile(int indexToStartLoadFrom, int howManyRecordsToGet, ref List<KOTGruppe> loadedObjectsListe)
        {
             string entityName = "KOTGruppe";
              if (!Directory.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}"))
                Directory.CreateDirectory($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}");

            string objectsJson = JsonConvert.SerializeObject(loadedObjectsListe, Formatting.Indented);
            if ( !File.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json"))
                File.WriteAllText($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json", objectsJson );

        }


    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class KOTGruppeTilmeldingLoadStrategy : BaseLoadStrategy
    {
         public KOTGruppeTilmeldingLoadStrategy(EsasWsContextFactory esasWebServiceContextFactory, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasWebServiceContextFactory, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff)
        {
            var esasContainer = _esasWebServiceContextFactory.Create();
            // design continuation here
            List<KOTGruppeTilmelding> loadedObjectsListe = new List<KOTGruppeTilmelding>();
            Microsoft.OData.Client.DataServiceQueryContinuation<KOTGruppeTilmelding> token = null;

            string datetimeoffSetFromLoadtimeCutOff = loadTimeCutoff.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var response = esasContainer.KOTGruppeTilmelding.AddQueryOption("$filter", $"ModifiedOn gt {datetimeoffSetFromLoadtimeCutOff}").Execute() as Microsoft.OData.Client.QueryOperationResponse<KOTGruppeTilmelding>;

            loadedObjectsListe.AddRange(response.ToList());
            if ( ! loadedObjectsListe.Any())
                return null;
            
            while ((token = response.GetContinuation()) != null)
            {
                System.Diagnostics.Debug.WriteLine("Continuation-token available, retrieving next batch.");
                esasContainer = _esasWebServiceContextFactory.Create();
                response = esasContainer.Execute<KOTGruppeTilmelding>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<KOTGruppeTilmelding>;
                loadedObjectsListe.AddRange(response.ToList());
            }

            return loadedObjectsListe.ToArray();
        }

        /// <summary>
        /// This method is only used for initial load of ESAS data - will become useless once we're in production.
        /// </summary>
        protected override object[] LoadObjectsAndPersistToDisk()
        {
            int i = 0; int j = 0;
            var esasContainer = _esasWebServiceContextFactory.Create();

            List<KOTGruppeTilmelding> loadedObjectsListe = new List<KOTGruppeTilmelding>();
            Microsoft.OData.Client.DataServiceQueryContinuation<KOTGruppeTilmelding> token = null;

            var response = esasContainer.KOTGruppeTilmelding.Execute() as Microsoft.OData.Client.QueryOperationResponse<KOTGruppeTilmelding>;
            loadedObjectsListe.AddRange(response.ToList());
            if ( loadedObjectsListe.Any())
            {
                j = loadedObjectsListe.Count();
                PersistToFile(i,j,ref loadedObjectsListe);
            }
            else
                return null;
            
            System.Diagnostics.Debug.WriteLine(loadedObjectsListe.Count());
         
            while ((token = response.GetContinuation()) != null)
            {
               Stopwatch st = new Stopwatch();
                st.Start();
                       esasContainer = _esasWebServiceContextFactory.Create();
                List<KOTGruppeTilmelding> loadedObjectsListen = new List<KOTGruppeTilmelding>();
                response = esasContainer.Execute<KOTGruppeTilmelding>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<KOTGruppeTilmelding>;
                loadedObjectsListen.AddRange(response.ToList());
                if ( loadedObjectsListen.Any())
            {
                i = j;
                j = j + loadedObjectsListen.Count();
                PersistToFile(i,j,ref loadedObjectsListen);
                loadedObjectsListen = null;
            }


                // loadedObjectsListe.AddRange(response.ToList()); - only persist to disc, for now.
               st.Stop();
               System.Diagnostics.Debug.WriteLine(j + "-" + st.Elapsed.Seconds);
            }
            
             System.Diagnostics.Debug.WriteLine($"*** total objects written to disk was {j} ***");
            //return loadedObjectsListe.ToArray();
            return null;
        }

        
        private void PersistToFile(int indexToStartLoadFrom, int howManyRecordsToGet, ref List<KOTGruppeTilmelding> loadedObjectsListe)
        {
             string entityName = "KOTGruppeTilmelding";
              if (!Directory.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}"))
                Directory.CreateDirectory($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}");

            string objectsJson = JsonConvert.SerializeObject(loadedObjectsListe, Formatting.Indented);
            if ( !File.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json"))
                File.WriteAllText($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json", objectsJson );

        }


    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class KarakterLoadStrategy : BaseLoadStrategy
    {
         public KarakterLoadStrategy(EsasWsContextFactory esasWebServiceContextFactory, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasWebServiceContextFactory, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff)
        {
            var esasContainer = _esasWebServiceContextFactory.Create();
            // design continuation here
            List<Karakter> loadedObjectsListe = new List<Karakter>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Karakter> token = null;

            string datetimeoffSetFromLoadtimeCutOff = loadTimeCutoff.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var response = esasContainer.Karakter.AddQueryOption("$filter", $"ModifiedOn gt {datetimeoffSetFromLoadtimeCutOff}").Execute() as Microsoft.OData.Client.QueryOperationResponse<Karakter>;

            loadedObjectsListe.AddRange(response.ToList());
            if ( ! loadedObjectsListe.Any())
                return null;
            
            while ((token = response.GetContinuation()) != null)
            {
                System.Diagnostics.Debug.WriteLine("Continuation-token available, retrieving next batch.");
                esasContainer = _esasWebServiceContextFactory.Create();
                response = esasContainer.Execute<Karakter>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Karakter>;
                loadedObjectsListe.AddRange(response.ToList());
            }

            return loadedObjectsListe.ToArray();
        }

        /// <summary>
        /// This method is only used for initial load of ESAS data - will become useless once we're in production.
        /// </summary>
        protected override object[] LoadObjectsAndPersistToDisk()
        {
            int i = 0; int j = 0;
            var esasContainer = _esasWebServiceContextFactory.Create();

            List<Karakter> loadedObjectsListe = new List<Karakter>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Karakter> token = null;

            var response = esasContainer.Karakter.Execute() as Microsoft.OData.Client.QueryOperationResponse<Karakter>;
            loadedObjectsListe.AddRange(response.ToList());
            if ( loadedObjectsListe.Any())
            {
                j = loadedObjectsListe.Count();
                PersistToFile(i,j,ref loadedObjectsListe);
            }
            else
                return null;
            
            System.Diagnostics.Debug.WriteLine(loadedObjectsListe.Count());
         
            while ((token = response.GetContinuation()) != null)
            {
               Stopwatch st = new Stopwatch();
                st.Start();
                       esasContainer = _esasWebServiceContextFactory.Create();
                List<Karakter> loadedObjectsListen = new List<Karakter>();
                response = esasContainer.Execute<Karakter>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Karakter>;
                loadedObjectsListen.AddRange(response.ToList());
                if ( loadedObjectsListen.Any())
            {
                i = j;
                j = j + loadedObjectsListen.Count();
                PersistToFile(i,j,ref loadedObjectsListen);
                loadedObjectsListen = null;
            }


                // loadedObjectsListe.AddRange(response.ToList()); - only persist to disc, for now.
               st.Stop();
               System.Diagnostics.Debug.WriteLine(j + "-" + st.Elapsed.Seconds);
            }
            
             System.Diagnostics.Debug.WriteLine($"*** total objects written to disk was {j} ***");
            //return loadedObjectsListe.ToArray();
            return null;
        }

        
        private void PersistToFile(int indexToStartLoadFrom, int howManyRecordsToGet, ref List<Karakter> loadedObjectsListe)
        {
             string entityName = "Karakter";
              if (!Directory.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}"))
                Directory.CreateDirectory($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}");

            string objectsJson = JsonConvert.SerializeObject(loadedObjectsListe, Formatting.Indented);
            if ( !File.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json"))
                File.WriteAllText($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json", objectsJson );

        }


    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class KommunikationLoadStrategy : BaseLoadStrategy
    {
         public KommunikationLoadStrategy(EsasWsContextFactory esasWebServiceContextFactory, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasWebServiceContextFactory, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff)
        {
            var esasContainer = _esasWebServiceContextFactory.Create();
            // design continuation here
            List<Kommunikation> loadedObjectsListe = new List<Kommunikation>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Kommunikation> token = null;

            string datetimeoffSetFromLoadtimeCutOff = loadTimeCutoff.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var response = esasContainer.Kommunikation.AddQueryOption("$filter", $"ModifiedOn gt {datetimeoffSetFromLoadtimeCutOff}").Execute() as Microsoft.OData.Client.QueryOperationResponse<Kommunikation>;

            loadedObjectsListe.AddRange(response.ToList());
            if ( ! loadedObjectsListe.Any())
                return null;
            
            while ((token = response.GetContinuation()) != null)
            {
                System.Diagnostics.Debug.WriteLine("Continuation-token available, retrieving next batch.");
                esasContainer = _esasWebServiceContextFactory.Create();
                response = esasContainer.Execute<Kommunikation>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Kommunikation>;
                loadedObjectsListe.AddRange(response.ToList());
            }

            return loadedObjectsListe.ToArray();
        }

        /// <summary>
        /// This method is only used for initial load of ESAS data - will become useless once we're in production.
        /// </summary>
        protected override object[] LoadObjectsAndPersistToDisk()
        {
            int i = 0; int j = 0;
            var esasContainer = _esasWebServiceContextFactory.Create();

            List<Kommunikation> loadedObjectsListe = new List<Kommunikation>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Kommunikation> token = null;

            var response = esasContainer.Kommunikation.Execute() as Microsoft.OData.Client.QueryOperationResponse<Kommunikation>;
            loadedObjectsListe.AddRange(response.ToList());
            if ( loadedObjectsListe.Any())
            {
                j = loadedObjectsListe.Count();
                PersistToFile(i,j,ref loadedObjectsListe);
            }
            else
                return null;
            
            System.Diagnostics.Debug.WriteLine(loadedObjectsListe.Count());
         
            while ((token = response.GetContinuation()) != null)
            {
               Stopwatch st = new Stopwatch();
                st.Start();
                       esasContainer = _esasWebServiceContextFactory.Create();
                List<Kommunikation> loadedObjectsListen = new List<Kommunikation>();
                response = esasContainer.Execute<Kommunikation>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Kommunikation>;
                loadedObjectsListen.AddRange(response.ToList());
                if ( loadedObjectsListen.Any())
            {
                i = j;
                j = j + loadedObjectsListen.Count();
                PersistToFile(i,j,ref loadedObjectsListen);
                loadedObjectsListen = null;
            }


                // loadedObjectsListe.AddRange(response.ToList()); - only persist to disc, for now.
               st.Stop();
               System.Diagnostics.Debug.WriteLine(j + "-" + st.Elapsed.Seconds);
            }
            
             System.Diagnostics.Debug.WriteLine($"*** total objects written to disk was {j} ***");
            //return loadedObjectsListe.ToArray();
            return null;
        }

        
        private void PersistToFile(int indexToStartLoadFrom, int howManyRecordsToGet, ref List<Kommunikation> loadedObjectsListe)
        {
             string entityName = "Kommunikation";
              if (!Directory.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}"))
                Directory.CreateDirectory($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}");

            string objectsJson = JsonConvert.SerializeObject(loadedObjectsListe, Formatting.Indented);
            if ( !File.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json"))
                File.WriteAllText($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json", objectsJson );

        }


    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class KommuneLoadStrategy : BaseLoadStrategy
    {
         public KommuneLoadStrategy(EsasWsContextFactory esasWebServiceContextFactory, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasWebServiceContextFactory, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff)
        {
            var esasContainer = _esasWebServiceContextFactory.Create();
            // design continuation here
            List<Kommune> loadedObjectsListe = new List<Kommune>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Kommune> token = null;

            string datetimeoffSetFromLoadtimeCutOff = loadTimeCutoff.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var response = esasContainer.Kommune.AddQueryOption("$filter", $"ModifiedOn gt {datetimeoffSetFromLoadtimeCutOff}").Execute() as Microsoft.OData.Client.QueryOperationResponse<Kommune>;

            loadedObjectsListe.AddRange(response.ToList());
            if ( ! loadedObjectsListe.Any())
                return null;
            
            while ((token = response.GetContinuation()) != null)
            {
                System.Diagnostics.Debug.WriteLine("Continuation-token available, retrieving next batch.");
                esasContainer = _esasWebServiceContextFactory.Create();
                response = esasContainer.Execute<Kommune>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Kommune>;
                loadedObjectsListe.AddRange(response.ToList());
            }

            return loadedObjectsListe.ToArray();
        }

        /// <summary>
        /// This method is only used for initial load of ESAS data - will become useless once we're in production.
        /// </summary>
        protected override object[] LoadObjectsAndPersistToDisk()
        {
            int i = 0; int j = 0;
            var esasContainer = _esasWebServiceContextFactory.Create();

            List<Kommune> loadedObjectsListe = new List<Kommune>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Kommune> token = null;

            var response = esasContainer.Kommune.Execute() as Microsoft.OData.Client.QueryOperationResponse<Kommune>;
            loadedObjectsListe.AddRange(response.ToList());
            if ( loadedObjectsListe.Any())
            {
                j = loadedObjectsListe.Count();
                PersistToFile(i,j,ref loadedObjectsListe);
            }
            else
                return null;
            
            System.Diagnostics.Debug.WriteLine(loadedObjectsListe.Count());
         
            while ((token = response.GetContinuation()) != null)
            {
               Stopwatch st = new Stopwatch();
                st.Start();
                       esasContainer = _esasWebServiceContextFactory.Create();
                List<Kommune> loadedObjectsListen = new List<Kommune>();
                response = esasContainer.Execute<Kommune>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Kommune>;
                loadedObjectsListen.AddRange(response.ToList());
                if ( loadedObjectsListen.Any())
            {
                i = j;
                j = j + loadedObjectsListen.Count();
                PersistToFile(i,j,ref loadedObjectsListen);
                loadedObjectsListen = null;
            }


                // loadedObjectsListe.AddRange(response.ToList()); - only persist to disc, for now.
               st.Stop();
               System.Diagnostics.Debug.WriteLine(j + "-" + st.Elapsed.Seconds);
            }
            
             System.Diagnostics.Debug.WriteLine($"*** total objects written to disk was {j} ***");
            //return loadedObjectsListe.ToArray();
            return null;
        }

        
        private void PersistToFile(int indexToStartLoadFrom, int howManyRecordsToGet, ref List<Kommune> loadedObjectsListe)
        {
             string entityName = "Kommune";
              if (!Directory.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}"))
                Directory.CreateDirectory($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}");

            string objectsJson = JsonConvert.SerializeObject(loadedObjectsListe, Formatting.Indented);
            if ( !File.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json"))
                File.WriteAllText($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json", objectsJson );

        }


    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class KurserSkoleopholdLoadStrategy : BaseLoadStrategy
    {
         public KurserSkoleopholdLoadStrategy(EsasWsContextFactory esasWebServiceContextFactory, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasWebServiceContextFactory, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff)
        {
            var esasContainer = _esasWebServiceContextFactory.Create();
            // design continuation here
            List<KurserSkoleophold> loadedObjectsListe = new List<KurserSkoleophold>();
            Microsoft.OData.Client.DataServiceQueryContinuation<KurserSkoleophold> token = null;

            string datetimeoffSetFromLoadtimeCutOff = loadTimeCutoff.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var response = esasContainer.KurserSkoleophold.AddQueryOption("$filter", $"ModifiedOn gt {datetimeoffSetFromLoadtimeCutOff}").Execute() as Microsoft.OData.Client.QueryOperationResponse<KurserSkoleophold>;

            loadedObjectsListe.AddRange(response.ToList());
            if ( ! loadedObjectsListe.Any())
                return null;
            
            while ((token = response.GetContinuation()) != null)
            {
                System.Diagnostics.Debug.WriteLine("Continuation-token available, retrieving next batch.");
                esasContainer = _esasWebServiceContextFactory.Create();
                response = esasContainer.Execute<KurserSkoleophold>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<KurserSkoleophold>;
                loadedObjectsListe.AddRange(response.ToList());
            }

            return loadedObjectsListe.ToArray();
        }

        /// <summary>
        /// This method is only used for initial load of ESAS data - will become useless once we're in production.
        /// </summary>
        protected override object[] LoadObjectsAndPersistToDisk()
        {
            int i = 0; int j = 0;
            var esasContainer = _esasWebServiceContextFactory.Create();

            List<KurserSkoleophold> loadedObjectsListe = new List<KurserSkoleophold>();
            Microsoft.OData.Client.DataServiceQueryContinuation<KurserSkoleophold> token = null;

            var response = esasContainer.KurserSkoleophold.Execute() as Microsoft.OData.Client.QueryOperationResponse<KurserSkoleophold>;
            loadedObjectsListe.AddRange(response.ToList());
            if ( loadedObjectsListe.Any())
            {
                j = loadedObjectsListe.Count();
                PersistToFile(i,j,ref loadedObjectsListe);
            }
            else
                return null;
            
            System.Diagnostics.Debug.WriteLine(loadedObjectsListe.Count());
         
            while ((token = response.GetContinuation()) != null)
            {
               Stopwatch st = new Stopwatch();
                st.Start();
                       esasContainer = _esasWebServiceContextFactory.Create();
                List<KurserSkoleophold> loadedObjectsListen = new List<KurserSkoleophold>();
                response = esasContainer.Execute<KurserSkoleophold>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<KurserSkoleophold>;
                loadedObjectsListen.AddRange(response.ToList());
                if ( loadedObjectsListen.Any())
            {
                i = j;
                j = j + loadedObjectsListen.Count();
                PersistToFile(i,j,ref loadedObjectsListen);
                loadedObjectsListen = null;
            }


                // loadedObjectsListe.AddRange(response.ToList()); - only persist to disc, for now.
               st.Stop();
               System.Diagnostics.Debug.WriteLine(j + "-" + st.Elapsed.Seconds);
            }
            
             System.Diagnostics.Debug.WriteLine($"*** total objects written to disk was {j} ***");
            //return loadedObjectsListe.ToArray();
            return null;
        }

        
        private void PersistToFile(int indexToStartLoadFrom, int howManyRecordsToGet, ref List<KurserSkoleophold> loadedObjectsListe)
        {
             string entityName = "KurserSkoleophold";
              if (!Directory.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}"))
                Directory.CreateDirectory($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}");

            string objectsJson = JsonConvert.SerializeObject(loadedObjectsListe, Formatting.Indented);
            if ( !File.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json"))
                File.WriteAllText($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json", objectsJson );

        }


    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class KvalifikationskriterieLoadStrategy : BaseLoadStrategy
    {
         public KvalifikationskriterieLoadStrategy(EsasWsContextFactory esasWebServiceContextFactory, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasWebServiceContextFactory, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff)
        {
            var esasContainer = _esasWebServiceContextFactory.Create();
            // design continuation here
            List<Kvalifikationskriterie> loadedObjectsListe = new List<Kvalifikationskriterie>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Kvalifikationskriterie> token = null;

            string datetimeoffSetFromLoadtimeCutOff = loadTimeCutoff.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var response = esasContainer.Kvalifikationskriterie.AddQueryOption("$filter", $"ModifiedOn gt {datetimeoffSetFromLoadtimeCutOff}").Execute() as Microsoft.OData.Client.QueryOperationResponse<Kvalifikationskriterie>;

            loadedObjectsListe.AddRange(response.ToList());
            if ( ! loadedObjectsListe.Any())
                return null;
            
            while ((token = response.GetContinuation()) != null)
            {
                System.Diagnostics.Debug.WriteLine("Continuation-token available, retrieving next batch.");
                esasContainer = _esasWebServiceContextFactory.Create();
                response = esasContainer.Execute<Kvalifikationskriterie>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Kvalifikationskriterie>;
                loadedObjectsListe.AddRange(response.ToList());
            }

            return loadedObjectsListe.ToArray();
        }

        /// <summary>
        /// This method is only used for initial load of ESAS data - will become useless once we're in production.
        /// </summary>
        protected override object[] LoadObjectsAndPersistToDisk()
        {
            int i = 0; int j = 0;
            var esasContainer = _esasWebServiceContextFactory.Create();

            List<Kvalifikationskriterie> loadedObjectsListe = new List<Kvalifikationskriterie>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Kvalifikationskriterie> token = null;

            var response = esasContainer.Kvalifikationskriterie.Execute() as Microsoft.OData.Client.QueryOperationResponse<Kvalifikationskriterie>;
            loadedObjectsListe.AddRange(response.ToList());
            if ( loadedObjectsListe.Any())
            {
                j = loadedObjectsListe.Count();
                PersistToFile(i,j,ref loadedObjectsListe);
            }
            else
                return null;
            
            System.Diagnostics.Debug.WriteLine(loadedObjectsListe.Count());
         
            while ((token = response.GetContinuation()) != null)
            {
               Stopwatch st = new Stopwatch();
                st.Start();
                       esasContainer = _esasWebServiceContextFactory.Create();
                List<Kvalifikationskriterie> loadedObjectsListen = new List<Kvalifikationskriterie>();
                response = esasContainer.Execute<Kvalifikationskriterie>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Kvalifikationskriterie>;
                loadedObjectsListen.AddRange(response.ToList());
                if ( loadedObjectsListen.Any())
            {
                i = j;
                j = j + loadedObjectsListen.Count();
                PersistToFile(i,j,ref loadedObjectsListen);
                loadedObjectsListen = null;
            }


                // loadedObjectsListe.AddRange(response.ToList()); - only persist to disc, for now.
               st.Stop();
               System.Diagnostics.Debug.WriteLine(j + "-" + st.Elapsed.Seconds);
            }
            
             System.Diagnostics.Debug.WriteLine($"*** total objects written to disk was {j} ***");
            //return loadedObjectsListe.ToArray();
            return null;
        }

        
        private void PersistToFile(int indexToStartLoadFrom, int howManyRecordsToGet, ref List<Kvalifikationskriterie> loadedObjectsListe)
        {
             string entityName = "Kvalifikationskriterie";
              if (!Directory.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}"))
                Directory.CreateDirectory($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}");

            string objectsJson = JsonConvert.SerializeObject(loadedObjectsListe, Formatting.Indented);
            if ( !File.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json"))
                File.WriteAllText($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json", objectsJson );

        }


    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class KvalifikationspointLoadStrategy : BaseLoadStrategy
    {
         public KvalifikationspointLoadStrategy(EsasWsContextFactory esasWebServiceContextFactory, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasWebServiceContextFactory, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff)
        {
            var esasContainer = _esasWebServiceContextFactory.Create();
            // design continuation here
            List<Kvalifikationspoint> loadedObjectsListe = new List<Kvalifikationspoint>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Kvalifikationspoint> token = null;

            string datetimeoffSetFromLoadtimeCutOff = loadTimeCutoff.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var response = esasContainer.Kvalifikationspoint.AddQueryOption("$filter", $"ModifiedOn gt {datetimeoffSetFromLoadtimeCutOff}").Execute() as Microsoft.OData.Client.QueryOperationResponse<Kvalifikationspoint>;

            loadedObjectsListe.AddRange(response.ToList());
            if ( ! loadedObjectsListe.Any())
                return null;
            
            while ((token = response.GetContinuation()) != null)
            {
                System.Diagnostics.Debug.WriteLine("Continuation-token available, retrieving next batch.");
                esasContainer = _esasWebServiceContextFactory.Create();
                response = esasContainer.Execute<Kvalifikationspoint>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Kvalifikationspoint>;
                loadedObjectsListe.AddRange(response.ToList());
            }

            return loadedObjectsListe.ToArray();
        }

        /// <summary>
        /// This method is only used for initial load of ESAS data - will become useless once we're in production.
        /// </summary>
        protected override object[] LoadObjectsAndPersistToDisk()
        {
            int i = 0; int j = 0;
            var esasContainer = _esasWebServiceContextFactory.Create();

            List<Kvalifikationspoint> loadedObjectsListe = new List<Kvalifikationspoint>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Kvalifikationspoint> token = null;

            var response = esasContainer.Kvalifikationspoint.Execute() as Microsoft.OData.Client.QueryOperationResponse<Kvalifikationspoint>;
            loadedObjectsListe.AddRange(response.ToList());
            if ( loadedObjectsListe.Any())
            {
                j = loadedObjectsListe.Count();
                PersistToFile(i,j,ref loadedObjectsListe);
            }
            else
                return null;
            
            System.Diagnostics.Debug.WriteLine(loadedObjectsListe.Count());
         
            while ((token = response.GetContinuation()) != null)
            {
               Stopwatch st = new Stopwatch();
                st.Start();
                       esasContainer = _esasWebServiceContextFactory.Create();
                List<Kvalifikationspoint> loadedObjectsListen = new List<Kvalifikationspoint>();
                response = esasContainer.Execute<Kvalifikationspoint>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Kvalifikationspoint>;
                loadedObjectsListen.AddRange(response.ToList());
                if ( loadedObjectsListen.Any())
            {
                i = j;
                j = j + loadedObjectsListen.Count();
                PersistToFile(i,j,ref loadedObjectsListen);
                loadedObjectsListen = null;
            }


                // loadedObjectsListe.AddRange(response.ToList()); - only persist to disc, for now.
               st.Stop();
               System.Diagnostics.Debug.WriteLine(j + "-" + st.Elapsed.Seconds);
            }
            
             System.Diagnostics.Debug.WriteLine($"*** total objects written to disk was {j} ***");
            //return loadedObjectsListe.ToArray();
            return null;
        }

        
        private void PersistToFile(int indexToStartLoadFrom, int howManyRecordsToGet, ref List<Kvalifikationspoint> loadedObjectsListe)
        {
             string entityName = "Kvalifikationspoint";
              if (!Directory.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}"))
                Directory.CreateDirectory($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}");

            string objectsJson = JsonConvert.SerializeObject(loadedObjectsListe, Formatting.Indented);
            if ( !File.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json"))
                File.WriteAllText($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json", objectsJson );

        }


    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class LandLoadStrategy : BaseLoadStrategy
    {
         public LandLoadStrategy(EsasWsContextFactory esasWebServiceContextFactory, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasWebServiceContextFactory, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff)
        {
            var esasContainer = _esasWebServiceContextFactory.Create();
            // design continuation here
            List<Land> loadedObjectsListe = new List<Land>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Land> token = null;

            string datetimeoffSetFromLoadtimeCutOff = loadTimeCutoff.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var response = esasContainer.Land.AddQueryOption("$filter", $"ModifiedOn gt {datetimeoffSetFromLoadtimeCutOff}").Execute() as Microsoft.OData.Client.QueryOperationResponse<Land>;

            loadedObjectsListe.AddRange(response.ToList());
            if ( ! loadedObjectsListe.Any())
                return null;
            
            while ((token = response.GetContinuation()) != null)
            {
                System.Diagnostics.Debug.WriteLine("Continuation-token available, retrieving next batch.");
                esasContainer = _esasWebServiceContextFactory.Create();
                response = esasContainer.Execute<Land>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Land>;
                loadedObjectsListe.AddRange(response.ToList());
            }

            return loadedObjectsListe.ToArray();
        }

        /// <summary>
        /// This method is only used for initial load of ESAS data - will become useless once we're in production.
        /// </summary>
        protected override object[] LoadObjectsAndPersistToDisk()
        {
            int i = 0; int j = 0;
            var esasContainer = _esasWebServiceContextFactory.Create();

            List<Land> loadedObjectsListe = new List<Land>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Land> token = null;

            var response = esasContainer.Land.Execute() as Microsoft.OData.Client.QueryOperationResponse<Land>;
            loadedObjectsListe.AddRange(response.ToList());
            if ( loadedObjectsListe.Any())
            {
                j = loadedObjectsListe.Count();
                PersistToFile(i,j,ref loadedObjectsListe);
            }
            else
                return null;
            
            System.Diagnostics.Debug.WriteLine(loadedObjectsListe.Count());
         
            while ((token = response.GetContinuation()) != null)
            {
               Stopwatch st = new Stopwatch();
                st.Start();
                       esasContainer = _esasWebServiceContextFactory.Create();
                List<Land> loadedObjectsListen = new List<Land>();
                response = esasContainer.Execute<Land>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Land>;
                loadedObjectsListen.AddRange(response.ToList());
                if ( loadedObjectsListen.Any())
            {
                i = j;
                j = j + loadedObjectsListen.Count();
                PersistToFile(i,j,ref loadedObjectsListen);
                loadedObjectsListen = null;
            }


                // loadedObjectsListe.AddRange(response.ToList()); - only persist to disc, for now.
               st.Stop();
               System.Diagnostics.Debug.WriteLine(j + "-" + st.Elapsed.Seconds);
            }
            
             System.Diagnostics.Debug.WriteLine($"*** total objects written to disk was {j} ***");
            //return loadedObjectsListe.ToArray();
            return null;
        }

        
        private void PersistToFile(int indexToStartLoadFrom, int howManyRecordsToGet, ref List<Land> loadedObjectsListe)
        {
             string entityName = "Land";
              if (!Directory.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}"))
                Directory.CreateDirectory($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}");

            string objectsJson = JsonConvert.SerializeObject(loadedObjectsListe, Formatting.Indented);
            if ( !File.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json"))
                File.WriteAllText($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json", objectsJson );

        }


    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class MeritRegistreringLoadStrategy : BaseLoadStrategy
    {
         public MeritRegistreringLoadStrategy(EsasWsContextFactory esasWebServiceContextFactory, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasWebServiceContextFactory, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff)
        {
            var esasContainer = _esasWebServiceContextFactory.Create();
            // design continuation here
            List<MeritRegistrering> loadedObjectsListe = new List<MeritRegistrering>();
            Microsoft.OData.Client.DataServiceQueryContinuation<MeritRegistrering> token = null;

            string datetimeoffSetFromLoadtimeCutOff = loadTimeCutoff.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var response = esasContainer.MeritRegistrering.AddQueryOption("$filter", $"ModifiedOn gt {datetimeoffSetFromLoadtimeCutOff}").Execute() as Microsoft.OData.Client.QueryOperationResponse<MeritRegistrering>;

            loadedObjectsListe.AddRange(response.ToList());
            if ( ! loadedObjectsListe.Any())
                return null;
            
            while ((token = response.GetContinuation()) != null)
            {
                System.Diagnostics.Debug.WriteLine("Continuation-token available, retrieving next batch.");
                esasContainer = _esasWebServiceContextFactory.Create();
                response = esasContainer.Execute<MeritRegistrering>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<MeritRegistrering>;
                loadedObjectsListe.AddRange(response.ToList());
            }

            return loadedObjectsListe.ToArray();
        }

        /// <summary>
        /// This method is only used for initial load of ESAS data - will become useless once we're in production.
        /// </summary>
        protected override object[] LoadObjectsAndPersistToDisk()
        {
            int i = 0; int j = 0;
            var esasContainer = _esasWebServiceContextFactory.Create();

            List<MeritRegistrering> loadedObjectsListe = new List<MeritRegistrering>();
            Microsoft.OData.Client.DataServiceQueryContinuation<MeritRegistrering> token = null;

            var response = esasContainer.MeritRegistrering.Execute() as Microsoft.OData.Client.QueryOperationResponse<MeritRegistrering>;
            loadedObjectsListe.AddRange(response.ToList());
            if ( loadedObjectsListe.Any())
            {
                j = loadedObjectsListe.Count();
                PersistToFile(i,j,ref loadedObjectsListe);
            }
            else
                return null;
            
            System.Diagnostics.Debug.WriteLine(loadedObjectsListe.Count());
         
            while ((token = response.GetContinuation()) != null)
            {
               Stopwatch st = new Stopwatch();
                st.Start();
                       esasContainer = _esasWebServiceContextFactory.Create();
                List<MeritRegistrering> loadedObjectsListen = new List<MeritRegistrering>();
                response = esasContainer.Execute<MeritRegistrering>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<MeritRegistrering>;
                loadedObjectsListen.AddRange(response.ToList());
                if ( loadedObjectsListen.Any())
            {
                i = j;
                j = j + loadedObjectsListen.Count();
                PersistToFile(i,j,ref loadedObjectsListen);
                loadedObjectsListen = null;
            }


                // loadedObjectsListe.AddRange(response.ToList()); - only persist to disc, for now.
               st.Stop();
               System.Diagnostics.Debug.WriteLine(j + "-" + st.Elapsed.Seconds);
            }
            
             System.Diagnostics.Debug.WriteLine($"*** total objects written to disk was {j} ***");
            //return loadedObjectsListe.ToArray();
            return null;
        }

        
        private void PersistToFile(int indexToStartLoadFrom, int howManyRecordsToGet, ref List<MeritRegistrering> loadedObjectsListe)
        {
             string entityName = "MeritRegistrering";
              if (!Directory.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}"))
                Directory.CreateDirectory($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}");

            string objectsJson = JsonConvert.SerializeObject(loadedObjectsListe, Formatting.Indented);
            if ( !File.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json"))
                File.WriteAllText($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json", objectsJson );

        }


    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class NationalAfgangsaarsagLoadStrategy : BaseLoadStrategy
    {
         public NationalAfgangsaarsagLoadStrategy(EsasWsContextFactory esasWebServiceContextFactory, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasWebServiceContextFactory, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff)
        {
            var esasContainer = _esasWebServiceContextFactory.Create();
            // design continuation here
            List<NationalAfgangsaarsag> loadedObjectsListe = new List<NationalAfgangsaarsag>();
            Microsoft.OData.Client.DataServiceQueryContinuation<NationalAfgangsaarsag> token = null;

            string datetimeoffSetFromLoadtimeCutOff = loadTimeCutoff.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var response = esasContainer.NationalAfgangsaarsag.AddQueryOption("$filter", $"ModifiedOn gt {datetimeoffSetFromLoadtimeCutOff}").Execute() as Microsoft.OData.Client.QueryOperationResponse<NationalAfgangsaarsag>;

            loadedObjectsListe.AddRange(response.ToList());
            if ( ! loadedObjectsListe.Any())
                return null;
            
            while ((token = response.GetContinuation()) != null)
            {
                System.Diagnostics.Debug.WriteLine("Continuation-token available, retrieving next batch.");
                esasContainer = _esasWebServiceContextFactory.Create();
                response = esasContainer.Execute<NationalAfgangsaarsag>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<NationalAfgangsaarsag>;
                loadedObjectsListe.AddRange(response.ToList());
            }

            return loadedObjectsListe.ToArray();
        }

        /// <summary>
        /// This method is only used for initial load of ESAS data - will become useless once we're in production.
        /// </summary>
        protected override object[] LoadObjectsAndPersistToDisk()
        {
            int i = 0; int j = 0;
            var esasContainer = _esasWebServiceContextFactory.Create();

            List<NationalAfgangsaarsag> loadedObjectsListe = new List<NationalAfgangsaarsag>();
            Microsoft.OData.Client.DataServiceQueryContinuation<NationalAfgangsaarsag> token = null;

            var response = esasContainer.NationalAfgangsaarsag.Execute() as Microsoft.OData.Client.QueryOperationResponse<NationalAfgangsaarsag>;
            loadedObjectsListe.AddRange(response.ToList());
            if ( loadedObjectsListe.Any())
            {
                j = loadedObjectsListe.Count();
                PersistToFile(i,j,ref loadedObjectsListe);
            }
            else
                return null;
            
            System.Diagnostics.Debug.WriteLine(loadedObjectsListe.Count());
         
            while ((token = response.GetContinuation()) != null)
            {
               Stopwatch st = new Stopwatch();
                st.Start();
                       esasContainer = _esasWebServiceContextFactory.Create();
                List<NationalAfgangsaarsag> loadedObjectsListen = new List<NationalAfgangsaarsag>();
                response = esasContainer.Execute<NationalAfgangsaarsag>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<NationalAfgangsaarsag>;
                loadedObjectsListen.AddRange(response.ToList());
                if ( loadedObjectsListen.Any())
            {
                i = j;
                j = j + loadedObjectsListen.Count();
                PersistToFile(i,j,ref loadedObjectsListen);
                loadedObjectsListen = null;
            }


                // loadedObjectsListe.AddRange(response.ToList()); - only persist to disc, for now.
               st.Stop();
               System.Diagnostics.Debug.WriteLine(j + "-" + st.Elapsed.Seconds);
            }
            
             System.Diagnostics.Debug.WriteLine($"*** total objects written to disk was {j} ***");
            //return loadedObjectsListe.ToArray();
            return null;
        }

        
        private void PersistToFile(int indexToStartLoadFrom, int howManyRecordsToGet, ref List<NationalAfgangsaarsag> loadedObjectsListe)
        {
             string entityName = "NationalAfgangsaarsag";
              if (!Directory.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}"))
                Directory.CreateDirectory($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}");

            string objectsJson = JsonConvert.SerializeObject(loadedObjectsListe, Formatting.Indented);
            if ( !File.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json"))
                File.WriteAllText($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json", objectsJson );

        }


    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class OmraadenummerLoadStrategy : BaseLoadStrategy
    {
         public OmraadenummerLoadStrategy(EsasWsContextFactory esasWebServiceContextFactory, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasWebServiceContextFactory, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff)
        {
            var esasContainer = _esasWebServiceContextFactory.Create();
            // design continuation here
            List<Omraadenummer> loadedObjectsListe = new List<Omraadenummer>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Omraadenummer> token = null;

            string datetimeoffSetFromLoadtimeCutOff = loadTimeCutoff.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var response = esasContainer.Omraadenummer.AddQueryOption("$filter", $"ModifiedOn gt {datetimeoffSetFromLoadtimeCutOff}").Execute() as Microsoft.OData.Client.QueryOperationResponse<Omraadenummer>;

            loadedObjectsListe.AddRange(response.ToList());
            if ( ! loadedObjectsListe.Any())
                return null;
            
            while ((token = response.GetContinuation()) != null)
            {
                System.Diagnostics.Debug.WriteLine("Continuation-token available, retrieving next batch.");
                esasContainer = _esasWebServiceContextFactory.Create();
                response = esasContainer.Execute<Omraadenummer>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Omraadenummer>;
                loadedObjectsListe.AddRange(response.ToList());
            }

            return loadedObjectsListe.ToArray();
        }

        /// <summary>
        /// This method is only used for initial load of ESAS data - will become useless once we're in production.
        /// </summary>
        protected override object[] LoadObjectsAndPersistToDisk()
        {
            int i = 0; int j = 0;
            var esasContainer = _esasWebServiceContextFactory.Create();

            List<Omraadenummer> loadedObjectsListe = new List<Omraadenummer>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Omraadenummer> token = null;

            var response = esasContainer.Omraadenummer.Execute() as Microsoft.OData.Client.QueryOperationResponse<Omraadenummer>;
            loadedObjectsListe.AddRange(response.ToList());
            if ( loadedObjectsListe.Any())
            {
                j = loadedObjectsListe.Count();
                PersistToFile(i,j,ref loadedObjectsListe);
            }
            else
                return null;
            
            System.Diagnostics.Debug.WriteLine(loadedObjectsListe.Count());
         
            while ((token = response.GetContinuation()) != null)
            {
               Stopwatch st = new Stopwatch();
                st.Start();
                       esasContainer = _esasWebServiceContextFactory.Create();
                List<Omraadenummer> loadedObjectsListen = new List<Omraadenummer>();
                response = esasContainer.Execute<Omraadenummer>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Omraadenummer>;
                loadedObjectsListen.AddRange(response.ToList());
                if ( loadedObjectsListen.Any())
            {
                i = j;
                j = j + loadedObjectsListen.Count();
                PersistToFile(i,j,ref loadedObjectsListen);
                loadedObjectsListen = null;
            }


                // loadedObjectsListe.AddRange(response.ToList()); - only persist to disc, for now.
               st.Stop();
               System.Diagnostics.Debug.WriteLine(j + "-" + st.Elapsed.Seconds);
            }
            
             System.Diagnostics.Debug.WriteLine($"*** total objects written to disk was {j} ***");
            //return loadedObjectsListe.ToArray();
            return null;
        }

        
        private void PersistToFile(int indexToStartLoadFrom, int howManyRecordsToGet, ref List<Omraadenummer> loadedObjectsListe)
        {
             string entityName = "Omraadenummer";
              if (!Directory.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}"))
                Directory.CreateDirectory($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}");

            string objectsJson = JsonConvert.SerializeObject(loadedObjectsListe, Formatting.Indented);
            if ( !File.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json"))
                File.WriteAllText($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json", objectsJson );

        }


    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class OmraadenummeropsaetningLoadStrategy : BaseLoadStrategy
    {
         public OmraadenummeropsaetningLoadStrategy(EsasWsContextFactory esasWebServiceContextFactory, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasWebServiceContextFactory, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff)
        {
            var esasContainer = _esasWebServiceContextFactory.Create();
            // design continuation here
            List<Omraadenummeropsaetning> loadedObjectsListe = new List<Omraadenummeropsaetning>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Omraadenummeropsaetning> token = null;

            string datetimeoffSetFromLoadtimeCutOff = loadTimeCutoff.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var response = esasContainer.Omraadenummeropsaetning.AddQueryOption("$filter", $"ModifiedOn gt {datetimeoffSetFromLoadtimeCutOff}").Execute() as Microsoft.OData.Client.QueryOperationResponse<Omraadenummeropsaetning>;

            loadedObjectsListe.AddRange(response.ToList());
            if ( ! loadedObjectsListe.Any())
                return null;
            
            while ((token = response.GetContinuation()) != null)
            {
                System.Diagnostics.Debug.WriteLine("Continuation-token available, retrieving next batch.");
                esasContainer = _esasWebServiceContextFactory.Create();
                response = esasContainer.Execute<Omraadenummeropsaetning>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Omraadenummeropsaetning>;
                loadedObjectsListe.AddRange(response.ToList());
            }

            return loadedObjectsListe.ToArray();
        }

        /// <summary>
        /// This method is only used for initial load of ESAS data - will become useless once we're in production.
        /// </summary>
        protected override object[] LoadObjectsAndPersistToDisk()
        {
            int i = 0; int j = 0;
            var esasContainer = _esasWebServiceContextFactory.Create();

            List<Omraadenummeropsaetning> loadedObjectsListe = new List<Omraadenummeropsaetning>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Omraadenummeropsaetning> token = null;

            var response = esasContainer.Omraadenummeropsaetning.Execute() as Microsoft.OData.Client.QueryOperationResponse<Omraadenummeropsaetning>;
            loadedObjectsListe.AddRange(response.ToList());
            if ( loadedObjectsListe.Any())
            {
                j = loadedObjectsListe.Count();
                PersistToFile(i,j,ref loadedObjectsListe);
            }
            else
                return null;
            
            System.Diagnostics.Debug.WriteLine(loadedObjectsListe.Count());
         
            while ((token = response.GetContinuation()) != null)
            {
               Stopwatch st = new Stopwatch();
                st.Start();
                       esasContainer = _esasWebServiceContextFactory.Create();
                List<Omraadenummeropsaetning> loadedObjectsListen = new List<Omraadenummeropsaetning>();
                response = esasContainer.Execute<Omraadenummeropsaetning>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Omraadenummeropsaetning>;
                loadedObjectsListen.AddRange(response.ToList());
                if ( loadedObjectsListen.Any())
            {
                i = j;
                j = j + loadedObjectsListen.Count();
                PersistToFile(i,j,ref loadedObjectsListen);
                loadedObjectsListen = null;
            }


                // loadedObjectsListe.AddRange(response.ToList()); - only persist to disc, for now.
               st.Stop();
               System.Diagnostics.Debug.WriteLine(j + "-" + st.Elapsed.Seconds);
            }
            
             System.Diagnostics.Debug.WriteLine($"*** total objects written to disk was {j} ***");
            //return loadedObjectsListe.ToArray();
            return null;
        }

        
        private void PersistToFile(int indexToStartLoadFrom, int howManyRecordsToGet, ref List<Omraadenummeropsaetning> loadedObjectsListe)
        {
             string entityName = "Omraadenummeropsaetning";
              if (!Directory.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}"))
                Directory.CreateDirectory($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}");

            string objectsJson = JsonConvert.SerializeObject(loadedObjectsListe, Formatting.Indented);
            if ( !File.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json"))
                File.WriteAllText($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json", objectsJson );

        }


    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class OmraadespecialiseringLoadStrategy : BaseLoadStrategy
    {
         public OmraadespecialiseringLoadStrategy(EsasWsContextFactory esasWebServiceContextFactory, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasWebServiceContextFactory, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff)
        {
            var esasContainer = _esasWebServiceContextFactory.Create();
            // design continuation here
            List<Omraadespecialisering> loadedObjectsListe = new List<Omraadespecialisering>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Omraadespecialisering> token = null;

            string datetimeoffSetFromLoadtimeCutOff = loadTimeCutoff.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var response = esasContainer.Omraadespecialisering.AddQueryOption("$filter", $"ModifiedOn gt {datetimeoffSetFromLoadtimeCutOff}").Execute() as Microsoft.OData.Client.QueryOperationResponse<Omraadespecialisering>;

            loadedObjectsListe.AddRange(response.ToList());
            if ( ! loadedObjectsListe.Any())
                return null;
            
            while ((token = response.GetContinuation()) != null)
            {
                System.Diagnostics.Debug.WriteLine("Continuation-token available, retrieving next batch.");
                esasContainer = _esasWebServiceContextFactory.Create();
                response = esasContainer.Execute<Omraadespecialisering>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Omraadespecialisering>;
                loadedObjectsListe.AddRange(response.ToList());
            }

            return loadedObjectsListe.ToArray();
        }

        /// <summary>
        /// This method is only used for initial load of ESAS data - will become useless once we're in production.
        /// </summary>
        protected override object[] LoadObjectsAndPersistToDisk()
        {
            int i = 0; int j = 0;
            var esasContainer = _esasWebServiceContextFactory.Create();

            List<Omraadespecialisering> loadedObjectsListe = new List<Omraadespecialisering>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Omraadespecialisering> token = null;

            var response = esasContainer.Omraadespecialisering.Execute() as Microsoft.OData.Client.QueryOperationResponse<Omraadespecialisering>;
            loadedObjectsListe.AddRange(response.ToList());
            if ( loadedObjectsListe.Any())
            {
                j = loadedObjectsListe.Count();
                PersistToFile(i,j,ref loadedObjectsListe);
            }
            else
                return null;
            
            System.Diagnostics.Debug.WriteLine(loadedObjectsListe.Count());
         
            while ((token = response.GetContinuation()) != null)
            {
               Stopwatch st = new Stopwatch();
                st.Start();
                       esasContainer = _esasWebServiceContextFactory.Create();
                List<Omraadespecialisering> loadedObjectsListen = new List<Omraadespecialisering>();
                response = esasContainer.Execute<Omraadespecialisering>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Omraadespecialisering>;
                loadedObjectsListen.AddRange(response.ToList());
                if ( loadedObjectsListen.Any())
            {
                i = j;
                j = j + loadedObjectsListen.Count();
                PersistToFile(i,j,ref loadedObjectsListen);
                loadedObjectsListen = null;
            }


                // loadedObjectsListe.AddRange(response.ToList()); - only persist to disc, for now.
               st.Stop();
               System.Diagnostics.Debug.WriteLine(j + "-" + st.Elapsed.Seconds);
            }
            
             System.Diagnostics.Debug.WriteLine($"*** total objects written to disk was {j} ***");
            //return loadedObjectsListe.ToArray();
            return null;
        }

        
        private void PersistToFile(int indexToStartLoadFrom, int howManyRecordsToGet, ref List<Omraadespecialisering> loadedObjectsListe)
        {
             string entityName = "Omraadespecialisering";
              if (!Directory.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}"))
                Directory.CreateDirectory($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}");

            string objectsJson = JsonConvert.SerializeObject(loadedObjectsListe, Formatting.Indented);
            if ( !File.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json"))
                File.WriteAllText($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json", objectsJson );

        }


    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class PersonLoadStrategy : BaseLoadStrategy
    {
         public PersonLoadStrategy(EsasWsContextFactory esasWebServiceContextFactory, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasWebServiceContextFactory, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff)
        {
            var esasContainer = _esasWebServiceContextFactory.Create();
            // design continuation here
            List<Person> loadedObjectsListe = new List<Person>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Person> token = null;

            string datetimeoffSetFromLoadtimeCutOff = loadTimeCutoff.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var response = esasContainer.Person.AddQueryOption("$filter", $"ModifiedOn gt {datetimeoffSetFromLoadtimeCutOff}").Execute() as Microsoft.OData.Client.QueryOperationResponse<Person>;

            loadedObjectsListe.AddRange(response.ToList());
            if ( ! loadedObjectsListe.Any())
                return null;
            
            while ((token = response.GetContinuation()) != null)
            {
                System.Diagnostics.Debug.WriteLine("Continuation-token available, retrieving next batch.");
                esasContainer = _esasWebServiceContextFactory.Create();
                response = esasContainer.Execute<Person>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Person>;
                loadedObjectsListe.AddRange(response.ToList());
            }

            return loadedObjectsListe.ToArray();
        }

        /// <summary>
        /// This method is only used for initial load of ESAS data - will become useless once we're in production.
        /// </summary>
        protected override object[] LoadObjectsAndPersistToDisk()
        {
            int i = 0; int j = 0;
            var esasContainer = _esasWebServiceContextFactory.Create();

            List<Person> loadedObjectsListe = new List<Person>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Person> token = null;

            var response = esasContainer.Person.Execute() as Microsoft.OData.Client.QueryOperationResponse<Person>;
            loadedObjectsListe.AddRange(response.ToList());
            if ( loadedObjectsListe.Any())
            {
                j = loadedObjectsListe.Count();
                PersistToFile(i,j,ref loadedObjectsListe);
            }
            else
                return null;
            
            System.Diagnostics.Debug.WriteLine(loadedObjectsListe.Count());
         
            while ((token = response.GetContinuation()) != null)
            {
               Stopwatch st = new Stopwatch();
                st.Start();
                       esasContainer = _esasWebServiceContextFactory.Create();
                List<Person> loadedObjectsListen = new List<Person>();
                response = esasContainer.Execute<Person>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Person>;
                loadedObjectsListen.AddRange(response.ToList());
                if ( loadedObjectsListen.Any())
            {
                i = j;
                j = j + loadedObjectsListen.Count();
                PersistToFile(i,j,ref loadedObjectsListen);
                loadedObjectsListen = null;
            }


                // loadedObjectsListe.AddRange(response.ToList()); - only persist to disc, for now.
               st.Stop();
               System.Diagnostics.Debug.WriteLine(j + "-" + st.Elapsed.Seconds);
            }
            
             System.Diagnostics.Debug.WriteLine($"*** total objects written to disk was {j} ***");
            //return loadedObjectsListe.ToArray();
            return null;
        }

        
        private void PersistToFile(int indexToStartLoadFrom, int howManyRecordsToGet, ref List<Person> loadedObjectsListe)
        {
             string entityName = "Person";
              if (!Directory.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}"))
                Directory.CreateDirectory($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}");

            string objectsJson = JsonConvert.SerializeObject(loadedObjectsListe, Formatting.Indented);
            if ( !File.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json"))
                File.WriteAllText($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json", objectsJson );

        }


    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class PersonoplysningLoadStrategy : BaseLoadStrategy
    {
         public PersonoplysningLoadStrategy(EsasWsContextFactory esasWebServiceContextFactory, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasWebServiceContextFactory, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff)
        {
            var esasContainer = _esasWebServiceContextFactory.Create();
            // design continuation here
            List<Personoplysning> loadedObjectsListe = new List<Personoplysning>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Personoplysning> token = null;

            string datetimeoffSetFromLoadtimeCutOff = loadTimeCutoff.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var response = esasContainer.Personoplysning.AddQueryOption("$filter", $"ModifiedOn gt {datetimeoffSetFromLoadtimeCutOff}").Execute() as Microsoft.OData.Client.QueryOperationResponse<Personoplysning>;

            loadedObjectsListe.AddRange(response.ToList());
            if ( ! loadedObjectsListe.Any())
                return null;
            
            while ((token = response.GetContinuation()) != null)
            {
                System.Diagnostics.Debug.WriteLine("Continuation-token available, retrieving next batch.");
                esasContainer = _esasWebServiceContextFactory.Create();
                response = esasContainer.Execute<Personoplysning>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Personoplysning>;
                loadedObjectsListe.AddRange(response.ToList());
            }

            return loadedObjectsListe.ToArray();
        }

        /// <summary>
        /// This method is only used for initial load of ESAS data - will become useless once we're in production.
        /// </summary>
        protected override object[] LoadObjectsAndPersistToDisk()
        {
            int i = 0; int j = 0;
            var esasContainer = _esasWebServiceContextFactory.Create();

            List<Personoplysning> loadedObjectsListe = new List<Personoplysning>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Personoplysning> token = null;

            var response = esasContainer.Personoplysning.Execute() as Microsoft.OData.Client.QueryOperationResponse<Personoplysning>;
            loadedObjectsListe.AddRange(response.ToList());
            if ( loadedObjectsListe.Any())
            {
                j = loadedObjectsListe.Count();
                PersistToFile(i,j,ref loadedObjectsListe);
            }
            else
                return null;
            
            System.Diagnostics.Debug.WriteLine(loadedObjectsListe.Count());
         
            while ((token = response.GetContinuation()) != null)
            {
               Stopwatch st = new Stopwatch();
                st.Start();
                       esasContainer = _esasWebServiceContextFactory.Create();
                List<Personoplysning> loadedObjectsListen = new List<Personoplysning>();
                response = esasContainer.Execute<Personoplysning>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Personoplysning>;
                loadedObjectsListen.AddRange(response.ToList());
                if ( loadedObjectsListen.Any())
            {
                i = j;
                j = j + loadedObjectsListen.Count();
                PersistToFile(i,j,ref loadedObjectsListen);
                loadedObjectsListen = null;
            }


                // loadedObjectsListe.AddRange(response.ToList()); - only persist to disc, for now.
               st.Stop();
               System.Diagnostics.Debug.WriteLine(j + "-" + st.Elapsed.Seconds);
            }
            
             System.Diagnostics.Debug.WriteLine($"*** total objects written to disk was {j} ***");
            //return loadedObjectsListe.ToArray();
            return null;
        }

        
        private void PersistToFile(int indexToStartLoadFrom, int howManyRecordsToGet, ref List<Personoplysning> loadedObjectsListe)
        {
             string entityName = "Personoplysning";
              if (!Directory.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}"))
                Directory.CreateDirectory($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}");

            string objectsJson = JsonConvert.SerializeObject(loadedObjectsListe, Formatting.Indented);
            if ( !File.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json"))
                File.WriteAllText($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json", objectsJson );

        }


    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class PlanlaegningsUddannelseselementLoadStrategy : BaseLoadStrategy
    {
         public PlanlaegningsUddannelseselementLoadStrategy(EsasWsContextFactory esasWebServiceContextFactory, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasWebServiceContextFactory, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff)
        {
            var esasContainer = _esasWebServiceContextFactory.Create();
            // design continuation here
            List<PlanlaegningsUddannelseselement> loadedObjectsListe = new List<PlanlaegningsUddannelseselement>();
            Microsoft.OData.Client.DataServiceQueryContinuation<PlanlaegningsUddannelseselement> token = null;

            string datetimeoffSetFromLoadtimeCutOff = loadTimeCutoff.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var response = esasContainer.PlanlaegningsUddannelseselement.AddQueryOption("$filter", $"ModifiedOn gt {datetimeoffSetFromLoadtimeCutOff}").Execute() as Microsoft.OData.Client.QueryOperationResponse<PlanlaegningsUddannelseselement>;

            loadedObjectsListe.AddRange(response.ToList());
            if ( ! loadedObjectsListe.Any())
                return null;
            
            while ((token = response.GetContinuation()) != null)
            {
                System.Diagnostics.Debug.WriteLine("Continuation-token available, retrieving next batch.");
                esasContainer = _esasWebServiceContextFactory.Create();
                response = esasContainer.Execute<PlanlaegningsUddannelseselement>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<PlanlaegningsUddannelseselement>;
                loadedObjectsListe.AddRange(response.ToList());
            }

            return loadedObjectsListe.ToArray();
        }

        /// <summary>
        /// This method is only used for initial load of ESAS data - will become useless once we're in production.
        /// </summary>
        protected override object[] LoadObjectsAndPersistToDisk()
        {
            int i = 0; int j = 0;
            var esasContainer = _esasWebServiceContextFactory.Create();

            List<PlanlaegningsUddannelseselement> loadedObjectsListe = new List<PlanlaegningsUddannelseselement>();
            Microsoft.OData.Client.DataServiceQueryContinuation<PlanlaegningsUddannelseselement> token = null;

            var response = esasContainer.PlanlaegningsUddannelseselement.Execute() as Microsoft.OData.Client.QueryOperationResponse<PlanlaegningsUddannelseselement>;
            loadedObjectsListe.AddRange(response.ToList());
            if ( loadedObjectsListe.Any())
            {
                j = loadedObjectsListe.Count();
                PersistToFile(i,j,ref loadedObjectsListe);
            }
            else
                return null;
            
            System.Diagnostics.Debug.WriteLine(loadedObjectsListe.Count());
         
            while ((token = response.GetContinuation()) != null)
            {
               Stopwatch st = new Stopwatch();
                st.Start();
                       esasContainer = _esasWebServiceContextFactory.Create();
                List<PlanlaegningsUddannelseselement> loadedObjectsListen = new List<PlanlaegningsUddannelseselement>();
                response = esasContainer.Execute<PlanlaegningsUddannelseselement>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<PlanlaegningsUddannelseselement>;
                loadedObjectsListen.AddRange(response.ToList());
                if ( loadedObjectsListen.Any())
            {
                i = j;
                j = j + loadedObjectsListen.Count();
                PersistToFile(i,j,ref loadedObjectsListen);
                loadedObjectsListen = null;
            }


                // loadedObjectsListe.AddRange(response.ToList()); - only persist to disc, for now.
               st.Stop();
               System.Diagnostics.Debug.WriteLine(j + "-" + st.Elapsed.Seconds);
            }
            
             System.Diagnostics.Debug.WriteLine($"*** total objects written to disk was {j} ***");
            //return loadedObjectsListe.ToArray();
            return null;
        }

        
        private void PersistToFile(int indexToStartLoadFrom, int howManyRecordsToGet, ref List<PlanlaegningsUddannelseselement> loadedObjectsListe)
        {
             string entityName = "PlanlaegningsUddannelseselement";
              if (!Directory.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}"))
                Directory.CreateDirectory($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}");

            string objectsJson = JsonConvert.SerializeObject(loadedObjectsListe, Formatting.Indented);
            if ( !File.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json"))
                File.WriteAllText($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json", objectsJson );

        }


    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class PostnummerLoadStrategy : BaseLoadStrategy
    {
         public PostnummerLoadStrategy(EsasWsContextFactory esasWebServiceContextFactory, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasWebServiceContextFactory, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff)
        {
            var esasContainer = _esasWebServiceContextFactory.Create();
            // design continuation here
            List<Postnummer> loadedObjectsListe = new List<Postnummer>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Postnummer> token = null;

            string datetimeoffSetFromLoadtimeCutOff = loadTimeCutoff.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var response = esasContainer.Postnummer.AddQueryOption("$filter", $"ModifiedOn gt {datetimeoffSetFromLoadtimeCutOff}").Execute() as Microsoft.OData.Client.QueryOperationResponse<Postnummer>;

            loadedObjectsListe.AddRange(response.ToList());
            if ( ! loadedObjectsListe.Any())
                return null;
            
            while ((token = response.GetContinuation()) != null)
            {
                System.Diagnostics.Debug.WriteLine("Continuation-token available, retrieving next batch.");
                esasContainer = _esasWebServiceContextFactory.Create();
                response = esasContainer.Execute<Postnummer>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Postnummer>;
                loadedObjectsListe.AddRange(response.ToList());
            }

            return loadedObjectsListe.ToArray();
        }

        /// <summary>
        /// This method is only used for initial load of ESAS data - will become useless once we're in production.
        /// </summary>
        protected override object[] LoadObjectsAndPersistToDisk()
        {
            int i = 0; int j = 0;
            var esasContainer = _esasWebServiceContextFactory.Create();

            List<Postnummer> loadedObjectsListe = new List<Postnummer>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Postnummer> token = null;

            var response = esasContainer.Postnummer.Execute() as Microsoft.OData.Client.QueryOperationResponse<Postnummer>;
            loadedObjectsListe.AddRange(response.ToList());
            if ( loadedObjectsListe.Any())
            {
                j = loadedObjectsListe.Count();
                PersistToFile(i,j,ref loadedObjectsListe);
            }
            else
                return null;
            
            System.Diagnostics.Debug.WriteLine(loadedObjectsListe.Count());
         
            while ((token = response.GetContinuation()) != null)
            {
               Stopwatch st = new Stopwatch();
                st.Start();
                       esasContainer = _esasWebServiceContextFactory.Create();
                List<Postnummer> loadedObjectsListen = new List<Postnummer>();
                response = esasContainer.Execute<Postnummer>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Postnummer>;
                loadedObjectsListen.AddRange(response.ToList());
                if ( loadedObjectsListen.Any())
            {
                i = j;
                j = j + loadedObjectsListen.Count();
                PersistToFile(i,j,ref loadedObjectsListen);
                loadedObjectsListen = null;
            }


                // loadedObjectsListe.AddRange(response.ToList()); - only persist to disc, for now.
               st.Stop();
               System.Diagnostics.Debug.WriteLine(j + "-" + st.Elapsed.Seconds);
            }
            
             System.Diagnostics.Debug.WriteLine($"*** total objects written to disk was {j} ***");
            //return loadedObjectsListe.ToArray();
            return null;
        }

        
        private void PersistToFile(int indexToStartLoadFrom, int howManyRecordsToGet, ref List<Postnummer> loadedObjectsListe)
        {
             string entityName = "Postnummer";
              if (!Directory.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}"))
                Directory.CreateDirectory($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}");

            string objectsJson = JsonConvert.SerializeObject(loadedObjectsListe, Formatting.Indented);
            if ( !File.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json"))
                File.WriteAllText($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json", objectsJson );

        }


    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class PraktikomraadeLoadStrategy : BaseLoadStrategy
    {
         public PraktikomraadeLoadStrategy(EsasWsContextFactory esasWebServiceContextFactory, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasWebServiceContextFactory, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff)
        {
            var esasContainer = _esasWebServiceContextFactory.Create();
            // design continuation here
            List<Praktikomraade> loadedObjectsListe = new List<Praktikomraade>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Praktikomraade> token = null;

            string datetimeoffSetFromLoadtimeCutOff = loadTimeCutoff.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var response = esasContainer.Praktikomraade.AddQueryOption("$filter", $"ModifiedOn gt {datetimeoffSetFromLoadtimeCutOff}").Execute() as Microsoft.OData.Client.QueryOperationResponse<Praktikomraade>;

            loadedObjectsListe.AddRange(response.ToList());
            if ( ! loadedObjectsListe.Any())
                return null;
            
            while ((token = response.GetContinuation()) != null)
            {
                System.Diagnostics.Debug.WriteLine("Continuation-token available, retrieving next batch.");
                esasContainer = _esasWebServiceContextFactory.Create();
                response = esasContainer.Execute<Praktikomraade>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Praktikomraade>;
                loadedObjectsListe.AddRange(response.ToList());
            }

            return loadedObjectsListe.ToArray();
        }

        /// <summary>
        /// This method is only used for initial load of ESAS data - will become useless once we're in production.
        /// </summary>
        protected override object[] LoadObjectsAndPersistToDisk()
        {
            int i = 0; int j = 0;
            var esasContainer = _esasWebServiceContextFactory.Create();

            List<Praktikomraade> loadedObjectsListe = new List<Praktikomraade>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Praktikomraade> token = null;

            var response = esasContainer.Praktikomraade.Execute() as Microsoft.OData.Client.QueryOperationResponse<Praktikomraade>;
            loadedObjectsListe.AddRange(response.ToList());
            if ( loadedObjectsListe.Any())
            {
                j = loadedObjectsListe.Count();
                PersistToFile(i,j,ref loadedObjectsListe);
            }
            else
                return null;
            
            System.Diagnostics.Debug.WriteLine(loadedObjectsListe.Count());
         
            while ((token = response.GetContinuation()) != null)
            {
               Stopwatch st = new Stopwatch();
                st.Start();
                       esasContainer = _esasWebServiceContextFactory.Create();
                List<Praktikomraade> loadedObjectsListen = new List<Praktikomraade>();
                response = esasContainer.Execute<Praktikomraade>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Praktikomraade>;
                loadedObjectsListen.AddRange(response.ToList());
                if ( loadedObjectsListen.Any())
            {
                i = j;
                j = j + loadedObjectsListen.Count();
                PersistToFile(i,j,ref loadedObjectsListen);
                loadedObjectsListen = null;
            }


                // loadedObjectsListe.AddRange(response.ToList()); - only persist to disc, for now.
               st.Stop();
               System.Diagnostics.Debug.WriteLine(j + "-" + st.Elapsed.Seconds);
            }
            
             System.Diagnostics.Debug.WriteLine($"*** total objects written to disk was {j} ***");
            //return loadedObjectsListe.ToArray();
            return null;
        }

        
        private void PersistToFile(int indexToStartLoadFrom, int howManyRecordsToGet, ref List<Praktikomraade> loadedObjectsListe)
        {
             string entityName = "Praktikomraade";
              if (!Directory.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}"))
                Directory.CreateDirectory($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}");

            string objectsJson = JsonConvert.SerializeObject(loadedObjectsListe, Formatting.Indented);
            if ( !File.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json"))
                File.WriteAllText($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json", objectsJson );

        }


    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class PraktikopholdLoadStrategy : BaseLoadStrategy
    {
         public PraktikopholdLoadStrategy(EsasWsContextFactory esasWebServiceContextFactory, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasWebServiceContextFactory, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff)
        {
            var esasContainer = _esasWebServiceContextFactory.Create();
            // design continuation here
            List<Praktikophold> loadedObjectsListe = new List<Praktikophold>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Praktikophold> token = null;

            string datetimeoffSetFromLoadtimeCutOff = loadTimeCutoff.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var response = esasContainer.Praktikophold.AddQueryOption("$filter", $"ModifiedOn gt {datetimeoffSetFromLoadtimeCutOff}").Execute() as Microsoft.OData.Client.QueryOperationResponse<Praktikophold>;

            loadedObjectsListe.AddRange(response.ToList());
            if ( ! loadedObjectsListe.Any())
                return null;
            
            while ((token = response.GetContinuation()) != null)
            {
                System.Diagnostics.Debug.WriteLine("Continuation-token available, retrieving next batch.");
                esasContainer = _esasWebServiceContextFactory.Create();
                response = esasContainer.Execute<Praktikophold>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Praktikophold>;
                loadedObjectsListe.AddRange(response.ToList());
            }

            return loadedObjectsListe.ToArray();
        }

        /// <summary>
        /// This method is only used for initial load of ESAS data - will become useless once we're in production.
        /// </summary>
        protected override object[] LoadObjectsAndPersistToDisk()
        {
            int i = 0; int j = 0;
            var esasContainer = _esasWebServiceContextFactory.Create();

            List<Praktikophold> loadedObjectsListe = new List<Praktikophold>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Praktikophold> token = null;

            var response = esasContainer.Praktikophold.Execute() as Microsoft.OData.Client.QueryOperationResponse<Praktikophold>;
            loadedObjectsListe.AddRange(response.ToList());
            if ( loadedObjectsListe.Any())
            {
                j = loadedObjectsListe.Count();
                PersistToFile(i,j,ref loadedObjectsListe);
            }
            else
                return null;
            
            System.Diagnostics.Debug.WriteLine(loadedObjectsListe.Count());
         
            while ((token = response.GetContinuation()) != null)
            {
               Stopwatch st = new Stopwatch();
                st.Start();
                       esasContainer = _esasWebServiceContextFactory.Create();
                List<Praktikophold> loadedObjectsListen = new List<Praktikophold>();
                response = esasContainer.Execute<Praktikophold>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Praktikophold>;
                loadedObjectsListen.AddRange(response.ToList());
                if ( loadedObjectsListen.Any())
            {
                i = j;
                j = j + loadedObjectsListen.Count();
                PersistToFile(i,j,ref loadedObjectsListen);
                loadedObjectsListen = null;
            }


                // loadedObjectsListe.AddRange(response.ToList()); - only persist to disc, for now.
               st.Stop();
               System.Diagnostics.Debug.WriteLine(j + "-" + st.Elapsed.Seconds);
            }
            
             System.Diagnostics.Debug.WriteLine($"*** total objects written to disk was {j} ***");
            //return loadedObjectsListe.ToArray();
            return null;
        }

        
        private void PersistToFile(int indexToStartLoadFrom, int howManyRecordsToGet, ref List<Praktikophold> loadedObjectsListe)
        {
             string entityName = "Praktikophold";
              if (!Directory.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}"))
                Directory.CreateDirectory($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}");

            string objectsJson = JsonConvert.SerializeObject(loadedObjectsListe, Formatting.Indented);
            if ( !File.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json"))
                File.WriteAllText($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json", objectsJson );

        }


    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class ProeveLoadStrategy : BaseLoadStrategy
    {
         public ProeveLoadStrategy(EsasWsContextFactory esasWebServiceContextFactory, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasWebServiceContextFactory, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff)
        {
            var esasContainer = _esasWebServiceContextFactory.Create();
            // design continuation here
            List<Proeve> loadedObjectsListe = new List<Proeve>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Proeve> token = null;

            string datetimeoffSetFromLoadtimeCutOff = loadTimeCutoff.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var response = esasContainer.Proeve.AddQueryOption("$filter", $"ModifiedOn gt {datetimeoffSetFromLoadtimeCutOff}").Execute() as Microsoft.OData.Client.QueryOperationResponse<Proeve>;

            loadedObjectsListe.AddRange(response.ToList());
            if ( ! loadedObjectsListe.Any())
                return null;
            
            while ((token = response.GetContinuation()) != null)
            {
                System.Diagnostics.Debug.WriteLine("Continuation-token available, retrieving next batch.");
                esasContainer = _esasWebServiceContextFactory.Create();
                response = esasContainer.Execute<Proeve>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Proeve>;
                loadedObjectsListe.AddRange(response.ToList());
            }

            return loadedObjectsListe.ToArray();
        }

        /// <summary>
        /// This method is only used for initial load of ESAS data - will become useless once we're in production.
        /// </summary>
        protected override object[] LoadObjectsAndPersistToDisk()
        {
            int i = 0; int j = 0;
            var esasContainer = _esasWebServiceContextFactory.Create();

            List<Proeve> loadedObjectsListe = new List<Proeve>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Proeve> token = null;

            var response = esasContainer.Proeve.Execute() as Microsoft.OData.Client.QueryOperationResponse<Proeve>;
            loadedObjectsListe.AddRange(response.ToList());
            if ( loadedObjectsListe.Any())
            {
                j = loadedObjectsListe.Count();
                PersistToFile(i,j,ref loadedObjectsListe);
            }
            else
                return null;
            
            System.Diagnostics.Debug.WriteLine(loadedObjectsListe.Count());
         
            while ((token = response.GetContinuation()) != null)
            {
               Stopwatch st = new Stopwatch();
                st.Start();
                       esasContainer = _esasWebServiceContextFactory.Create();
                List<Proeve> loadedObjectsListen = new List<Proeve>();
                response = esasContainer.Execute<Proeve>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Proeve>;
                loadedObjectsListen.AddRange(response.ToList());
                if ( loadedObjectsListen.Any())
            {
                i = j;
                j = j + loadedObjectsListen.Count();
                PersistToFile(i,j,ref loadedObjectsListen);
                loadedObjectsListen = null;
            }


                // loadedObjectsListe.AddRange(response.ToList()); - only persist to disc, for now.
               st.Stop();
               System.Diagnostics.Debug.WriteLine(j + "-" + st.Elapsed.Seconds);
            }
            
             System.Diagnostics.Debug.WriteLine($"*** total objects written to disk was {j} ***");
            //return loadedObjectsListe.ToArray();
            return null;
        }

        
        private void PersistToFile(int indexToStartLoadFrom, int howManyRecordsToGet, ref List<Proeve> loadedObjectsListe)
        {
             string entityName = "Proeve";
              if (!Directory.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}"))
                Directory.CreateDirectory($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}");

            string objectsJson = JsonConvert.SerializeObject(loadedObjectsListe, Formatting.Indented);
            if ( !File.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json"))
                File.WriteAllText($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json", objectsJson );

        }


    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class PubliceringLoadStrategy : BaseLoadStrategy
    {
         public PubliceringLoadStrategy(EsasWsContextFactory esasWebServiceContextFactory, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasWebServiceContextFactory, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff)
        {
            var esasContainer = _esasWebServiceContextFactory.Create();
            // design continuation here
            List<Publicering> loadedObjectsListe = new List<Publicering>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Publicering> token = null;

            string datetimeoffSetFromLoadtimeCutOff = loadTimeCutoff.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var response = esasContainer.Publicering.AddQueryOption("$filter", $"ModifiedOn gt {datetimeoffSetFromLoadtimeCutOff}").Execute() as Microsoft.OData.Client.QueryOperationResponse<Publicering>;

            loadedObjectsListe.AddRange(response.ToList());
            if ( ! loadedObjectsListe.Any())
                return null;
            
            while ((token = response.GetContinuation()) != null)
            {
                System.Diagnostics.Debug.WriteLine("Continuation-token available, retrieving next batch.");
                esasContainer = _esasWebServiceContextFactory.Create();
                response = esasContainer.Execute<Publicering>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Publicering>;
                loadedObjectsListe.AddRange(response.ToList());
            }

            return loadedObjectsListe.ToArray();
        }

        /// <summary>
        /// This method is only used for initial load of ESAS data - will become useless once we're in production.
        /// </summary>
        protected override object[] LoadObjectsAndPersistToDisk()
        {
            int i = 0; int j = 0;
            var esasContainer = _esasWebServiceContextFactory.Create();

            List<Publicering> loadedObjectsListe = new List<Publicering>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Publicering> token = null;

            var response = esasContainer.Publicering.Execute() as Microsoft.OData.Client.QueryOperationResponse<Publicering>;
            loadedObjectsListe.AddRange(response.ToList());
            if ( loadedObjectsListe.Any())
            {
                j = loadedObjectsListe.Count();
                PersistToFile(i,j,ref loadedObjectsListe);
            }
            else
                return null;
            
            System.Diagnostics.Debug.WriteLine(loadedObjectsListe.Count());
         
            while ((token = response.GetContinuation()) != null)
            {
               Stopwatch st = new Stopwatch();
                st.Start();
                       esasContainer = _esasWebServiceContextFactory.Create();
                List<Publicering> loadedObjectsListen = new List<Publicering>();
                response = esasContainer.Execute<Publicering>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Publicering>;
                loadedObjectsListen.AddRange(response.ToList());
                if ( loadedObjectsListen.Any())
            {
                i = j;
                j = j + loadedObjectsListen.Count();
                PersistToFile(i,j,ref loadedObjectsListen);
                loadedObjectsListen = null;
            }


                // loadedObjectsListe.AddRange(response.ToList()); - only persist to disc, for now.
               st.Stop();
               System.Diagnostics.Debug.WriteLine(j + "-" + st.Elapsed.Seconds);
            }
            
             System.Diagnostics.Debug.WriteLine($"*** total objects written to disk was {j} ***");
            //return loadedObjectsListe.ToArray();
            return null;
        }

        
        private void PersistToFile(int indexToStartLoadFrom, int howManyRecordsToGet, ref List<Publicering> loadedObjectsListe)
        {
             string entityName = "Publicering";
              if (!Directory.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}"))
                Directory.CreateDirectory($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}");

            string objectsJson = JsonConvert.SerializeObject(loadedObjectsListe, Formatting.Indented);
            if ( !File.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json"))
                File.WriteAllText($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json", objectsJson );

        }


    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class RelationsStatusLoadStrategy : BaseLoadStrategy
    {
         public RelationsStatusLoadStrategy(EsasWsContextFactory esasWebServiceContextFactory, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasWebServiceContextFactory, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff)
        {
            var esasContainer = _esasWebServiceContextFactory.Create();
            // design continuation here
            List<RelationsStatus> loadedObjectsListe = new List<RelationsStatus>();
            Microsoft.OData.Client.DataServiceQueryContinuation<RelationsStatus> token = null;

            string datetimeoffSetFromLoadtimeCutOff = loadTimeCutoff.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var response = esasContainer.RelationsStatus.AddQueryOption("$filter", $"ModifiedOn gt {datetimeoffSetFromLoadtimeCutOff}").Execute() as Microsoft.OData.Client.QueryOperationResponse<RelationsStatus>;

            loadedObjectsListe.AddRange(response.ToList());
            if ( ! loadedObjectsListe.Any())
                return null;
            
            while ((token = response.GetContinuation()) != null)
            {
                System.Diagnostics.Debug.WriteLine("Continuation-token available, retrieving next batch.");
                esasContainer = _esasWebServiceContextFactory.Create();
                response = esasContainer.Execute<RelationsStatus>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<RelationsStatus>;
                loadedObjectsListe.AddRange(response.ToList());
            }

            return loadedObjectsListe.ToArray();
        }

        /// <summary>
        /// This method is only used for initial load of ESAS data - will become useless once we're in production.
        /// </summary>
        protected override object[] LoadObjectsAndPersistToDisk()
        {
            int i = 0; int j = 0;
            var esasContainer = _esasWebServiceContextFactory.Create();

            List<RelationsStatus> loadedObjectsListe = new List<RelationsStatus>();
            Microsoft.OData.Client.DataServiceQueryContinuation<RelationsStatus> token = null;

            var response = esasContainer.RelationsStatus.Execute() as Microsoft.OData.Client.QueryOperationResponse<RelationsStatus>;
            loadedObjectsListe.AddRange(response.ToList());
            if ( loadedObjectsListe.Any())
            {
                j = loadedObjectsListe.Count();
                PersistToFile(i,j,ref loadedObjectsListe);
            }
            else
                return null;
            
            System.Diagnostics.Debug.WriteLine(loadedObjectsListe.Count());
         
            while ((token = response.GetContinuation()) != null)
            {
               Stopwatch st = new Stopwatch();
                st.Start();
                       esasContainer = _esasWebServiceContextFactory.Create();
                List<RelationsStatus> loadedObjectsListen = new List<RelationsStatus>();
                response = esasContainer.Execute<RelationsStatus>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<RelationsStatus>;
                loadedObjectsListen.AddRange(response.ToList());
                if ( loadedObjectsListen.Any())
            {
                i = j;
                j = j + loadedObjectsListen.Count();
                PersistToFile(i,j,ref loadedObjectsListen);
                loadedObjectsListen = null;
            }


                // loadedObjectsListe.AddRange(response.ToList()); - only persist to disc, for now.
               st.Stop();
               System.Diagnostics.Debug.WriteLine(j + "-" + st.Elapsed.Seconds);
            }
            
             System.Diagnostics.Debug.WriteLine($"*** total objects written to disk was {j} ***");
            //return loadedObjectsListe.ToArray();
            return null;
        }

        
        private void PersistToFile(int indexToStartLoadFrom, int howManyRecordsToGet, ref List<RelationsStatus> loadedObjectsListe)
        {
             string entityName = "RelationsStatus";
              if (!Directory.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}"))
                Directory.CreateDirectory($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}");

            string objectsJson = JsonConvert.SerializeObject(loadedObjectsListe, Formatting.Indented);
            if ( !File.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json"))
                File.WriteAllText($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json", objectsJson );

        }


    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class RekvirenttypeLoadStrategy : BaseLoadStrategy
    {
         public RekvirenttypeLoadStrategy(EsasWsContextFactory esasWebServiceContextFactory, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasWebServiceContextFactory, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff)
        {
            var esasContainer = _esasWebServiceContextFactory.Create();
            // design continuation here
            List<Rekvirenttype> loadedObjectsListe = new List<Rekvirenttype>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Rekvirenttype> token = null;

            string datetimeoffSetFromLoadtimeCutOff = loadTimeCutoff.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var response = esasContainer.Rekvirenttype.AddQueryOption("$filter", $"ModifiedOn gt {datetimeoffSetFromLoadtimeCutOff}").Execute() as Microsoft.OData.Client.QueryOperationResponse<Rekvirenttype>;

            loadedObjectsListe.AddRange(response.ToList());
            if ( ! loadedObjectsListe.Any())
                return null;
            
            while ((token = response.GetContinuation()) != null)
            {
                System.Diagnostics.Debug.WriteLine("Continuation-token available, retrieving next batch.");
                esasContainer = _esasWebServiceContextFactory.Create();
                response = esasContainer.Execute<Rekvirenttype>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Rekvirenttype>;
                loadedObjectsListe.AddRange(response.ToList());
            }

            return loadedObjectsListe.ToArray();
        }

        /// <summary>
        /// This method is only used for initial load of ESAS data - will become useless once we're in production.
        /// </summary>
        protected override object[] LoadObjectsAndPersistToDisk()
        {
            int i = 0; int j = 0;
            var esasContainer = _esasWebServiceContextFactory.Create();

            List<Rekvirenttype> loadedObjectsListe = new List<Rekvirenttype>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Rekvirenttype> token = null;

            var response = esasContainer.Rekvirenttype.Execute() as Microsoft.OData.Client.QueryOperationResponse<Rekvirenttype>;
            loadedObjectsListe.AddRange(response.ToList());
            if ( loadedObjectsListe.Any())
            {
                j = loadedObjectsListe.Count();
                PersistToFile(i,j,ref loadedObjectsListe);
            }
            else
                return null;
            
            System.Diagnostics.Debug.WriteLine(loadedObjectsListe.Count());
         
            while ((token = response.GetContinuation()) != null)
            {
               Stopwatch st = new Stopwatch();
                st.Start();
                       esasContainer = _esasWebServiceContextFactory.Create();
                List<Rekvirenttype> loadedObjectsListen = new List<Rekvirenttype>();
                response = esasContainer.Execute<Rekvirenttype>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Rekvirenttype>;
                loadedObjectsListen.AddRange(response.ToList());
                if ( loadedObjectsListen.Any())
            {
                i = j;
                j = j + loadedObjectsListen.Count();
                PersistToFile(i,j,ref loadedObjectsListen);
                loadedObjectsListen = null;
            }


                // loadedObjectsListe.AddRange(response.ToList()); - only persist to disc, for now.
               st.Stop();
               System.Diagnostics.Debug.WriteLine(j + "-" + st.Elapsed.Seconds);
            }
            
             System.Diagnostics.Debug.WriteLine($"*** total objects written to disk was {j} ***");
            //return loadedObjectsListe.ToArray();
            return null;
        }

        
        private void PersistToFile(int indexToStartLoadFrom, int howManyRecordsToGet, ref List<Rekvirenttype> loadedObjectsListe)
        {
             string entityName = "Rekvirenttype";
              if (!Directory.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}"))
                Directory.CreateDirectory($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}");

            string objectsJson = JsonConvert.SerializeObject(loadedObjectsListe, Formatting.Indented);
            if ( !File.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json"))
                File.WriteAllText($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json", objectsJson );

        }


    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class SpecialiseringLoadStrategy : BaseLoadStrategy
    {
         public SpecialiseringLoadStrategy(EsasWsContextFactory esasWebServiceContextFactory, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasWebServiceContextFactory, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff)
        {
            var esasContainer = _esasWebServiceContextFactory.Create();
            // design continuation here
            List<Specialisering> loadedObjectsListe = new List<Specialisering>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Specialisering> token = null;

            string datetimeoffSetFromLoadtimeCutOff = loadTimeCutoff.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var response = esasContainer.Specialisering.AddQueryOption("$filter", $"ModifiedOn gt {datetimeoffSetFromLoadtimeCutOff}").Execute() as Microsoft.OData.Client.QueryOperationResponse<Specialisering>;

            loadedObjectsListe.AddRange(response.ToList());
            if ( ! loadedObjectsListe.Any())
                return null;
            
            while ((token = response.GetContinuation()) != null)
            {
                System.Diagnostics.Debug.WriteLine("Continuation-token available, retrieving next batch.");
                esasContainer = _esasWebServiceContextFactory.Create();
                response = esasContainer.Execute<Specialisering>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Specialisering>;
                loadedObjectsListe.AddRange(response.ToList());
            }

            return loadedObjectsListe.ToArray();
        }

        /// <summary>
        /// This method is only used for initial load of ESAS data - will become useless once we're in production.
        /// </summary>
        protected override object[] LoadObjectsAndPersistToDisk()
        {
            int i = 0; int j = 0;
            var esasContainer = _esasWebServiceContextFactory.Create();

            List<Specialisering> loadedObjectsListe = new List<Specialisering>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Specialisering> token = null;

            var response = esasContainer.Specialisering.Execute() as Microsoft.OData.Client.QueryOperationResponse<Specialisering>;
            loadedObjectsListe.AddRange(response.ToList());
            if ( loadedObjectsListe.Any())
            {
                j = loadedObjectsListe.Count();
                PersistToFile(i,j,ref loadedObjectsListe);
            }
            else
                return null;
            
            System.Diagnostics.Debug.WriteLine(loadedObjectsListe.Count());
         
            while ((token = response.GetContinuation()) != null)
            {
               Stopwatch st = new Stopwatch();
                st.Start();
                       esasContainer = _esasWebServiceContextFactory.Create();
                List<Specialisering> loadedObjectsListen = new List<Specialisering>();
                response = esasContainer.Execute<Specialisering>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Specialisering>;
                loadedObjectsListen.AddRange(response.ToList());
                if ( loadedObjectsListen.Any())
            {
                i = j;
                j = j + loadedObjectsListen.Count();
                PersistToFile(i,j,ref loadedObjectsListen);
                loadedObjectsListen = null;
            }


                // loadedObjectsListe.AddRange(response.ToList()); - only persist to disc, for now.
               st.Stop();
               System.Diagnostics.Debug.WriteLine(j + "-" + st.Elapsed.Seconds);
            }
            
             System.Diagnostics.Debug.WriteLine($"*** total objects written to disk was {j} ***");
            //return loadedObjectsListe.ToArray();
            return null;
        }

        
        private void PersistToFile(int indexToStartLoadFrom, int howManyRecordsToGet, ref List<Specialisering> loadedObjectsListe)
        {
             string entityName = "Specialisering";
              if (!Directory.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}"))
                Directory.CreateDirectory($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}");

            string objectsJson = JsonConvert.SerializeObject(loadedObjectsListe, Formatting.Indented);
            if ( !File.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json"))
                File.WriteAllText($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json", objectsJson );

        }


    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class SamlaesningLoadStrategy : BaseLoadStrategy
    {
         public SamlaesningLoadStrategy(EsasWsContextFactory esasWebServiceContextFactory, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasWebServiceContextFactory, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff)
        {
            var esasContainer = _esasWebServiceContextFactory.Create();
            // design continuation here
            List<Samlaesning> loadedObjectsListe = new List<Samlaesning>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Samlaesning> token = null;

            string datetimeoffSetFromLoadtimeCutOff = loadTimeCutoff.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var response = esasContainer.Samlaesning.AddQueryOption("$filter", $"ModifiedOn gt {datetimeoffSetFromLoadtimeCutOff}").Execute() as Microsoft.OData.Client.QueryOperationResponse<Samlaesning>;

            loadedObjectsListe.AddRange(response.ToList());
            if ( ! loadedObjectsListe.Any())
                return null;
            
            while ((token = response.GetContinuation()) != null)
            {
                System.Diagnostics.Debug.WriteLine("Continuation-token available, retrieving next batch.");
                esasContainer = _esasWebServiceContextFactory.Create();
                response = esasContainer.Execute<Samlaesning>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Samlaesning>;
                loadedObjectsListe.AddRange(response.ToList());
            }

            return loadedObjectsListe.ToArray();
        }

        /// <summary>
        /// This method is only used for initial load of ESAS data - will become useless once we're in production.
        /// </summary>
        protected override object[] LoadObjectsAndPersistToDisk()
        {
            int i = 0; int j = 0;
            var esasContainer = _esasWebServiceContextFactory.Create();

            List<Samlaesning> loadedObjectsListe = new List<Samlaesning>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Samlaesning> token = null;

            var response = esasContainer.Samlaesning.Execute() as Microsoft.OData.Client.QueryOperationResponse<Samlaesning>;
            loadedObjectsListe.AddRange(response.ToList());
            if ( loadedObjectsListe.Any())
            {
                j = loadedObjectsListe.Count();
                PersistToFile(i,j,ref loadedObjectsListe);
            }
            else
                return null;
            
            System.Diagnostics.Debug.WriteLine(loadedObjectsListe.Count());
         
            while ((token = response.GetContinuation()) != null)
            {
               Stopwatch st = new Stopwatch();
                st.Start();
                       esasContainer = _esasWebServiceContextFactory.Create();
                List<Samlaesning> loadedObjectsListen = new List<Samlaesning>();
                response = esasContainer.Execute<Samlaesning>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Samlaesning>;
                loadedObjectsListen.AddRange(response.ToList());
                if ( loadedObjectsListen.Any())
            {
                i = j;
                j = j + loadedObjectsListen.Count();
                PersistToFile(i,j,ref loadedObjectsListen);
                loadedObjectsListen = null;
            }


                // loadedObjectsListe.AddRange(response.ToList()); - only persist to disc, for now.
               st.Stop();
               System.Diagnostics.Debug.WriteLine(j + "-" + st.Elapsed.Seconds);
            }
            
             System.Diagnostics.Debug.WriteLine($"*** total objects written to disk was {j} ***");
            //return loadedObjectsListe.ToArray();
            return null;
        }

        
        private void PersistToFile(int indexToStartLoadFrom, int howManyRecordsToGet, ref List<Samlaesning> loadedObjectsListe)
        {
             string entityName = "Samlaesning";
              if (!Directory.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}"))
                Directory.CreateDirectory($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}");

            string objectsJson = JsonConvert.SerializeObject(loadedObjectsListe, Formatting.Indented);
            if ( !File.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json"))
                File.WriteAllText($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json", objectsJson );

        }


    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class StruktureltUddannelseselementLoadStrategy : BaseLoadStrategy
    {
         public StruktureltUddannelseselementLoadStrategy(EsasWsContextFactory esasWebServiceContextFactory, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasWebServiceContextFactory, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff)
        {
            var esasContainer = _esasWebServiceContextFactory.Create();
            // design continuation here
            List<StruktureltUddannelseselement> loadedObjectsListe = new List<StruktureltUddannelseselement>();
            Microsoft.OData.Client.DataServiceQueryContinuation<StruktureltUddannelseselement> token = null;

            string datetimeoffSetFromLoadtimeCutOff = loadTimeCutoff.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var response = esasContainer.StruktureltUddannelseselement.AddQueryOption("$filter", $"ModifiedOn gt {datetimeoffSetFromLoadtimeCutOff}").Execute() as Microsoft.OData.Client.QueryOperationResponse<StruktureltUddannelseselement>;

            loadedObjectsListe.AddRange(response.ToList());
            if ( ! loadedObjectsListe.Any())
                return null;
            
            while ((token = response.GetContinuation()) != null)
            {
                System.Diagnostics.Debug.WriteLine("Continuation-token available, retrieving next batch.");
                esasContainer = _esasWebServiceContextFactory.Create();
                response = esasContainer.Execute<StruktureltUddannelseselement>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<StruktureltUddannelseselement>;
                loadedObjectsListe.AddRange(response.ToList());
            }

            return loadedObjectsListe.ToArray();
        }

        /// <summary>
        /// This method is only used for initial load of ESAS data - will become useless once we're in production.
        /// </summary>
        protected override object[] LoadObjectsAndPersistToDisk()
        {
            int i = 0; int j = 0;
            var esasContainer = _esasWebServiceContextFactory.Create();

            List<StruktureltUddannelseselement> loadedObjectsListe = new List<StruktureltUddannelseselement>();
            Microsoft.OData.Client.DataServiceQueryContinuation<StruktureltUddannelseselement> token = null;

            var response = esasContainer.StruktureltUddannelseselement.Execute() as Microsoft.OData.Client.QueryOperationResponse<StruktureltUddannelseselement>;
            loadedObjectsListe.AddRange(response.ToList());
            if ( loadedObjectsListe.Any())
            {
                j = loadedObjectsListe.Count();
                PersistToFile(i,j,ref loadedObjectsListe);
            }
            else
                return null;
            
            System.Diagnostics.Debug.WriteLine(loadedObjectsListe.Count());
         
            while ((token = response.GetContinuation()) != null)
            {
               Stopwatch st = new Stopwatch();
                st.Start();
                       esasContainer = _esasWebServiceContextFactory.Create();
                List<StruktureltUddannelseselement> loadedObjectsListen = new List<StruktureltUddannelseselement>();
                response = esasContainer.Execute<StruktureltUddannelseselement>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<StruktureltUddannelseselement>;
                loadedObjectsListen.AddRange(response.ToList());
                if ( loadedObjectsListen.Any())
            {
                i = j;
                j = j + loadedObjectsListen.Count();
                PersistToFile(i,j,ref loadedObjectsListen);
                loadedObjectsListen = null;
            }


                // loadedObjectsListe.AddRange(response.ToList()); - only persist to disc, for now.
               st.Stop();
               System.Diagnostics.Debug.WriteLine(j + "-" + st.Elapsed.Seconds);
            }
            
             System.Diagnostics.Debug.WriteLine($"*** total objects written to disk was {j} ***");
            //return loadedObjectsListe.ToArray();
            return null;
        }

        
        private void PersistToFile(int indexToStartLoadFrom, int howManyRecordsToGet, ref List<StruktureltUddannelseselement> loadedObjectsListe)
        {
             string entityName = "StruktureltUddannelseselement";
              if (!Directory.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}"))
                Directory.CreateDirectory($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}");

            string objectsJson = JsonConvert.SerializeObject(loadedObjectsListe, Formatting.Indented);
            if ( !File.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json"))
                File.WriteAllText($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json", objectsJson );

        }


    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class StudieforloebLoadStrategy : BaseLoadStrategy
    {
         public StudieforloebLoadStrategy(EsasWsContextFactory esasWebServiceContextFactory, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasWebServiceContextFactory, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff)
        {
            var esasContainer = _esasWebServiceContextFactory.Create();
            // design continuation here
            List<Studieforloeb> loadedObjectsListe = new List<Studieforloeb>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Studieforloeb> token = null;

            string datetimeoffSetFromLoadtimeCutOff = loadTimeCutoff.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var response = esasContainer.Studieforloeb.AddQueryOption("$filter", $"ModifiedOn gt {datetimeoffSetFromLoadtimeCutOff}").Execute() as Microsoft.OData.Client.QueryOperationResponse<Studieforloeb>;

            loadedObjectsListe.AddRange(response.ToList());
            if ( ! loadedObjectsListe.Any())
                return null;
            
            while ((token = response.GetContinuation()) != null)
            {
                System.Diagnostics.Debug.WriteLine("Continuation-token available, retrieving next batch.");
                esasContainer = _esasWebServiceContextFactory.Create();
                response = esasContainer.Execute<Studieforloeb>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Studieforloeb>;
                loadedObjectsListe.AddRange(response.ToList());
            }

            return loadedObjectsListe.ToArray();
        }

        /// <summary>
        /// This method is only used for initial load of ESAS data - will become useless once we're in production.
        /// </summary>
        protected override object[] LoadObjectsAndPersistToDisk()
        {
            int i = 0; int j = 0;
            var esasContainer = _esasWebServiceContextFactory.Create();

            List<Studieforloeb> loadedObjectsListe = new List<Studieforloeb>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Studieforloeb> token = null;

            var response = esasContainer.Studieforloeb.Execute() as Microsoft.OData.Client.QueryOperationResponse<Studieforloeb>;
            loadedObjectsListe.AddRange(response.ToList());
            if ( loadedObjectsListe.Any())
            {
                j = loadedObjectsListe.Count();
                PersistToFile(i,j,ref loadedObjectsListe);
            }
            else
                return null;
            
            System.Diagnostics.Debug.WriteLine(loadedObjectsListe.Count());
         
            while ((token = response.GetContinuation()) != null)
            {
               Stopwatch st = new Stopwatch();
                st.Start();
                       esasContainer = _esasWebServiceContextFactory.Create();
                List<Studieforloeb> loadedObjectsListen = new List<Studieforloeb>();
                response = esasContainer.Execute<Studieforloeb>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Studieforloeb>;
                loadedObjectsListen.AddRange(response.ToList());
                if ( loadedObjectsListen.Any())
            {
                i = j;
                j = j + loadedObjectsListen.Count();
                PersistToFile(i,j,ref loadedObjectsListen);
                loadedObjectsListen = null;
            }


                // loadedObjectsListe.AddRange(response.ToList()); - only persist to disc, for now.
               st.Stop();
               System.Diagnostics.Debug.WriteLine(j + "-" + st.Elapsed.Seconds);
            }
            
             System.Diagnostics.Debug.WriteLine($"*** total objects written to disk was {j} ***");
            //return loadedObjectsListe.ToArray();
            return null;
        }

        
        private void PersistToFile(int indexToStartLoadFrom, int howManyRecordsToGet, ref List<Studieforloeb> loadedObjectsListe)
        {
             string entityName = "Studieforloeb";
              if (!Directory.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}"))
                Directory.CreateDirectory($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}");

            string objectsJson = JsonConvert.SerializeObject(loadedObjectsListe, Formatting.Indented);
            if ( !File.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json"))
                File.WriteAllText($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json", objectsJson );

        }


    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class StudieinaktivPeriodeLoadStrategy : BaseLoadStrategy
    {
         public StudieinaktivPeriodeLoadStrategy(EsasWsContextFactory esasWebServiceContextFactory, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasWebServiceContextFactory, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff)
        {
            var esasContainer = _esasWebServiceContextFactory.Create();
            // design continuation here
            List<StudieinaktivPeriode> loadedObjectsListe = new List<StudieinaktivPeriode>();
            Microsoft.OData.Client.DataServiceQueryContinuation<StudieinaktivPeriode> token = null;

            string datetimeoffSetFromLoadtimeCutOff = loadTimeCutoff.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var response = esasContainer.StudieinaktivPeriode.AddQueryOption("$filter", $"ModifiedOn gt {datetimeoffSetFromLoadtimeCutOff}").Execute() as Microsoft.OData.Client.QueryOperationResponse<StudieinaktivPeriode>;

            loadedObjectsListe.AddRange(response.ToList());
            if ( ! loadedObjectsListe.Any())
                return null;
            
            while ((token = response.GetContinuation()) != null)
            {
                System.Diagnostics.Debug.WriteLine("Continuation-token available, retrieving next batch.");
                esasContainer = _esasWebServiceContextFactory.Create();
                response = esasContainer.Execute<StudieinaktivPeriode>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<StudieinaktivPeriode>;
                loadedObjectsListe.AddRange(response.ToList());
            }

            return loadedObjectsListe.ToArray();
        }

        /// <summary>
        /// This method is only used for initial load of ESAS data - will become useless once we're in production.
        /// </summary>
        protected override object[] LoadObjectsAndPersistToDisk()
        {
            int i = 0; int j = 0;
            var esasContainer = _esasWebServiceContextFactory.Create();

            List<StudieinaktivPeriode> loadedObjectsListe = new List<StudieinaktivPeriode>();
            Microsoft.OData.Client.DataServiceQueryContinuation<StudieinaktivPeriode> token = null;

            var response = esasContainer.StudieinaktivPeriode.Execute() as Microsoft.OData.Client.QueryOperationResponse<StudieinaktivPeriode>;
            loadedObjectsListe.AddRange(response.ToList());
            if ( loadedObjectsListe.Any())
            {
                j = loadedObjectsListe.Count();
                PersistToFile(i,j,ref loadedObjectsListe);
            }
            else
                return null;
            
            System.Diagnostics.Debug.WriteLine(loadedObjectsListe.Count());
         
            while ((token = response.GetContinuation()) != null)
            {
               Stopwatch st = new Stopwatch();
                st.Start();
                       esasContainer = _esasWebServiceContextFactory.Create();
                List<StudieinaktivPeriode> loadedObjectsListen = new List<StudieinaktivPeriode>();
                response = esasContainer.Execute<StudieinaktivPeriode>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<StudieinaktivPeriode>;
                loadedObjectsListen.AddRange(response.ToList());
                if ( loadedObjectsListen.Any())
            {
                i = j;
                j = j + loadedObjectsListen.Count();
                PersistToFile(i,j,ref loadedObjectsListen);
                loadedObjectsListen = null;
            }


                // loadedObjectsListe.AddRange(response.ToList()); - only persist to disc, for now.
               st.Stop();
               System.Diagnostics.Debug.WriteLine(j + "-" + st.Elapsed.Seconds);
            }
            
             System.Diagnostics.Debug.WriteLine($"*** total objects written to disk was {j} ***");
            //return loadedObjectsListe.ToArray();
            return null;
        }

        
        private void PersistToFile(int indexToStartLoadFrom, int howManyRecordsToGet, ref List<StudieinaktivPeriode> loadedObjectsListe)
        {
             string entityName = "StudieinaktivPeriode";
              if (!Directory.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}"))
                Directory.CreateDirectory($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}");

            string objectsJson = JsonConvert.SerializeObject(loadedObjectsListe, Formatting.Indented);
            if ( !File.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json"))
                File.WriteAllText($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json", objectsJson );

        }


    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class SystemUserLoadStrategy : BaseLoadStrategy
    {
         public SystemUserLoadStrategy(EsasWsContextFactory esasWebServiceContextFactory, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasWebServiceContextFactory, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff)
        {
            var esasContainer = _esasWebServiceContextFactory.Create();
            // design continuation here
            List<SystemUser> loadedObjectsListe = new List<SystemUser>();
            Microsoft.OData.Client.DataServiceQueryContinuation<SystemUser> token = null;

            string datetimeoffSetFromLoadtimeCutOff = loadTimeCutoff.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var response = esasContainer.SystemUser.AddQueryOption("$filter", $"ModifiedOn gt {datetimeoffSetFromLoadtimeCutOff}").Execute() as Microsoft.OData.Client.QueryOperationResponse<SystemUser>;

            loadedObjectsListe.AddRange(response.ToList());
            if ( ! loadedObjectsListe.Any())
                return null;
            
            while ((token = response.GetContinuation()) != null)
            {
                System.Diagnostics.Debug.WriteLine("Continuation-token available, retrieving next batch.");
                esasContainer = _esasWebServiceContextFactory.Create();
                response = esasContainer.Execute<SystemUser>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<SystemUser>;
                loadedObjectsListe.AddRange(response.ToList());
            }

            return loadedObjectsListe.ToArray();
        }

        /// <summary>
        /// This method is only used for initial load of ESAS data - will become useless once we're in production.
        /// </summary>
        protected override object[] LoadObjectsAndPersistToDisk()
        {
            int i = 0; int j = 0;
            var esasContainer = _esasWebServiceContextFactory.Create();

            List<SystemUser> loadedObjectsListe = new List<SystemUser>();
            Microsoft.OData.Client.DataServiceQueryContinuation<SystemUser> token = null;

            var response = esasContainer.SystemUser.Execute() as Microsoft.OData.Client.QueryOperationResponse<SystemUser>;
            loadedObjectsListe.AddRange(response.ToList());
            if ( loadedObjectsListe.Any())
            {
                j = loadedObjectsListe.Count();
                PersistToFile(i,j,ref loadedObjectsListe);
            }
            else
                return null;
            
            System.Diagnostics.Debug.WriteLine(loadedObjectsListe.Count());
         
            while ((token = response.GetContinuation()) != null)
            {
               Stopwatch st = new Stopwatch();
                st.Start();
                       esasContainer = _esasWebServiceContextFactory.Create();
                List<SystemUser> loadedObjectsListen = new List<SystemUser>();
                response = esasContainer.Execute<SystemUser>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<SystemUser>;
                loadedObjectsListen.AddRange(response.ToList());
                if ( loadedObjectsListen.Any())
            {
                i = j;
                j = j + loadedObjectsListen.Count();
                PersistToFile(i,j,ref loadedObjectsListen);
                loadedObjectsListen = null;
            }


                // loadedObjectsListe.AddRange(response.ToList()); - only persist to disc, for now.
               st.Stop();
               System.Diagnostics.Debug.WriteLine(j + "-" + st.Elapsed.Seconds);
            }
            
             System.Diagnostics.Debug.WriteLine($"*** total objects written to disk was {j} ***");
            //return loadedObjectsListe.ToArray();
            return null;
        }

        
        private void PersistToFile(int indexToStartLoadFrom, int howManyRecordsToGet, ref List<SystemUser> loadedObjectsListe)
        {
             string entityName = "SystemUser";
              if (!Directory.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}"))
                Directory.CreateDirectory($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}");

            string objectsJson = JsonConvert.SerializeObject(loadedObjectsListe, Formatting.Indented);
            if ( !File.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json"))
                File.WriteAllText($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json", objectsJson );

        }


    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class UddannelsesaktivitetLoadStrategy : BaseLoadStrategy
    {
         public UddannelsesaktivitetLoadStrategy(EsasWsContextFactory esasWebServiceContextFactory, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasWebServiceContextFactory, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff)
        {
            var esasContainer = _esasWebServiceContextFactory.Create();
            // design continuation here
            List<Uddannelsesaktivitet> loadedObjectsListe = new List<Uddannelsesaktivitet>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Uddannelsesaktivitet> token = null;

            string datetimeoffSetFromLoadtimeCutOff = loadTimeCutoff.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var response = esasContainer.Uddannelsesaktivitet.AddQueryOption("$filter", $"ModifiedOn gt {datetimeoffSetFromLoadtimeCutOff}").Execute() as Microsoft.OData.Client.QueryOperationResponse<Uddannelsesaktivitet>;

            loadedObjectsListe.AddRange(response.ToList());
            if ( ! loadedObjectsListe.Any())
                return null;
            
            while ((token = response.GetContinuation()) != null)
            {
                System.Diagnostics.Debug.WriteLine("Continuation-token available, retrieving next batch.");
                esasContainer = _esasWebServiceContextFactory.Create();
                response = esasContainer.Execute<Uddannelsesaktivitet>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Uddannelsesaktivitet>;
                loadedObjectsListe.AddRange(response.ToList());
            }

            return loadedObjectsListe.ToArray();
        }

        /// <summary>
        /// This method is only used for initial load of ESAS data - will become useless once we're in production.
        /// </summary>
        protected override object[] LoadObjectsAndPersistToDisk()
        {
            int i = 0; int j = 0;
            var esasContainer = _esasWebServiceContextFactory.Create();

            List<Uddannelsesaktivitet> loadedObjectsListe = new List<Uddannelsesaktivitet>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Uddannelsesaktivitet> token = null;

            var response = esasContainer.Uddannelsesaktivitet.Execute() as Microsoft.OData.Client.QueryOperationResponse<Uddannelsesaktivitet>;
            loadedObjectsListe.AddRange(response.ToList());
            if ( loadedObjectsListe.Any())
            {
                j = loadedObjectsListe.Count();
                PersistToFile(i,j,ref loadedObjectsListe);
            }
            else
                return null;
            
            System.Diagnostics.Debug.WriteLine(loadedObjectsListe.Count());
         
            while ((token = response.GetContinuation()) != null)
            {
               Stopwatch st = new Stopwatch();
                st.Start();
                       esasContainer = _esasWebServiceContextFactory.Create();
                List<Uddannelsesaktivitet> loadedObjectsListen = new List<Uddannelsesaktivitet>();
                response = esasContainer.Execute<Uddannelsesaktivitet>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Uddannelsesaktivitet>;
                loadedObjectsListen.AddRange(response.ToList());
                if ( loadedObjectsListen.Any())
            {
                i = j;
                j = j + loadedObjectsListen.Count();
                PersistToFile(i,j,ref loadedObjectsListen);
                loadedObjectsListen = null;
            }


                // loadedObjectsListe.AddRange(response.ToList()); - only persist to disc, for now.
               st.Stop();
               System.Diagnostics.Debug.WriteLine(j + "-" + st.Elapsed.Seconds);
            }
            
             System.Diagnostics.Debug.WriteLine($"*** total objects written to disk was {j} ***");
            //return loadedObjectsListe.ToArray();
            return null;
        }

        
        private void PersistToFile(int indexToStartLoadFrom, int howManyRecordsToGet, ref List<Uddannelsesaktivitet> loadedObjectsListe)
        {
             string entityName = "Uddannelsesaktivitet";
              if (!Directory.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}"))
                Directory.CreateDirectory($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}");

            string objectsJson = JsonConvert.SerializeObject(loadedObjectsListe, Formatting.Indented);
            if ( !File.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json"))
                File.WriteAllText($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json", objectsJson );

        }


    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class UddannelsesstrukturLoadStrategy : BaseLoadStrategy
    {
         public UddannelsesstrukturLoadStrategy(EsasWsContextFactory esasWebServiceContextFactory, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasWebServiceContextFactory, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff)
        {
            var esasContainer = _esasWebServiceContextFactory.Create();
            // design continuation here
            List<Uddannelsesstruktur> loadedObjectsListe = new List<Uddannelsesstruktur>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Uddannelsesstruktur> token = null;

            string datetimeoffSetFromLoadtimeCutOff = loadTimeCutoff.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var response = esasContainer.Uddannelsesstruktur.AddQueryOption("$filter", $"ModifiedOn gt {datetimeoffSetFromLoadtimeCutOff}").Execute() as Microsoft.OData.Client.QueryOperationResponse<Uddannelsesstruktur>;

            loadedObjectsListe.AddRange(response.ToList());
            if ( ! loadedObjectsListe.Any())
                return null;
            
            while ((token = response.GetContinuation()) != null)
            {
                System.Diagnostics.Debug.WriteLine("Continuation-token available, retrieving next batch.");
                esasContainer = _esasWebServiceContextFactory.Create();
                response = esasContainer.Execute<Uddannelsesstruktur>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Uddannelsesstruktur>;
                loadedObjectsListe.AddRange(response.ToList());
            }

            return loadedObjectsListe.ToArray();
        }

        /// <summary>
        /// This method is only used for initial load of ESAS data - will become useless once we're in production.
        /// </summary>
        protected override object[] LoadObjectsAndPersistToDisk()
        {
            int i = 0; int j = 0;
            var esasContainer = _esasWebServiceContextFactory.Create();

            List<Uddannelsesstruktur> loadedObjectsListe = new List<Uddannelsesstruktur>();
            Microsoft.OData.Client.DataServiceQueryContinuation<Uddannelsesstruktur> token = null;

            var response = esasContainer.Uddannelsesstruktur.Execute() as Microsoft.OData.Client.QueryOperationResponse<Uddannelsesstruktur>;
            loadedObjectsListe.AddRange(response.ToList());
            if ( loadedObjectsListe.Any())
            {
                j = loadedObjectsListe.Count();
                PersistToFile(i,j,ref loadedObjectsListe);
            }
            else
                return null;
            
            System.Diagnostics.Debug.WriteLine(loadedObjectsListe.Count());
         
            while ((token = response.GetContinuation()) != null)
            {
               Stopwatch st = new Stopwatch();
                st.Start();
                       esasContainer = _esasWebServiceContextFactory.Create();
                List<Uddannelsesstruktur> loadedObjectsListen = new List<Uddannelsesstruktur>();
                response = esasContainer.Execute<Uddannelsesstruktur>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<Uddannelsesstruktur>;
                loadedObjectsListen.AddRange(response.ToList());
                if ( loadedObjectsListen.Any())
            {
                i = j;
                j = j + loadedObjectsListen.Count();
                PersistToFile(i,j,ref loadedObjectsListen);
                loadedObjectsListen = null;
            }


                // loadedObjectsListe.AddRange(response.ToList()); - only persist to disc, for now.
               st.Stop();
               System.Diagnostics.Debug.WriteLine(j + "-" + st.Elapsed.Seconds);
            }
            
             System.Diagnostics.Debug.WriteLine($"*** total objects written to disk was {j} ***");
            //return loadedObjectsListe.ToArray();
            return null;
        }

        
        private void PersistToFile(int indexToStartLoadFrom, int howManyRecordsToGet, ref List<Uddannelsesstruktur> loadedObjectsListe)
        {
             string entityName = "Uddannelsesstruktur";
              if (!Directory.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}"))
                Directory.CreateDirectory($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}");

            string objectsJson = JsonConvert.SerializeObject(loadedObjectsListe, Formatting.Indented);
            if ( !File.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json"))
                File.WriteAllText($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json", objectsJson );

        }


    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class UdlandsopholdAnsoegningLoadStrategy : BaseLoadStrategy
    {
         public UdlandsopholdAnsoegningLoadStrategy(EsasWsContextFactory esasWebServiceContextFactory, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasWebServiceContextFactory, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff)
        {
            var esasContainer = _esasWebServiceContextFactory.Create();
            // design continuation here
            List<UdlandsopholdAnsoegning> loadedObjectsListe = new List<UdlandsopholdAnsoegning>();
            Microsoft.OData.Client.DataServiceQueryContinuation<UdlandsopholdAnsoegning> token = null;

            string datetimeoffSetFromLoadtimeCutOff = loadTimeCutoff.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var response = esasContainer.UdlandsopholdAnsoegning.AddQueryOption("$filter", $"ModifiedOn gt {datetimeoffSetFromLoadtimeCutOff}").Execute() as Microsoft.OData.Client.QueryOperationResponse<UdlandsopholdAnsoegning>;

            loadedObjectsListe.AddRange(response.ToList());
            if ( ! loadedObjectsListe.Any())
                return null;
            
            while ((token = response.GetContinuation()) != null)
            {
                System.Diagnostics.Debug.WriteLine("Continuation-token available, retrieving next batch.");
                esasContainer = _esasWebServiceContextFactory.Create();
                response = esasContainer.Execute<UdlandsopholdAnsoegning>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<UdlandsopholdAnsoegning>;
                loadedObjectsListe.AddRange(response.ToList());
            }

            return loadedObjectsListe.ToArray();
        }

        /// <summary>
        /// This method is only used for initial load of ESAS data - will become useless once we're in production.
        /// </summary>
        protected override object[] LoadObjectsAndPersistToDisk()
        {
            int i = 0; int j = 0;
            var esasContainer = _esasWebServiceContextFactory.Create();

            List<UdlandsopholdAnsoegning> loadedObjectsListe = new List<UdlandsopholdAnsoegning>();
            Microsoft.OData.Client.DataServiceQueryContinuation<UdlandsopholdAnsoegning> token = null;

            var response = esasContainer.UdlandsopholdAnsoegning.Execute() as Microsoft.OData.Client.QueryOperationResponse<UdlandsopholdAnsoegning>;
            loadedObjectsListe.AddRange(response.ToList());
            if ( loadedObjectsListe.Any())
            {
                j = loadedObjectsListe.Count();
                PersistToFile(i,j,ref loadedObjectsListe);
            }
            else
                return null;
            
            System.Diagnostics.Debug.WriteLine(loadedObjectsListe.Count());
         
            while ((token = response.GetContinuation()) != null)
            {
               Stopwatch st = new Stopwatch();
                st.Start();
                       esasContainer = _esasWebServiceContextFactory.Create();
                List<UdlandsopholdAnsoegning> loadedObjectsListen = new List<UdlandsopholdAnsoegning>();
                response = esasContainer.Execute<UdlandsopholdAnsoegning>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<UdlandsopholdAnsoegning>;
                loadedObjectsListen.AddRange(response.ToList());
                if ( loadedObjectsListen.Any())
            {
                i = j;
                j = j + loadedObjectsListen.Count();
                PersistToFile(i,j,ref loadedObjectsListen);
                loadedObjectsListen = null;
            }


                // loadedObjectsListe.AddRange(response.ToList()); - only persist to disc, for now.
               st.Stop();
               System.Diagnostics.Debug.WriteLine(j + "-" + st.Elapsed.Seconds);
            }
            
             System.Diagnostics.Debug.WriteLine($"*** total objects written to disk was {j} ***");
            //return loadedObjectsListe.ToArray();
            return null;
        }

        
        private void PersistToFile(int indexToStartLoadFrom, int howManyRecordsToGet, ref List<UdlandsopholdAnsoegning> loadedObjectsListe)
        {
             string entityName = "UdlandsopholdAnsoegning";
              if (!Directory.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}"))
                Directory.CreateDirectory($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}");

            string objectsJson = JsonConvert.SerializeObject(loadedObjectsListe, Formatting.Indented);
            if ( !File.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json"))
                File.WriteAllText($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json", objectsJson );

        }


    }


	// Denne klasse er auto-genereret. Enhver ændring vil blive overskrevet når auto-generingen kører på ny.
    public class VideregaaendeUddannelseLoadStrategy : BaseLoadStrategy
    {
         public VideregaaendeUddannelseLoadStrategy(EsasWsContextFactory esasWebServiceContextFactory, ILoadTimeStrategy loadTimeStrategy, ILogger logger) 
			: base(esasWebServiceContextFactory, loadTimeStrategy, logger)
        {
        }

        protected override object[] LoadObjects(DateTime loadTimeCutoff)
        {
            var esasContainer = _esasWebServiceContextFactory.Create();
            // design continuation here
            List<VideregaaendeUddannelse> loadedObjectsListe = new List<VideregaaendeUddannelse>();
            Microsoft.OData.Client.DataServiceQueryContinuation<VideregaaendeUddannelse> token = null;

            string datetimeoffSetFromLoadtimeCutOff = loadTimeCutoff.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var response = esasContainer.VideregaaendeUddannelse.AddQueryOption("$filter", $"ModifiedOn gt {datetimeoffSetFromLoadtimeCutOff}").Execute() as Microsoft.OData.Client.QueryOperationResponse<VideregaaendeUddannelse>;

            loadedObjectsListe.AddRange(response.ToList());
            if ( ! loadedObjectsListe.Any())
                return null;
            
            while ((token = response.GetContinuation()) != null)
            {
                System.Diagnostics.Debug.WriteLine("Continuation-token available, retrieving next batch.");
                esasContainer = _esasWebServiceContextFactory.Create();
                response = esasContainer.Execute<VideregaaendeUddannelse>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<VideregaaendeUddannelse>;
                loadedObjectsListe.AddRange(response.ToList());
            }

            return loadedObjectsListe.ToArray();
        }

        /// <summary>
        /// This method is only used for initial load of ESAS data - will become useless once we're in production.
        /// </summary>
        protected override object[] LoadObjectsAndPersistToDisk()
        {
            int i = 0; int j = 0;
            var esasContainer = _esasWebServiceContextFactory.Create();

            List<VideregaaendeUddannelse> loadedObjectsListe = new List<VideregaaendeUddannelse>();
            Microsoft.OData.Client.DataServiceQueryContinuation<VideregaaendeUddannelse> token = null;

            var response = esasContainer.VideregaaendeUddannelse.Execute() as Microsoft.OData.Client.QueryOperationResponse<VideregaaendeUddannelse>;
            loadedObjectsListe.AddRange(response.ToList());
            if ( loadedObjectsListe.Any())
            {
                j = loadedObjectsListe.Count();
                PersistToFile(i,j,ref loadedObjectsListe);
            }
            else
                return null;
            
            System.Diagnostics.Debug.WriteLine(loadedObjectsListe.Count());
         
            while ((token = response.GetContinuation()) != null)
            {
               Stopwatch st = new Stopwatch();
                st.Start();
                       esasContainer = _esasWebServiceContextFactory.Create();
                List<VideregaaendeUddannelse> loadedObjectsListen = new List<VideregaaendeUddannelse>();
                response = esasContainer.Execute<VideregaaendeUddannelse>(token.NextLinkUri) as Microsoft.OData.Client.QueryOperationResponse<VideregaaendeUddannelse>;
                loadedObjectsListen.AddRange(response.ToList());
                if ( loadedObjectsListen.Any())
            {
                i = j;
                j = j + loadedObjectsListen.Count();
                PersistToFile(i,j,ref loadedObjectsListen);
                loadedObjectsListen = null;
            }


                // loadedObjectsListe.AddRange(response.ToList()); - only persist to disc, for now.
               st.Stop();
               System.Diagnostics.Debug.WriteLine(j + "-" + st.Elapsed.Seconds);
            }
            
             System.Diagnostics.Debug.WriteLine($"*** total objects written to disk was {j} ***");
            //return loadedObjectsListe.ToArray();
            return null;
        }

        
        private void PersistToFile(int indexToStartLoadFrom, int howManyRecordsToGet, ref List<VideregaaendeUddannelse> loadedObjectsListe)
        {
             string entityName = "VideregaaendeUddannelse";
              if (!Directory.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}"))
                Directory.CreateDirectory($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}");

            string objectsJson = JsonConvert.SerializeObject(loadedObjectsListe, Formatting.Indented);
            if ( !File.Exists($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json"))
                File.WriteAllText($@"E:\ESAS_prod_fileBackupsAfPPdataUge8\{entityName}\{entityName}{indexToStartLoadFrom.ToString()}-{howManyRecordsToGet.ToString()}.json", objectsJson );

        }


    }



} // namespace end