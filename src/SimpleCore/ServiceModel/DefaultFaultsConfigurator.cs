using System;
using System.Collections.Generic;

using System.Text;
using System.ServiceModel.Description;
using Simple.DataAccess;
using Simple.Configuration2;
using Simple.Services;
using Simple.Config;
using Simple.Common;
using System.Reflection;
using Simple.Reflecion;

namespace Simple.ServiceModel
{
    public class DefaultFaultsConfigurator : IEndpointConfigurator
    {
        protected static IList<Type> faults = null;

        protected IList<Type> Faults
        {
            get
            {
                if (faults == null)
                {
                    SimpleConfig simpleLibraryConfig = SimpleConfig.Get();

                    List<Type> list = new List<Type>();
                    list.AddRange(DecoratedTypeFinder.Locate(
                        Assembly.GetAssembly(typeof(GenericFault<>)),
                        typeof(SimpleFaultContractAttribute),
                        true
                    ));
                    list.AddRange(DecoratedTypeFinder.Locate(
                        simpleLibraryConfig.Business.ContractsAssembly.LoadAssembly(),
                        typeof(SimpleFaultContractAttribute),
                        true
                    ));
                    faults = list;
                }
                return faults;
            }
        }

        #region IEndpointConfigurator Members



        public void Configure(bool isClientSide, System.ServiceModel.Description.ServiceEndpoint endpoint, Simple.Config.ConfiguratorElement config)
        {
            foreach (OperationDescription operation in endpoint.Contract.Operations)
            {
                foreach (Type type in Faults)
                {
                    FaultDescription fault = new FaultDescription(type.Name);
                    fault.DetailType = type;
                    fault.Name = type.Name;
                    fault.Namespace = type.Namespace;
                    operation.Faults.Add(fault);
                }
            }
        }

        #endregion
    }
}
