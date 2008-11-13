using System;
using System.Collections.Generic;

using System.Text;
using System.ServiceModel;
using SimpleLibrary.Config;

namespace SimpleLibrary.ServiceModel
{
    public interface IServiceConfigurator
    {
        void Configure(ServiceHost service, ConfiguratorElement config);
    }
}
