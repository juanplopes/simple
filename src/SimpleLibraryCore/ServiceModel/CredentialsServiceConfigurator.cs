using System;
using System.Collections.Generic;

using System.Text;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.ServiceModel.Description;
using System.Security.Cryptography.X509Certificates;

namespace SimpleLibrary.ServiceModel
{
    public class CredentialsServiceConfigurator : IEndpointConfigurator
    {
        #region IEndpointConfigurator Members

        public void Configure(bool isClientSide, ServiceEndpoint endpoint, SimpleLibrary.Config.ConfiguratorElement config)
        {
            if (!isClientSide || true)
            {
                EndpointAddressBuilder builder = new EndpointAddressBuilder(endpoint.Address);
                X509Certificate2 cert = new X509Certificate2("Test.cer");
                builder.Identity = new X509CertificateEndpointIdentity(cert);
                endpoint.Address = builder.ToEndpointAddress();
            }
        }

        #endregion
    }
}
