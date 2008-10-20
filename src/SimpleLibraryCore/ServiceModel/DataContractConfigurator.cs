using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Description;
using SimpleLibrary.Config;
using BasicLibrary.Configuration;
using BasicLibrary.ServiceModel;
using System.Reflection;

namespace SimpleLibrary.ServiceModel
{
    public class DataContractConfigurator : IEndpointConfigurator
    {
        public void Configure(System.ServiceModel.Description.ServiceEndpoint endpoint, ConfiguratorElement config)
        {
            SimpleLibraryConfig simpleLibraryConfig = SimpleLibraryConfig.Get();

            IList<Assembly> assemblies = new List<Assembly>();
            assemblies.Add(Assembly.GetAssembly(typeof(Filters.Filter)));
            assemblies.Add(Assembly.Load(simpleLibraryConfig.Business.InterfaceAssembly));

            CustomDataContractSerializerOperationBehavior.OverrideOperations(endpoint.Contract.Operations, assemblies);
        }
    }
}
