using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.ConfigSource;

namespace Simple.Services.Default
{
    public class DefaultHostProvider : Factory<DefaultHostConfig>, IServiceHostProvider
    {
        #region IServiceHostProvider Members

        public void Host(object server, Type contract)
        {
            ServiceLocationFactory.Get(ConfigCache).Set(server, contract);
        }

        public void Start()
        {
        }

        public void Stop()
        {
        }

        #endregion

        protected override void OnConfig(DefaultHostConfig config)
        {
        }

        protected override void OnClearConfig()
        {
            ServiceLocationFactory.Get(ConfigCache).Clear();
        }
    }
}
