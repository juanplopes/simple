using System;
using System.Collections.Generic;

using System.Text;
using Simple.ServiceModel;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Description;
using Simple.Config;
using System.ServiceModel.Channels;
using Simple.Rules;
using Simple.Logging;
using System.IO;
using log4net;

namespace Simple.ServiceModel
{
    public class AssemblyLocatorHoster : WCFHostingHelper
    {
        //protected static ILog Logger = MainLogger.Get(MethodInfo.GetCurrentMethod().DeclaringType);

        protected IList<Type> ServiceTypes { get; set; }
        protected SimpleConfig Config { get; set; }
        protected Binding DefaultBinding { get; set; }

        public AssemblyLocatorHoster()
        {
            ServiceTypes = new List<Type>();
            Config = SimpleConfig.Get();
            DefaultBinding = ConfigLoader.CreateDefaultBinding();
        }

        protected Type GetMainContractType(Type serviceType)
        {
            Type returnType = null;

            foreach (Type t in serviceType.GetInterfaces())
            {
                if (Attribute.IsDefined(t, typeof(ServiceContractAttribute)) && Attribute.IsDefined(t, typeof(MainContractAttribute)))
                {
                    if (returnType == null)
                        returnType = t;
                    else
                        throw new InvalidOperationException("A service should contain only one contract implementation.");
                }
            }

            if (returnType == null) throw new InvalidOperationException("The specified service contains no contract implmentation.");

            return returnType;
        }

        protected override ServiceHost GetServiceHost(Type type)
        {
            Type contractType = GetMainContractType(type);
            object obj = RulesProxyBuilder.Instance.WrapInstance(Activator.CreateInstance(type), contractType);

            Uri uri = new Uri(new Uri(Config.ServiceModel.DefaultBaseAddress), contractType.Name);
            ServiceHost host = new ServiceHost(obj, uri);

            if (host.Description.Endpoints.Count == 0)
            {
                ConfigLoader.ApplyConfigurators(host, Config.ServiceModel, false);

                ServiceEndpoint endpoint = AddEndpoint(host, Config.ServiceModel.DefaultEndpoint, contractType);
                ConfigLoader.ApplyConfigurators(endpoint, Config.ServiceModel.DefaultEndpoint, false);

                if (Config.ServiceModel.Endpoints != null)
                    foreach (EndpointElement addEndpointCfg in Config.ServiceModel.Endpoints)
                    {
                        ServiceEndpoint addEndpoint = AddEndpoint(host, addEndpointCfg, contractType);
                        ConfigLoader.ApplyConfigurators(addEndpoint, addEndpointCfg, false);
                    }

                //CustomDataContractSerializerOperationBehavior.OverrideOperations(endpoint.Contract.Operations);
            }
            return host;
        }

        protected ServiceEndpoint AddEndpoint(ServiceHost host, EndpointElement element, Type contractType)
        {
            return host.AddServiceEndpoint(contractType, DefaultBinding, element.Address);
        }

        public void LocateServices(Assembly assembly)
        {
            Logger.Debug("Locating service classes...");
            foreach (Type t in assembly.GetTypes())
            {
                if (Attribute.IsDefined(t, typeof(ServiceEnableAttribute)))
                {
                    Logger.Debug("Found " + t.FullName + " by ServiceEnable attribute");
                    ServiceTypes.Add(t);
                }
                else
                {
                    foreach (Type ti in t.GetInterfaces())
                    {
                        if (Attribute.IsDefined(ti, typeof(MainContractAttribute)))
                        {
                            Logger.Debug("Found " + t.FullName + " by implement an interface with MainContract attribute");
                            ServiceTypes.Add(t);
                            break;
                        }
                    }
                }
            }
        }

        public void StartHosting()
        {
            foreach (Type t in ServiceTypes)
            {
                Register(t);
            }
            Execute();
        }
    }
}
