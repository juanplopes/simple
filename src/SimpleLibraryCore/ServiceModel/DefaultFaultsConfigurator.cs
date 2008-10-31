using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Description;
using SimpleLibrary.DataAccess;
using BasicLibrary.Configuration;
using SimpleLibrary.Rules;
using SimpleLibrary.Config;

namespace SimpleLibrary.ServiceModel
{
    public class DefaultFaultsConfigurator : IEndpointConfigurator
    {
        #region IEndpointConfigurator Members

        public void Configure(bool isClientSide, System.ServiceModel.Description.ServiceEndpoint endpoint, SimpleLibrary.Config.ConfiguratorElement config)
        {
            foreach (OperationDescription operation in endpoint.Contract.Operations)
            {
                FaultDescription fault = new FaultDescription(typeof(PersistenceFault).Name);
                fault.DetailType = typeof(PersistenceFault);
                operation.Faults.Add(fault);

                fault = new FaultDescription(typeof(GenericFault).Name);
                fault.DetailType = typeof(GenericFault);
                operation.Faults.Add(fault);
            }
        }

        #endregion
    }
}
