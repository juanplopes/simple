using System;
using System.Collections.Generic;
using System.Linq;
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
            Type bindingType = Type.GetType(element.BindingType);
            Binding binding = (Binding)Activator.CreateInstance(bindingType, element.BindingNameRef);
            return binding;
        }

        public static void ApplyConfigurators(ServiceHost endpoint, ServiceModelElement element, bool isServer)
        {
            foreach (ConfiguratorElement opClassType in element.ServiceConfigurators)
            {
                if (opClassType.RunOnlyAtServer && !isServer) continue;
                
                Type t = Type.GetType(opClassType.Type);
                if (!typeof(IServiceConfigurator).IsAssignableFrom(t)) throw new InvalidOperationException("Configurator must implement IServiceConfigurator: " + t.FullName);

                IServiceConfigurator configurator = (IServiceConfigurator)Activator.CreateInstance(t);
                configurator.Configure(endpoint, opClassType);
            }
        }

        public static void ApplyConfigurators(ServiceEndpoint endpoint, EndpointElement element, bool isServer)
        {
            foreach (ConfiguratorElement opClassType in element.EndpointConfigurators)
            {
                if (opClassType.RunOnlyAtServer && !isServer) continue;

                Type t = Type.GetType(opClassType.Type);
                if (!typeof(IEndpointConfigurator).IsAssignableFrom(t)) throw new InvalidOperationException("Configurator must implement IEndpointConfigurator: " + t.FullName);

                IEndpointConfigurator configurator =  (IEndpointConfigurator)Activator.CreateInstance(t);
                configurator.Configure(endpoint, opClassType);
            }
        }
    }
}
