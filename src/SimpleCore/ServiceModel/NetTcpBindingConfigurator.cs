using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Description;
using Simple.Config;
using System.ServiceModel;

namespace Simple.ServiceModel
{
    public class NetTcpBindingConfigurator : IEndpointConfigurator
    {
        #region IEndpointConfigurator Members

        public void Configure(bool isClientSide, ServiceEndpoint endpoint, ConfiguratorElement config)
        {
            NetTcpBinding binding = new NetTcpBinding(SecurityMode.None);
            endpoint.Binding = binding;
        }

        #endregion
    }
}
