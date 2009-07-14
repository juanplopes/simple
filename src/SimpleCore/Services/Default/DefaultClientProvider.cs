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
            return Activator.CreateInstance(
                ServiceLocationFactory.Get(ConfigCache).Get(type));
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
