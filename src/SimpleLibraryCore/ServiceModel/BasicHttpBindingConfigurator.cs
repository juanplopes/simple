using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;

namespace SimpleLibrary.ServiceModel
{
    public class BasicHttpBindingConfigurator : IEndpointConfigurator
    {
        #region IEndpointConfigurator Members

        public void Configure(bool isClientSide, System.ServiceModel.Description.ServiceEndpoint endpoint, SimpleLibrary.Config.ConfiguratorElement config)
        {
            endpoint.Binding = new BasicHttpBinding(BasicHttpSecurityMode.None);
        }

        #endregion
    }
}
