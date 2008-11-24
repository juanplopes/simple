using System;
using System.Collections.Generic;

using System.Text;
using System.ServiceModel.Description;
using BasicLibrary.Configuration;

namespace SimpleLibrary.ServiceModel
{
    public class DebugServiceConfigurator : IServiceConfigurator
    {
        #region IServiceConfigurator Members

        public void Configure(System.ServiceModel.ServiceHost service, SimpleLibrary.Config.ConfiguratorElement config)
        {
            DebugServiceConfiguratorElement configT = new DebugServiceConfiguratorElement();
            (configT as IConfigElement).LoadFromElement(config);

            ServiceDebugBehavior behavior = new ServiceDebugBehavior();
            behavior.IncludeExceptionDetailInFaults = configT.Enabled;;

            service.Description.Behaviors.Remove<ServiceDebugBehavior>();
            service.Description.Behaviors.Add(behavior);
        }

        #endregion
    }

    public class DebugServiceConfiguratorElement : ConfigElement
    {
        [ConfigElement("enabled")]
        public bool Enabled { get; set; }
    }
}
