using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.ConfigSource;

namespace Simple.Services.Default
{
    public class DefaultClientProvider : Factory<DefaultHostConfig>, IServiceClientProvider
    {
        #region IServiceClientProvider Members

        public object Create(Type type)
        {
            try
            {
                return ServiceLocationFactory.Get(ConfigCache).Get(type);
            }
            catch (KeyNotFoundException e)
            {
                throw new ServiceConnectionException(e.Message, e);
            }
        }

        #endregion

        protected override void OnConfig(DefaultHostConfig config)
        {
        }

        protected override void OnClearConfig()
        {
        }
    }
}
