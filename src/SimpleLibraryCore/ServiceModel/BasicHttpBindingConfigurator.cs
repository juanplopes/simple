using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;

namespace Simple.ServiceModel
{
    public class BasicHttpBindingConfigurator : IEndpointConfigurator
    {
        #region IEndpointConfigurator Members

        public void Configure(bool isClientSide, System.ServiceModel.Description.ServiceEndpoint endpoint, Simple.Config.ConfiguratorElement config)
        {
            endpoint.Binding = new BasicHttpBinding(BasicHttpSecurityMode.None);
        }

        #endregion
    }
}
