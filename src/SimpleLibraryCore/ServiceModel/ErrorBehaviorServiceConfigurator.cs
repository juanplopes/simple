using System;
using System.Collections.Generic;

using System.Text;
using BasicLibrary.ServiceModel;
using System.ServiceModel;

namespace SimpleLibrary.ServiceModel
{
    public class ErrorBehaviorServiceConfigurator : IServiceConfigurator
    {
        public void Configure(System.ServiceModel.ServiceHost service, SimpleLibrary.Config.ConfiguratorElement config)
        {
            service.Description.Behaviors.Remove<ErrorServiceBehavior>();
            service.Description.Behaviors.Add(new ErrorServiceBehavior());
        }
    }
}
