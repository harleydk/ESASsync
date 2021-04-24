using Default;
using Synchronization.ESAS.DAL;
using Synchronization.ESAS.Models;
using Synchronization.ESAS.SyncDestinations;
using Synchronization.ESAS.Synchronizations;
using Synchronization.ESAS.Synchronizations.EntityLoaderStrategies;
using Synchronization.ESAS.UtilityComponents;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.OData.Client;
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

            CertifikatPassword = ConfigurationManager.AppSettings["esasFunktionsCertPassword"],
            CertifikatSti = string.Join(@"\", AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["esasFunktionsCertFilename"])
        };

        public static void Main()
        {
            _logger = InitializeLogger();
            _emailService = new DummyEmailService();

            try
            {
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true; // Hvis ESAS anvendes med et self-signed certifikat, kan vi ikke validere på det - certifikatets domæne vil ikke svare til localhost.

                _logger.LogInformation("Creating OData container connection.");
                Uri odataWs = new Uri(ConfigurationManager.AppSettings["EsasWsUri"]);
                var esasWebService = new Container(odataWs);

                var credentials = new NetworkCredential(
                    _esasSecuritySettings.Username,
                    _esasSecuritySettings.Password,
                    _esasSecuritySettings.Domain);
                esasWebService.Credentials = credentials;
                esasWebService.SendingRequest2 += addCertificateToRequest;

                #region examples of web-service use
#if (DEBUG)
                //#region web-service use inspiration...
                //var lande = esasWebService.Land.ToList();
                //var landeTake = esasWebService.Land.Take(10).ToList();
                //var landeTopOgSkip = esasWebService.Land.Skip(5).Take(10).ToList();
                //var landeByModified = esasWebService.Land.Where(l => l.ModifiedOn > DateTime.Now.AddDays(-2)).ToList();
                //var danmark = esasWebService.Land.Where(l => l.esas_navn == "Danmark").SingleOrDefault();
                //var danmarksMedPostnumre = esasWebService.Land.Expand(l => l.list_esas_postnummer).Where(l => l.esas_navn == "Danmark").SingleOrDefault();
                //var udvalgteAttrs = esasWebService.Land.AsEnumerable().Select(l => new { l.esas_navn, l.esas_engelsknavn }).ToList(); // sucks - everything is retrieved
                //var toDictionaryExample = esasWebService.Land.ToDictionary(key => key.esas_landId, value => value.esas_navn); // still sucks
                //esasWebService.AddToPerson(new person...);
                //esasWebService.SaveChanges();
#endif
                #endregion

                EsasWebServiceHealthChecker esasWebServiceHealthChecker = new EsasWebServiceHealthChecker(ConfigurationManager.AppSettings["EsasWsUri"], _esasSecuritySettings, _logger);
                _logger.LogInformation("Establishing sync-strategies.");

                IEsasDbContextFactory esasStagingDbContextFactory = new EsasDbContextFactory();
                IEnumerable<IEsasStagingDbDestination> esasStagingDbSyncStrategies = CreateEsasStagingDbDestinations();

                IEsasSyncDestination stagingDbDestination = new EsasStagingDbSyncDestination(esasStagingDbContextFactory, esasStagingDbSyncStrategies);
                ISyncResultsDestination whereToSendTheSyncResults = new EsasStagingDbLoadResultDestination(esasStagingDbContextFactory);
                ILoadTimeStrategy loadTimeStrategy = new LatestLoadStrategy(esasStagingDbContextFactory);

                IList<SyncStrategyBundle> syncStrategyBundles = new List<SyncStrategyBundle>();

                IEnumerable<IEsasSyncStrategy> standardEntitiesSyncs = CreateStandardEntitiesSyncs(esasWebService, whereToSendTheSyncResults, loadTimeStrategy, _logger, stagingDbDestination);

                // create strategy-bundles for N-minute intervals for 4 am thru 10 pm, i.e. 18 hours
                for (int hour = 6; hour < 22; hour++)
                {
                    for (int minutes = 0; minutes < 60; minutes += 20)
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
#if (DEBUG)
                // Debug? manual sync here we go
                EsasSynchronizationService service = new EsasSynchronizationService(syncStrategyBundles, esasStagingDbContextFactory, esasWebServiceHealthChecker, _logger, _emailService);
                foreach (var bundelOfSyncStrategies in syncStrategyBundles)
                {
                    bundelOfSyncStrategies.ExecuteSync();
                }
#else
                // Release? Windows service rock´n roll:
                ServiceBase.Run(ServicesToRun);
#endif
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                _emailService.SendStatusMail("Error in start of ESAS sync service", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Tilføj sikkerhedscertifikat til request'en, hvis denne kaldes på et miljø der kræver dette.
        /// </summary>
        private static void addCertificateToRequest(object sender, Microsoft.OData.Client.SendingRequest2EventArgs e)
        {
            X509Certificate2 certificate = new X509Certificate2();
            certificate.Import(_esasSecuritySettings.CertifikatSti, _esasSecuritySettings.CertifikatPassword, X509KeyStorageFlags.DefaultKeySet);
            if (null != certificate)
            {
                ((HttpWebRequestMessage)e.RequestMessage).HttpWebRequest.ClientCertificates.Add(certificate);

                DateTime expirationDate = DateTime.Parse(certificate.GetExpirationDateString());
                if (DateTime.Today.AddDays(14) >= expirationDate.Date)
                {
                    string certExpirationErrorMsg = @"Et ESAS sikkerhedscertifikat er tæt på at udløbe - om mindre end 14 dage vil denne applikation stoppe med at fungere, hvis ikke dette certifikat bliver fornyet. Ref. https://confluence.umit.dk/display/esasdokumentation/Systemtilslutning";
                    _emailService.SendStatusMail(certExpirationErrorMsg);
                    throw new Exception(certExpirationErrorMsg);
                }
            }
            else
            {
                throw new Exception("Sikkerhedscertifikat kunne ikke loades :-( - data vil ikke kunne hentes fra OData.");
            }
        }

        private static List<IEsasSyncStrategy> CreateStandardEntitiesSyncs(Container esasWebService, ISyncResultsDestination whereToSendTheLoadResults, ILoadTimeStrategy loadTimeStrategy, ILogger logger, IEsasSyncDestination stagingDbDestination)
        {
            // let's hook up some sync-strategies
            List<IEsasSyncStrategy> standardEntitiesSyncs = new List<IEsasSyncStrategy>();

            // Virksomheds- og person-oplysninger
            IEsasSyncStrategy LandSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 30 }, new LandLoadStrategy(esasWebService, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheLoadResults, logger);
            standardEntitiesSyncs.Add(LandSync);
            IEsasSyncStrategy PostNrSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 40 }, new PostnummerLoadStrategy(esasWebService, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheLoadResults, logger);
            standardEntitiesSyncs.Add(PostNrSync);
            IEsasSyncStrategy BrancheSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 45 }, new BrancheLoadStrategy(esasWebService, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheLoadResults, logger);
            standardEntitiesSyncs.Add(BrancheSync);

            IEsasSyncStrategy InstInstitutionsoplysningerSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 50 }, new InstitutionsoplysningerLoadStrategy(esasWebService, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheLoadResults, logger);
            standardEntitiesSyncs.Add(InstInstitutionsoplysningerSync);
            IEsasSyncStrategy InstitutionsVirksomhedSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 60 }, new InstitutionVirksomhedLoadStrategy(esasWebService, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheLoadResults, logger);
            standardEntitiesSyncs.Add(InstitutionsVirksomhedSync);
            IEsasSyncStrategy AfdelingSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 70 }, new AfdelingLoadStrategy(esasWebService, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheLoadResults, logger);
            standardEntitiesSyncs.Add(AfdelingSync);

            IEsasSyncStrategy PersonSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 75 }, new PersonLoadStrategy(esasWebService, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheLoadResults, logger);
            standardEntitiesSyncs.Add(PersonSync);
            IEsasSyncStrategy PersonOplysningSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 80 }, new PersonoplysningLoadStrategy(esasWebService, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheLoadResults, logger);
            standardEntitiesSyncs.Add(PersonOplysningSync);


            // Uddannelsesaktiviteter
            IEsasSyncStrategy UddannelsesaktivitetSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 100 }, new UddannelsesaktivitetLoadStrategy(esasWebService, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheLoadResults, logger);
            standardEntitiesSyncs.Add(UddannelsesaktivitetSync);
            IEsasSyncStrategy UddannelsesstrukturSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 200 }, new UddannelsesstrukturLoadStrategy(esasWebService, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheLoadResults, logger);
            standardEntitiesSyncs.Add(UddannelsesstrukturSync); // Uddannelsesstrukturen består af strukturelle uddannelseselementer på bekendtgørelsesniveau og på studieordningsniveau.
            IEsasSyncStrategy SueSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 250 }, new StruktureltUddannelseselementLoadStrategy(esasWebService, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheLoadResults, logger);
            standardEntitiesSyncs.Add(SueSync); // Et strukturelt uddannelseselement er enten et semester, et fag eller en gruppering
            IEsasSyncStrategy AktivitetsUdbudSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 300 }, new AktivitetsudbudLoadStrategy(esasWebService, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheLoadResults, logger);
            standardEntitiesSyncs.Add(AktivitetsUdbudSync);

            // PUE//Hold
            IEsasSyncStrategy PueSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 400 }, new PlanlaegningsUddannelseselementLoadStrategy(esasWebService, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheLoadResults, logger);
            standardEntitiesSyncs.Add(PueSync);
            IEsasSyncStrategy HoldSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 410 }, new HoldLoadStrategy(esasWebService, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheLoadResults, logger);
            standardEntitiesSyncs.Add(HoldSync);


            // Ansøgninger
            IEsasSyncStrategy AnsøgerSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 500 }, new AnsoegerLoadStrategy(esasWebService, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheLoadResults, logger);
            standardEntitiesSyncs.Add(AnsøgerSync);

            IEsasSyncStrategy AnsøgningskortopsætningSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 510 }, new AnsoegningskortOpsaetningLoadStrategy(esasWebService, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheLoadResults, logger);
            standardEntitiesSyncs.Add(AnsøgningskortopsætningSync);

            IEsasSyncStrategy AnsøgningsopsætningSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 520 }, new AnsoegningsopsaetningLoadStrategy(esasWebService, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheLoadResults, logger);
            standardEntitiesSyncs.Add(AnsøgningsopsætningSync);

            IEsasSyncStrategy AnsøgningsKorttekstSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 530 }, new AnsoegningskortTekstLoadStrategy(esasWebService, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheLoadResults, logger);
            standardEntitiesSyncs.Add(AnsøgningsKorttekstSync);

            IEsasSyncStrategy AnsøgningskortSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 540 }, new AnsoegningskortLoadStrategy(esasWebService, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheLoadResults, logger);
            standardEntitiesSyncs.Add(AnsøgningskortSync);

            IEsasSyncStrategy EksamenstypeSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 550 }, new EksamenstypeLoadStrategy(esasWebService, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheLoadResults, logger);
            standardEntitiesSyncs.Add(EksamenstypeSync);

            IEsasSyncStrategy OmrådenummerSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 555 }, new OmraadenummerLoadStrategy(esasWebService, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheLoadResults, logger);
            standardEntitiesSyncs.Add(OmrådenummerSync);

            IEsasSyncStrategy Områdenummeropsætningsync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 560 }, new OmraadenummeropsaetningLoadStrategy(esasWebService, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheLoadResults, logger);
            standardEntitiesSyncs.Add(Områdenummeropsætningsync);

            IEsasSyncStrategy OmrådeSpecialiseringSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 570 }, new OmraadespecialiseringLoadStrategy(esasWebService, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheLoadResults, logger);
            standardEntitiesSyncs.Add(OmrådeSpecialiseringSync);

            IEsasSyncStrategy AdgangskravSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 580 }, new AdgangskravLoadStrategy(esasWebService, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheLoadResults, logger);
            standardEntitiesSyncs.Add(AdgangskravSync);

            IEsasSyncStrategy AfslagsbegrundelseSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 590 }, new AfslagsbegrundelseLoadStrategy(esasWebService, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheLoadResults, logger);
            standardEntitiesSyncs.Add(AfslagsbegrundelseSync);

            IEsasSyncStrategy AnsøgningSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 600 }, new AnsoegningLoadStrategy(esasWebService, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheLoadResults, logger);
            standardEntitiesSyncs.Add(AnsøgningSync);

            IEsasSyncStrategy AnsøgninghandlingSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 610 }, new AnsoegningshandlingLoadStrategy(esasWebService, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheLoadResults, logger);
            standardEntitiesSyncs.Add(AnsøgninghandlingSync);

            IEsasSyncStrategy AnsoegningPlanlaegningsUddannelseselementSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 615 }, new AnsoegningPlanlaegningsUddannelseselementLoadStrategy(esasWebService, logger), stagingDbDestination, whereToSendTheLoadResults, logger);
            standardEntitiesSyncs.Add(AnsoegningPlanlaegningsUddannelseselementSync);

            IEsasSyncStrategy BilagSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 620 }, new BilagLoadStrategy(esasWebService, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheLoadResults, logger);
            standardEntitiesSyncs.Add(BilagSync);

            IEsasSyncStrategy NationalAfgangsårsagSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 630 }, new NationalAfgangsaarsagLoadStrategy(esasWebService, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheLoadResults, logger);
            standardEntitiesSyncs.Add(NationalAfgangsårsagSync);

            IEsasSyncStrategy EnkeltfagSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 640 }, new EnkeltfagLoadStrategy(esasWebService, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheLoadResults, logger);
            standardEntitiesSyncs.Add(EnkeltfagSync);


            // GUEer/Studieforløb

            IEsasSyncStrategy StudieforløbSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 700 }, new StudieforloebLoadStrategy(esasWebService, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheLoadResults, logger);
            standardEntitiesSyncs.Add(StudieforløbSync);

            IEsasSyncStrategy BedømmelsesrundeSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 720 }, new BedoemmelsesrundeLoadStrategy(esasWebService, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheLoadResults, logger);
            standardEntitiesSyncs.Add(BedømmelsesrundeSync);
            IEsasSyncStrategy KarakterSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 740 }, new KarakterLoadStrategy(esasWebService, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheLoadResults, logger);
            standardEntitiesSyncs.Add(KarakterSync);

            IEsasSyncStrategy GueSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 750 }, new GennemfoerelsesUddannelseselementLoadStrategy(esasWebService, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheLoadResults, logger);
            standardEntitiesSyncs.Add(GueSync);

            IEsasSyncStrategy Bedømmelsesync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 760 }, new BedoemmelseLoadStrategy(esasWebService, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheLoadResults, logger);
            standardEntitiesSyncs.Add(Bedømmelsesync);

            // TODO: Fraværsårsag mangler endpoint - afvent SIU
            //IEsasSyncStrategy FravaersaarsagSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 770 }, new FravaersaarsagLoadStrategy(esasWebService, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheLoadResults, logger);
            //standardEntitiesSyncs.Add(FravaersaarsagSync);

            IEsasSyncStrategy StudieinaktivPeriodeSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 780 }, new StudieinaktivPeriodeLoadStrategy(esasWebService, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheLoadResults, logger);
            standardEntitiesSyncs.Add(StudieinaktivPeriodeSync);

            IEsasSyncStrategy MeritRegistreringSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 790 }, new MeritRegistreringLoadStrategy(esasWebService, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheLoadResults, logger);
            standardEntitiesSyncs.Add(MeritRegistreringSync);

            IEsasSyncStrategy PraktikomraadeSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 800 }, new PraktikomraadeLoadStrategy(esasWebService, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheLoadResults, logger);
            standardEntitiesSyncs.Add(PraktikomraadeSync);
            IEsasSyncStrategy PraktikopholdSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 810 }, new PraktikopholdLoadStrategy(esasWebService, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheLoadResults, logger);
            standardEntitiesSyncs.Add(PraktikopholdSync);

            IEsasSyncStrategy BevisgrundlagSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 820 }, new BevisgrundlagLoadStrategy(esasWebService, loadTimeStrategy, logger), stagingDbDestination, whereToSendTheLoadResults, logger);
            standardEntitiesSyncs.Add(BevisgrundlagSync);

            // Øvrige
            IEsasSyncStrategy OptionSetValueSync = new EsasSyncStrategy(new SyncStrategySettings { SyncPriorityNumber = 1000 }, new OptionSetValueLoadStrategy(esasWebService, logger), stagingDbDestination, whereToSendTheLoadResults, logger);
            standardEntitiesSyncs.Add(OptionSetValueSync);

            //esasDataLoaders.Add(new AndenAktivitetLoadStrategy(esasWebService, loadTimeStrategy, logger)); --er ikke et endpoint i sig selv
            //esasDataLoaders.Add(new ErfaringerLoadStrategy(esasWebService, loadTimeStrategy, logger));
            //esasDataLoaders.Add(new FagpersonsrelationLoadStrategy(esasWebService, loadTimeStrategy, logger));
            //esasDataLoaders.Add(new GebyrtypePUERelationLoadStrategy(esasWebService, loadTimeStrategy, logger));

            //esasDataLoaders.Add(new HoldStudieforloebLoadStrategy(esasWebService, loadTimeStrategy, logger)); --indeholder ikke modifiedOn timestamp, det er en ren relationstabel

            //esasDataLoaders.Add(new KommunikationLoadStrategy(esasWebService, loadTimeStrategy, logger));
            //esasDataLoaders.Add(new KurserSkoleopholdLoadStrategy(esasWebService, loadTimeStrategy, logger));


            //esasDataLoaders.Add(new ProeveLoadStrategy(esasWebService, loadTimeStrategy, logger));
            //esasDataLoaders.Add(new PubliceringLoadStrategy(esasWebService, loadTimeStrategy, logger));

            //esasDataLoaders.Add(new UdlandsopholdAnsoegningLoadStrategy(esasWebService, loadTimeStrategy, logger));
            //esasDataLoaders.Add(new VideregaaendeUddannelseLoadStrategy(esasWebService, loadTimeStrategy, logger));

            return standardEntitiesSyncs;
        }

        /// <summary>
        /// Create Esas staging db destinations. Doing it in reflection saves a bit of typing.
        /// </summary>
        private static IEnumerable<IEsasStagingDbDestination> CreateEsasStagingDbDestinations()
        {
            IList<IEsasStagingDbDestination> strategies = new List<IEsasStagingDbDestination>();

            Type[] typelist = GetTypesInNamespace(Assembly.GetExecutingAssembly(), "Synchronization.ESAS.Synchronizations.EsasStagingDbSyncStrategies").Where(t => t.Name.EndsWith("EsasStagingDbDestination")).ToArray();
            for (int i = 0; i < typelist.Length; i++)
            {
                var instance = Activator.CreateInstance(typelist[i]);
                strategies.Add((IEsasStagingDbDestination)instance);
            }

            return strategies;
        }

        /// <summary>
        /// Initialisér logning.
        /// </summary>
        private static ILogger InitializeLogger()
        {
            // TODO: roll your own logger
            return NullLogger.Instance;
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
