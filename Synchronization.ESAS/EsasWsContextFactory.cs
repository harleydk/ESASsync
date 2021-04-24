using Default;
using Synchronization.ESAS.Models;
using Microsoft.Extensions.Logging;
using Microsoft.OData.Client;
using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace Synchronization.ESAS
{
    public class EsasWsContextFactory
    {
        private EsasSecuritySettings _esasSecuritySettings;
        private Uri _odataWs;
        private readonly ILogger _logger;

        public EsasWsContextFactory(EsasSecuritySettings esasSecuritySettings, Uri webServiceUri, ILogger logger)
        {
            _esasSecuritySettings = esasSecuritySettings;
            _odataWs = webServiceUri;
            this._logger = logger;

        }

        public Container Create()
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true; // Hvis ESAS anvendes med et self-signed certifikat, kan vi ikke validere på det - certifikatets domæne vil ikke svare til localhost.


            var esasWebService = new Container(_odataWs);

            var credentials = new NetworkCredential(
                 _esasSecuritySettings.Username,
                 _esasSecuritySettings.Password,
                 _esasSecuritySettings.Domain);

            esasWebService.Credentials = credentials;
            esasWebService.SendingRequest2 += addCertificateToRequest;
            //esasWebService.ReceivingResponse += EsasWebService_ReceivingResponse;
            _logger.LogInformation("Created ESAS web-service context.");
            return esasWebService;
        }


        private void addCertificateToRequest(object sender, Microsoft.OData.Client.SendingRequest2EventArgs e)
        {
            // Importér certifikat fra cert-store, hvis det er installeret hér.
            //X509Store myX509Store = new X509Store(StoreName.Root, StoreLocation.CurrentUser);
            //myX509Store.Open(OpenFlags.ReadWrite);
            //X509Certificate2 myCertificate = myX509Store.Certificates.OfType<X509Certificate2>().FirstOrDefault(cert => cert.Thumbprint == "9B72559C78B3A849DBB6536806AE5FCB4151AF83");

            // Importer fra PFX/P12:
            X509Certificate2 certificate = new X509Certificate2();
            certificate.Import(_esasSecuritySettings.CertificatePath, _esasSecuritySettings.CertificatePassword, X509KeyStorageFlags.DefaultKeySet);
            if (certificate != null)
            {
                if (e.IsBatchPart)
                    return; // no-go for batch operations

                ((HttpWebRequestMessage)e.RequestMessage).HttpWebRequest.ClientCertificates.Add(certificate);

                DateTime expirationDate = DateTime.Parse(certificate.GetExpirationDateString());
                if (DateTime.Today.AddDays(14) >= expirationDate.Date)
                {
                    string certExpirationErrorMsg = @"Et ESAS sikkerhedscertifikat er tæt på at udløbe - om mindre end 14 dage vil denne applikation stoppe med at fungere, hvis ikke dette certifikat bliver fornyet. Ref. https://confluence.umit.dk/display/esasdokumentation/Systemtilslutning";
                    throw new Exception(certExpirationErrorMsg);

                }
            }
            else
            {
                throw new Exception("Sikkerhedscertifikat kunne ikke loades :-( - data vil ikke kunne hentes fra OData.");
            }
        }

    }
}
