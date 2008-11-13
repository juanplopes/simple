using System;
using System.Collections.Generic;

using System.Text;
using System.ServiceModel.Description;
using SimpleLibrary.Config;

namespace SimpleLibrary.ServiceModel
{
    public interface IEndpointConfigurator
    {
        void Configure(bool isClientSide, ServiceEndpoint endpoint, ConfiguratorElement config);
    }
}
