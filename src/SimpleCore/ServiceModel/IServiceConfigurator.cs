using System;
using System.Collections.Generic;

using System.Text;
using System.ServiceModel;
using Simple.Config;

namespace Simple.ServiceModel
{
    public interface IServiceConfigurator
    {
        void Configure(ServiceHost service, ConfiguratorElement config);
    }
}
