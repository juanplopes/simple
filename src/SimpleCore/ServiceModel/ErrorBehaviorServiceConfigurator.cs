using System;
using System.Collections.Generic;

using System.Text;
using Simple.ServiceModel;
using System.ServiceModel;

namespace Simple.ServiceModel
{
    public class ErrorBehaviorServiceConfigurator : IServiceConfigurator
    {
        public void Configure(System.ServiceModel.ServiceHost service, Simple.Config.ConfiguratorElement config)
        {
            service.Description.Behaviors.Remove<ErrorServiceBehavior>();
            service.Description.Behaviors.Add(new ErrorServiceBehavior());
        }
    }
}
