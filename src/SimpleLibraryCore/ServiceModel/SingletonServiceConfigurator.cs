using System;
using System.Collections.Generic;

using System.Text;
using System.ServiceModel;

namespace Simple.ServiceModel
{
    public class SingletonServiceConfigurator : IServiceConfigurator
    {
        public void Configure(System.ServiceModel.ServiceHost service, Simple.Config.ConfiguratorElement config)
        {
            ServiceBehaviorAttribute behavior = service.Description.Behaviors.Find<ServiceBehaviorAttribute>();
            if (behavior != null)
                behavior.InstanceContextMode = InstanceContextMode.Single;
        }
    }
}
