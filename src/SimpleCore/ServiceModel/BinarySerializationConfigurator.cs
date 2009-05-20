using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.ServiceModel
{
    public class BinarySerializationConfigurator : IEndpointConfigurator
    {
        #region IEndpointConfigurator Members

        public void Configure(bool isClientSide, System.ServiceModel.Description.ServiceEndpoint endpoint, Simple.Config.ConfiguratorElement config)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
