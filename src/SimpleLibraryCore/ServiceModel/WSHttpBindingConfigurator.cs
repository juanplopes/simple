using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel.Description;
using SimpleLibrary.Config;
using BasicLibrary.Configuration;
using System.ServiceModel;

namespace SimpleLibrary.ServiceModel
{
    public class WSHttpBindingConfigurator : IEndpointConfigurator
    {
        #region IEndpointConfigurator Members

        public void Configure(bool isClientSide, ServiceEndpoint endpoint, ConfiguratorElement config)
        {
            WSHttpBindingElement element = new WSHttpBindingElement();
            (element as IConfigElement).LoadFromElement(config);

            WSHttpBinding binding = new WSHttpBinding();
            binding.MaxReceivedMessageSize = element.MaxReceivedMessageSize;
            binding.ReceiveTimeout = new TimeSpan(0, 0, element.ReceiveTimeout);

            endpoint.Binding = binding;
        }

        #endregion
    }

    public class WSHttpBindingElement : ConfigElement
    {
        [ConfigElement("maxReceivedMessageSize", Default = 1 << 24)]
        public long MaxReceivedMessageSize { get; set; }

        [ConfigElement("receiveTimeout", Default = 1 << 24)]
        public int ReceiveTimeout { get; set; }

    }
}
