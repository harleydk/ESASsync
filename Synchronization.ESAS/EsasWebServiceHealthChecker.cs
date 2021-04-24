using Synchronization.ESAS.DAL.Models;
using Synchronization.ESAS.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Configuration;
using System.Data.Services.Client;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace Synchronization.ESAS
{
    /// <summary>
    /// En hjælpekomponent der kan kalde et dedikeret health-check endpoint på ESAS odata-servicen.
    /// </summary>
    public class EsasWebServiceHealthChecker
    {
        private const string IS_ALIVE_ENDPOINT = "isAlive";
        private const string ODATA_METADATA_ENDPOINT = "$metadata";

        private readonly string _esasWsUri;
        private readonly EsasSecuritySettings _esasSecuritySettings;
        private readonly ILogger _logger;

        public EsasWebServiceHealthChecker(string esasWsUri, EsasSecuritySettings esasSecuritySettings, ILogger logger)
        {
            _esasWsUri = esasWsUri;
            _esasSecuritySettings = esasSecuritySettings;
            _logger = logger;
        }

        public EsasWebServiceHealthCheck PerformHealthCheck()
        {
            _logger.LogInformation("Performing health-check...");
            string wsAliveUri = string.Join(@"/", _esasWsUri, IS_ALIVE_ENDPOINT);
            EsasWebServiceHealthCheck healthCheck = new EsasWebServiceHealthCheck
            {
                CheckTime = DateTime.Now
            };

            Stopwatch sw = new Stopwatch();
            sw.Start();

            try
            {
                HttpWebRequest isAliveWebRequest = (HttpWebRequest)HttpWebRequest.Create(wsAliveUri);

                string esasIntegrationUserName = ConfigurationManager.AppSettings["EsasIntegrationUserName"];
                string esasIntegrationPassword = ConfigurationManager.AppSettings["EsasIntegrationPassword"];
                string esasIntegrationDomain = ConfigurationManager.AppSettings["EsasIntegrationDomain"];
                isAliveWebRequest.Credentials = new NetworkCredential(esasIntegrationUserName, esasIntegrationPassword, esasIntegrationDomain);

                // Importér fra PFX/P12, hvis en sådan findes:
                X509Certificate2 certificate = new X509Certificate2();
                certificate.Import(_esasSecuritySettings.CertificatePath, _esasSecuritySettings.CertificatePassword, X509KeyStorageFlags.DefaultKeySet);
                if (certificate == null)
                {
                    _logger.LogError("Could not add ESAS certificate!");
                    throw new Exception("Could not add ESAS certificate!");
                }

                isAliveWebRequest.ClientCertificates.Add(certificate);

                var response = isAliveWebRequest.GetResponse();

                HttpStatusCode status = (response as HttpWebResponse).StatusCode;
                healthCheck.HttpStatusCode = ((int)status).ToString();

                StreamReader sr = new StreamReader(response.GetResponseStream());
                using (sr)
                {
                    healthCheck.Message = sr.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                HttpStatusCode status = (ex.Response as HttpWebResponse).StatusCode;
                healthCheck.HttpStatusCode = ((int)status).ToString();
                healthCheck.Message = ex.Message;
            }
            catch (Exception ex)
            {
                string errorMessage = $"Der opstod en fejl i forsøget på at kontakte ESAS web-servicen. Fejlen var {ex.Message}";
                healthCheck.HttpStatusCode = "500";
                healthCheck.Message = errorMessage;
                _logger.LogError(errorMessage);
            }

            sw.Stop();
            healthCheck.CheckTimeMs = sw.ElapsedMilliseconds;
            return healthCheck;
        }

        public string GetMetaData()
        {
            _logger.LogInformation("Getting metadata...");
            string wsAliveUri = string.Join(@"/", _esasWsUri, ODATA_METADATA_ENDPOINT);

            try
            {
                HttpWebRequest isAliveWebRequest = (HttpWebRequest)HttpWebRequest.Create(wsAliveUri);

                string esasIntegrationUserName = ConfigurationManager.AppSettings["EsasIntegrationUserName"];
                string esasIntegrationPassword = ConfigurationManager.AppSettings["EsasIntegrationPassword"];
                string esasIntegrationDomain = ConfigurationManager.AppSettings["EsasIntegrationDomain"];
                isAliveWebRequest.Credentials = new NetworkCredential(esasIntegrationUserName, esasIntegrationPassword, esasIntegrationDomain);

                // Importér fra PFX/P12, hvis en sådan findes:
                X509Certificate2 certificate = new X509Certificate2();
                certificate.Import(_esasSecuritySettings.CertificatePath, _esasSecuritySettings.CertificatePassword, X509KeyStorageFlags.DefaultKeySet);
                if (certificate == null)
                {
                    _logger.LogError("Could not add ESAS certificate!");
                    throw new Exception("Could not add ESAS certificate!");
                }

                isAliveWebRequest.ClientCertificates.Add(certificate);

                var response = isAliveWebRequest.GetResponse();
                HttpStatusCode status = (response as HttpWebResponse).StatusCode;
                if ((int)status != 200)
                    throw new WebException("$metadata not available for OData-service");

                StreamReader sr = new StreamReader(response.GetResponseStream());
                using (sr)
                {
                    return sr.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }


}
