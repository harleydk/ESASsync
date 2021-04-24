using Default;
using esas.Dynamics.Models.Contracts;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.OData.Client;
using Synchronization.ESAS.DAL;
using Synchronization.ESAS.Models;
using Synchronization.ESAS.SyncDestinations;
using Synchronization.ESAS.Synchronizations;
using Synchronization.ESAS.Synchronizations.EntityLoaderStrategies;
using Synchronization.ESAS.UtilityComponents;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.ServiceProcess;

namespace Synchronization.ESAS
{
    public partial class Program
    {
        private static ILogger _logger;
        private static IEmailService _emailService;

        private static EsasSecuritySettings _esasSecuritySettings = new EsasSecuritySettings
        {
            Username = ConfigurationManager.AppSettings["EsasIntegrationUserName"],
            Password = ConfigurationManager.AppSettings["EsasIntegrationPassword"],
            Domain = ConfigurationManager.AppSettings["EsasIntegrationDomain"],

            CertificatePassword = ConfigurationManager.AppSettings["esasFunktionsCertPassword"],
            CertificatePath = string.Join(@"\", AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["esasFunktionsCertFilename"])
        };

        public static void Main()
        {
            try
            {
                _logger = InitializeLogger();
                _emailService = new DummyEmailService();
                _logger.LogInformation("Creating OData container connection.");
      
                Uri odataWs = new Uri(ConfigurationManager.AppSettings["EsasWsUri"]);

                EsasWsContextFactory esasWsContextFactory = new EsasWsContextFactory(_esasSecuritySettings, odataWs, _logger);

                EsasWebServiceHealthChecker esasWebServiceHealthChecker = new EsasWebServiceHealthChecker(ConfigurationManager.AppSettings["EsasWsUri"], _esasSecuritySettings, _logger);
                _logger.LogInformation("Establishing sync-strategies.");

                IEsasDbContextFactory esasStagingDbContextFactory = new EsasDbContextFactory();
                IEnumerable<IEsasStagingDbDestination> esasStagingDbSyncStrategies = CreateEsasStagingDbDestinations(_logger);

                IEsasSyncDestination stagingDbDestination = new EsasStagingDbSyncDestination(esasStagingDbContextFactory, esasStagingDbSyncStrategies);
                ISyncResultsDestination whereToSendTheSyncResults = new EsasStagingDbLoadResultDestination(esasStagingDbContextFactory);
                ILoadTimeStrategy loadTimeStrategy = new LatestSuccesfulLoadStrategy(esasStagingDbContextFactory);

                IList<SyncStrategyBundle> syncStrategyBundles = new List<SyncStrategyBundle>();

                IEnumerable<IEsasSyncStrategy> standardEntitiesSyncs = CreateStandardEntitiesSyncs(esasWsContextFactory, whereToSendTheSyncResults, loadTimeStrategy, _logger, stagingDbDestination);

                // Create strategy-bundles for N-minute intervals for A am thru B pm, fx. 6 to 22:
                for (int hour = 6; hour < 22; hour++)
                {
                    for (int minutes = 0; minutes < 60; minutes += 30)
                    {
                        TimeSpan syncTimeSpan = new TimeSpan(hour, minutes, 0);
                        SyncStrategyBundle bundle = new SyncStrategyBundle(syncTimeSpan, standardEntitiesSyncs, _logger);
                        syncStrategyBundles.Add(bundle);
                    }
                }

                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                    new EsasSynchronizationService(syncStrategyBundles, esasStagingDbContextFactory, esasWebServiceHealthChecker, _logger, _emailService)
                };
#if (!DEBUG)
                // windows service rock'n roll
                ServiceBase.Run(ServicesToRun);
#else
                #region devSync - for manuel syncs.


                List<string> positivListe = new List<string>();

                #region strategyList
                // ***Virksomheds - og person - oplysninger ***
                positivListe.Add("LandLoadStrategy");
                positivListe.Add("KommuneLoadStrategy");
                positivListe.Add("PostnummerLoadStrategy");
                positivListe.Add("BrancheLoadStrategy");
                positivListe.Add("InstitutionVirksomhedLoadStrategy");
                positivListe.Add("InstitutionsoplysningerLoadStrategy");
                positivListe.Add("AfdelingsniveauLoadStrategy");
                positivListe.Add("AfdelingLoadStrategy");
                positivListe.Add("PersonLoadStrategy");
                positivListe.Add("PersonoplysningLoadStrategy");

                // *** opsætningstabeller, som øvrige syncs er afhængige af. ***
                positivListe.Add("AnsoegningskortOpsaetningLoadStrategy");
                positivListe.Add("PubliceringLoadStrategy");
                positivListe.Add("AdgangskravLoadStrategy");
                positivListe.Add("FravaersaarsagLoadStrategy");

                ////////// *** Uddannelsesaktiviteter ***
                positivListe.Add("UddannelsesaktivitetLoadStrategy");
                positivListe.Add("UddannelsesstrukturLoadStrategy");
                positivListe.Add("StruktureltUddannelseselementLoadStrategy");
                positivListe.Add("AktivitetsudbudLoadStrategy");

                //// *** PUE//Hold ***
                positivListe.Add("PlanlaegningsUddannelseselementLoadStrategy");
                positivListe.Add("HoldLoadStrategy");
                positivListe.Add("SamlaesningLoadStrategy");

                //// *** Ansøgninger ***
                positivListe.Add("RekvirenttypeLoadStrategy");
                positivListe.Add("AnsoegerLoadStrategy");
                positivListe.Add("AnsoegningsopsaetningLoadStrategy");
                positivListe.Add("AnsoegningskortTekstLoadStrategy");
                positivListe.Add("AnsoegningskortLoadStrategy");
                positivListe.Add("EksamenstypeLoadStrategy");
                positivListe.Add("OmraadenummerLoadStrategy");
                positivListe.Add("OmraadenummeropsaetningLoadStrategy");
                positivListe.Add("OmraadespecialiseringLoadStrategy");
                positivListe.Add("SpecialiseringLoadStrategy");
                positivListe.Add("AfslagsbegrundelseLoadStrategy");
                positivListe.Add("AnsoegningLoadStrategy");
                positivListe.Add("AnsoegningshandlingLoadStrategy");
                positivListe.Add("AnsoegningPlanlaegningsUddannelseselementLoadStrategy");
                positivListe.Add("BilagLoadStrategy");
                positivListe.Add("NationalAfgangsaarsagLoadStrategy");
                positivListe.Add("EnkeltfagLoadStrategy");

                //***GUEer / Studieforløb * **
                positivListe.Add("IndskrivningsformLoadStrategy");
                positivListe.Add("StudieforloebLoadStrategy");
                positivListe.Add("BedoemmelsesrundeLoadStrategy");
                positivListe.Add("KarakterLoadStrategy");
                positivListe.Add("GennemfoerelsesUddannelseselementLoadStrategy");
                positivListe.Add("BedoemmelseLoadStrategy");
                positivListe.Add("StudieinaktivPeriodeLoadStrategy");
                positivListe.Add("MeritRegistreringLoadStrategy");
                positivListe.Add("PraktikomraadeLoadStrategy");
                positivListe.Add("PraktikopholdLoadStrategy");
                //positivListe.Add("BevisgrundlagLoadStrategy"); --meget stor og omfattende tabel, er ikke sikker på dens forretningsværdi.Udelader den, for now.

                ////// *** Øvrige ***
                positivListe.Add("OptionSetValueLoadStrategy");
                #endregion strategyList

                var debugSyncStrategy = new LatestSuccesfulLoadStrategy(esasStagingDbContextFactory);
                var debugSyncStrategies = CreateStandardEntitiesSyncs(esasWsContextFactory, whereToSendTheSyncResults, debugSyncStrategy, _logger, stagingDbDestination);

                //// tilføj initial-optionsetvaluestring load
                IEsasSyncStrategy initialOptionSetValueBulkLoadStrategy = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 1000 },
                    new OptionSetValueLoadStrategy(esasWsContextFactory, _logger), stagingDbDestination, whereToSendTheSyncResults, _logger);
                debugSyncStrategies.Add(initialOptionSetValueBulkLoadStrategy);

                var initielleStrategier = debugSyncStrategies.Where(strat =>
                        positivListe.Contains(strat.EsasEntitiesLoaderStrategy.GetType().Name)
                        ||
                        strat.EsasEntitiesLoaderStrategy.GetType().Name == "OptionSetValueInitialBulkLoadStrategy"
                    );
                SyncStrategyBundle initialSaveToFileSyncBundle = new SyncStrategyBundle(
                    syncTime: new TimeSpan(01, 00, 00), // let's go back just a day for this manuel debug sync
                    syncStrategies: initielleStrategier,
                    logger: _logger);

                EsasSynchronizationService service = new EsasSynchronizationService(new List<SyncStrategyBundle>() { initialSaveToFileSyncBundle }, esasStagingDbContextFactory, esasWebServiceHealthChecker, _logger, _emailService);
                initialSaveToFileSyncBundle.ExecuteSync();

                #endregion
#endif
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                _emailService.SendStatusMail("Error in start of ESAS sync service", ex.Message);
                throw;
            }
        }


        private static List<IEsasSyncStrategy> CreateStandardEntitiesSyncs(EsasWsContextFactory esasWsContextFactory, ISyncResultsDestination whereToSendTheSyncResults, ILoadTimeStrategy loadTimeStrategy, ILogger logger, IEsasSyncDestination stagingDbDestination)
        {
            // let's hook up some sync-strategies
            List<IEsasSyncStrategy> standardEntitiesSyncs = new List<IEsasSyncStrategy>();

            // Virksomheds- og person-oplysninger
            IEsasSyncStrategy LandSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 30 }, new LandLoadStrategy(esasWsContextFactory, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheSyncResults, logger);
            standardEntitiesSyncs.Add(LandSync);
            IEsasSyncStrategy KommuneSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 35 }, new KommuneLoadStrategy(esasWsContextFactory, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheSyncResults, logger);
            standardEntitiesSyncs.Add(KommuneSync);
            IEsasSyncStrategy PostNrSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 40 }, new PostnummerLoadStrategy(esasWsContextFactory, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheSyncResults, logger);
            standardEntitiesSyncs.Add(PostNrSync);
            IEsasSyncStrategy BrancheSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 45 }, new BrancheLoadStrategy(esasWsContextFactory, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheSyncResults, logger);
            standardEntitiesSyncs.Add(BrancheSync);

            IEsasSyncStrategy InstitutionsVirksomhedSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 50 }, new InstitutionVirksomhedLoadStrategy(esasWsContextFactory, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheSyncResults, logger);
            standardEntitiesSyncs.Add(InstitutionsVirksomhedSync);
            IEsasSyncStrategy InstInstitutionsoplysningerSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 60 }, new InstitutionsoplysningerLoadStrategy(esasWsContextFactory, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheSyncResults, logger);
            standardEntitiesSyncs.Add(InstInstitutionsoplysningerSync);
            IEsasSyncStrategy AAfdelingsniveauSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 65 }, new AfdelingsniveauLoadStrategy(esasWsContextFactory, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheSyncResults, logger);
            standardEntitiesSyncs.Add(AAfdelingsniveauSync);
            IEsasSyncStrategy AfdelingSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 70 }, new AfdelingLoadStrategy(esasWsContextFactory, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheSyncResults, logger);
            standardEntitiesSyncs.Add(AfdelingSync);

            IEsasSyncStrategy PersonSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 75 }, new PersonLoadStrategy(esasWsContextFactory, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheSyncResults, logger);
            standardEntitiesSyncs.Add(PersonSync);
            IEsasSyncStrategy PersonOplysningSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 80 }, new PersonoplysningLoadStrategy(esasWsContextFactory, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheSyncResults, logger);
            standardEntitiesSyncs.Add(PersonOplysningSync);

            // opsætningstabeller, som øvrige syncs er afhængige af.
            IEsasSyncStrategy AnsøgningskortopsætningSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 90 }, new AnsoegningskortOpsaetningLoadStrategy(esasWsContextFactory, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheSyncResults, logger);
            standardEntitiesSyncs.Add(AnsøgningskortopsætningSync);
            IEsasSyncStrategy PubliceringsSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 91 }, new PubliceringLoadStrategy(esasWsContextFactory, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheSyncResults, logger);
            standardEntitiesSyncs.Add(PubliceringsSync);
            IEsasSyncStrategy AdgangskravSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 92 }, new AdgangskravLoadStrategy(esasWsContextFactory, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheSyncResults, logger);
            standardEntitiesSyncs.Add(AdgangskravSync);


            // Uddannelsesaktiviteter
            IEsasSyncStrategy UddannelsesaktivitetSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 100 }, new UddannelsesaktivitetLoadStrategy(esasWsContextFactory, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheSyncResults, logger);
            standardEntitiesSyncs.Add(UddannelsesaktivitetSync);
            IEsasSyncStrategy UddannelsesstrukturSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 200 }, new UddannelsesstrukturLoadStrategy(esasWsContextFactory, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheSyncResults, logger);
            standardEntitiesSyncs.Add(UddannelsesstrukturSync); // Uddannelsesstrukturen består af strukturelle uddannelseselementer på bekendtgørelsesniveau og på studieordningsniveau.
            IEsasSyncStrategy SueSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 250 }, new StruktureltUddannelseselementLoadStrategy(esasWsContextFactory, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheSyncResults, logger);
            standardEntitiesSyncs.Add(SueSync); // Et strukturelt uddannelseselement er enten et semester, et fag eller en gruppering
            IEsasSyncStrategy AktivitetsUdbudSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 300 }, new AktivitetsudbudLoadStrategy(esasWsContextFactory, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheSyncResults, logger);
            standardEntitiesSyncs.Add(AktivitetsUdbudSync);

            // PUE//Hold
            IEsasSyncStrategy PueSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 400 }, new PlanlaegningsUddannelseselementLoadStrategy(esasWsContextFactory, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheSyncResults, logger);
            standardEntitiesSyncs.Add(PueSync);
            IEsasSyncStrategy HoldSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 410 }, new HoldLoadStrategy(esasWsContextFactory, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheSyncResults, logger);
            standardEntitiesSyncs.Add(HoldSync);
            IEsasSyncStrategy SamlæsningSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 420 }, new SamlaesningLoadStrategy(esasWsContextFactory, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheSyncResults, logger);
            standardEntitiesSyncs.Add(SamlæsningSync);

            // Ansøgninger
            IEsasSyncStrategy RekvirenttypeSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 490 }, new RekvirenttypeLoadStrategy(esasWsContextFactory, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheSyncResults, logger);
            standardEntitiesSyncs.Add(RekvirenttypeSync);
            IEsasSyncStrategy AnsøgerSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 500 }, new AnsoegerLoadStrategy(esasWsContextFactory, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheSyncResults, logger);
            standardEntitiesSyncs.Add(AnsøgerSync);

            IEsasSyncStrategy AnsøgningsopsætningSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 520 }, new AnsoegningsopsaetningLoadStrategy(esasWsContextFactory, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheSyncResults, logger);
            standardEntitiesSyncs.Add(AnsøgningsopsætningSync);

            IEsasSyncStrategy AnsøgningsKorttekstSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 530 }, new AnsoegningskortTekstLoadStrategy(esasWsContextFactory, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheSyncResults, logger);
            standardEntitiesSyncs.Add(AnsøgningsKorttekstSync);

            IEsasSyncStrategy AnsøgningskortSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 540 }, new AnsoegningskortLoadStrategy(esasWsContextFactory, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheSyncResults, logger);
            standardEntitiesSyncs.Add(AnsøgningskortSync);

            IEsasSyncStrategy EksamenstypeSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 550 }, new EksamenstypeLoadStrategy(esasWsContextFactory, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheSyncResults, logger);
            standardEntitiesSyncs.Add(EksamenstypeSync);

            IEsasSyncStrategy OmrådenummerSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 555 }, new OmraadenummerLoadStrategy(esasWsContextFactory, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheSyncResults, logger);
            standardEntitiesSyncs.Add(OmrådenummerSync);

            IEsasSyncStrategy Områdenummeropsætningsync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 560 }, new OmraadenummeropsaetningLoadStrategy(esasWsContextFactory, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheSyncResults, logger);
            standardEntitiesSyncs.Add(Områdenummeropsætningsync);

            IEsasSyncStrategy OmrådeSpecialiseringSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 570 }, new OmraadespecialiseringLoadStrategy(esasWsContextFactory, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheSyncResults, logger);
            standardEntitiesSyncs.Add(OmrådeSpecialiseringSync);

            IEsasSyncStrategy SpecialiseringSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 580 }, new SpecialiseringLoadStrategy(esasWsContextFactory, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheSyncResults, logger);
            standardEntitiesSyncs.Add(SpecialiseringSync);

            IEsasSyncStrategy AfslagsbegrundelseSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 590 }, new AfslagsbegrundelseLoadStrategy(esasWsContextFactory, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheSyncResults, logger);
            standardEntitiesSyncs.Add(AfslagsbegrundelseSync);

            IEsasSyncStrategy AnsøgningSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 600 }, new AnsoegningLoadStrategy(esasWsContextFactory, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheSyncResults, logger);
            standardEntitiesSyncs.Add(AnsøgningSync);

            IEsasSyncStrategy AnsøgninghandlingSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 610 }, new AnsoegningshandlingLoadStrategy(esasWsContextFactory, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheSyncResults, logger);
            standardEntitiesSyncs.Add(AnsøgninghandlingSync);

            IEsasSyncStrategy BilagSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 620 }, new BilagLoadStrategy(esasWsContextFactory, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheSyncResults, logger);
            standardEntitiesSyncs.Add(BilagSync);

            IEsasSyncStrategy NationalAfgangsårsagSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 630 }, new NationalAfgangsaarsagLoadStrategy(esasWsContextFactory, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheSyncResults, logger);
            standardEntitiesSyncs.Add(NationalAfgangsårsagSync);

            IEsasSyncStrategy EnkeltfagSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 640 }, new EnkeltfagLoadStrategy(esasWsContextFactory, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheSyncResults, logger);
            standardEntitiesSyncs.Add(EnkeltfagSync);


            // GUEer/Studieforløb
            IEsasSyncStrategy IndskrivningsformSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 695 }, new IndskrivningsformLoadStrategy(esasWsContextFactory, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheSyncResults, logger);
            standardEntitiesSyncs.Add(IndskrivningsformSync);

            IEsasSyncStrategy StudieforløbSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 700 }, new StudieforloebLoadStrategy(esasWsContextFactory, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheSyncResults, logger);
            standardEntitiesSyncs.Add(StudieforløbSync);

            IEsasSyncStrategy BedømmelsesrundeSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 720 }, new BedoemmelsesrundeLoadStrategy(esasWsContextFactory, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheSyncResults, logger);
            standardEntitiesSyncs.Add(BedømmelsesrundeSync);
            IEsasSyncStrategy KarakterSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 740 }, new KarakterLoadStrategy(esasWsContextFactory, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheSyncResults, logger);
            standardEntitiesSyncs.Add(KarakterSync);

            IEsasSyncStrategy GueSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 750 }, new GennemfoerelsesUddannelseselementLoadStrategy(esasWsContextFactory, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheSyncResults, logger);
            standardEntitiesSyncs.Add(GueSync);

            IEsasSyncStrategy Bedømmelsesync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 760 }, new BedoemmelseLoadStrategy(esasWsContextFactory, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheSyncResults, logger);
            standardEntitiesSyncs.Add(Bedømmelsesync);

            // TODO: Fraværsårsag mangler endpoint - afvent SIU
            IEsasSyncStrategy FravaersaarsagSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 770 }, new FravaersaarsagLoadStrategy(esasWsContextFactory, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheSyncResults, logger);
            standardEntitiesSyncs.Add(FravaersaarsagSync);

            IEsasSyncStrategy StudieinaktivPeriodeSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 780 }, new StudieinaktivPeriodeLoadStrategy(esasWsContextFactory, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheSyncResults, logger);
            standardEntitiesSyncs.Add(StudieinaktivPeriodeSync);

            IEsasSyncStrategy MeritRegistreringSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 790 }, new MeritRegistreringLoadStrategy(esasWsContextFactory, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheSyncResults, logger);
            standardEntitiesSyncs.Add(MeritRegistreringSync);

            IEsasSyncStrategy PraktikomraadeSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 800 }, new PraktikomraadeLoadStrategy(esasWsContextFactory, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheSyncResults, logger);
            standardEntitiesSyncs.Add(PraktikomraadeSync);
            IEsasSyncStrategy PraktikopholdSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 810 }, new PraktikopholdLoadStrategy(esasWsContextFactory, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheSyncResults, logger);
            standardEntitiesSyncs.Add(PraktikopholdSync);

            //IEsasSyncStrategy BevisgrundlagSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 820 }, new BevisgrundlagLoadStrategy(esasWsContextFactory, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheSyncResults, logger);
            //standardEntitiesSyncs.Add(BevisgrundlagSync); // Meget stor og omfattende tabel, er ikke sikker på dens forretningsværdi. Undlader den, for now.

            return standardEntitiesSyncs;
        }

        /// <summary>
        /// Create Esas staging db destinations.
        /// </summary>
        private static IEnumerable<IEsasStagingDbDestination> CreateEsasStagingDbDestinations(ILogger logger)
        {
            IList<IEsasStagingDbDestination> strategies = new List<IEsasStagingDbDestination>();

            Type[] typelist = GetTypesInNamespace(Assembly.GetExecutingAssembly(), "KP.Synchronization.ESAS.Synchronizations.EsasStagingDbSyncStrategies").Where(t => t.Name.EndsWith("EsasStagingDbDestination")).ToArray();
            for (int i = 0; i < typelist.Length; i++)
            {
                var instance = Activator.CreateInstance(typelist[i], new object[] { logger });
                strategies.Add((IEsasStagingDbDestination)instance);
            }

            return strategies;
        }

        /// <summary>
        /// Initialisér logning.
        /// </summary>
        private static ILogger InitializeLogger()
        {
            return NullLogger.Instance; // TODO: roll your own
        }


        private static Type[] GetTypesInNamespace(Assembly assembly, string nameSpace)
        {
            return
              assembly.GetTypes()
                      .Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal))
                      .ToArray();
        }
    }
}
