using System;
using System.Collections.Generic;

using System.Text;
using System.ServiceModel.Channels;
using SimpleLibrary.Config;
using System.ServiceModel.Description;
using System.ServiceModel;

namespace SimpleLibrary.ServiceModel
{
    public class ConfigLoader
    {
        public static Binding CreateBinding(EndpointElement element)
        {
            Type bindingType = element.BindingType.LoadType();
            Binding binding = (Binding)Activator.CreateInstance(bindingType, element.BindingNameRef);
            return binding;
        }

        public static void ApplyConfigurators(ServiceHost endpoint, ServiceModelElement element, bool isClient)
        {
            foreach (ConfiguratorElement opClassType in element.ServiceConfigurators)
            {
                if ((opClassType.RunAt == SideType.Server && isClient) ||
                    (opClassType.RunAt == SideType.Client && !isClient))
                    continue;

                Type t = opClassType.LoadType();
                if (!typeof(IServiceConfigurator).IsAssignableFrom(t)) throw new InvalidOperationException("Configurator must implement IServiceConfigurator: " + t.FullName);

                IServiceConfigurator configurator = (IServiceConfigurator)Activator.CreateInstance(t);
                configurator.Configure(endpoint, opClassType);
            }
        }

        public static void ApplyConfigurators(ServiceEndpoint endpoint, EndpointElement element, bool isClient)
        {
            foreach (ConfiguratorElement opClassType in element.EndpointConfigurators)
            {
                if ((opClassType.RunAt == SideType.Server && isClient) ||
                    (opClassType.RunAt == SideType.Client && !isClient))
                    continue;


                Type t = opClassType.LoadType();
                if (!typeof(IEndpointConfigurator).IsAssignableFrom(t)) throw new InvalidOperationException("Configurator must implement IEndpointConfigurator: " + t.FullName);

                IEndpointConfigurator configurator = (IEndpointConfigurator)Activator.CreateInstance(t);
                configurator.Configure(isClient, endpoint, opClassType);
            }
        }
    }
}
