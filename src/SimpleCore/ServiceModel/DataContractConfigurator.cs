using System;
using System.Collections.Generic;

using System.Text;
using System.ServiceModel.Description;
using Simple.Config;
using Simple.Configuration;
using Simple.ServiceModel;
using System.Reflection;

namespace Simple.ServiceModel
{
    public class DataContractConfigurator : IEndpointConfigurator
    {
        public void Configure(bool isClientSide, System.ServiceModel.Description.ServiceEndpoint endpoint, ConfiguratorElement config)
        {
            SimpleConfig simpleLibraryConfig = SimpleConfig.Get();

            IList<Assembly> assemblies = new List<Assembly>();
            assemblies.Add(Assembly.GetAssembly(typeof(Filters.Filter)));
            assemblies.Add(simpleLibraryConfig.Business.ContractsAssembly.LoadAssembly());
            CustomDataContractSerializerOperationBehavior.OverrideOperations(endpoint.Contract.Operations, assemblies);
        }
    }
}
