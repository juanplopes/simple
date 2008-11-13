using System;
using System.Collections.Generic;

using System.Text;
using System.ServiceModel.Description;
using SimpleLibrary.DataAccess;
using BasicLibrary.Configuration;
using SimpleLibrary.Rules;
using SimpleLibrary.Config;
using BasicLibrary.Common;
using System.Reflection;

namespace SimpleLibrary.ServiceModel
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
                    SimpleLibraryConfig simpleLibraryConfig = SimpleLibraryConfig.Get();

                    List<Type> list = new List<Type>();
                    list.AddRange(DecoratedTypeFinder.Locate(
                        Assembly.GetAssembly(typeof(GenericFault<>)),
                        typeof(SimpleFaultContractAttribute),
                        true
                    ));
                    list.AddRange(DecoratedTypeFinder.Locate(
                        simpleLibraryConfig.Business.InterfaceAssembly.LoadAssembly(),
                        typeof(SimpleFaultContractAttribute),
                        true
                    ));
                    faults = list;
                }
                return faults;
            }
        }

        #region IEndpointConfigurator Members



        public void Configure(bool isClientSide, System.ServiceModel.Description.ServiceEndpoint endpoint, SimpleLibrary.Config.ConfiguratorElement config)
        {
            foreach (OperationDescription operation in endpoint.Contract.Operations)
            {
                foreach (Type type in Faults)
                {
                    FaultDescription fault = new FaultDescription(type.Name);
                    fault.DetailType = type;
                    operation.Faults.Add(fault);
                }
            }
        }

        #endregion
    }
}
