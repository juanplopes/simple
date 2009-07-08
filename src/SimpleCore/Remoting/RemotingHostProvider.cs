using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.ConfigSource;
using Simple.Services;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting;
using log4net;
using Simple.Client;
using System.Reflection;
using Simple.Patterns;

namespace Simple.Remoting
{
    public class RemotingHostProvider : Factory<RemotingConfig>, IServiceHostProvider, IDisposable
    {
        ILog logger = Simply.Do.Log(MethodInfo.GetCurrentMethod());
        AppDomain domain = null;
        RemotingServerInstance server = null;
        protected override void OnConfig(RemotingConfig config)
        {
            TryUnloadDomain();
            domain = AppDomain.CreateDomain("Remoting_" + Guid.NewGuid().ToString(), AppDomain.CurrentDomain.Evidence,
                AppDomain.CurrentDomain.SetupInformation);

            Type serverType = typeof(RemotingServerInstance);
            server = (RemotingServerInstance)domain.CreateInstanceAndUnwrap
                (serverType.Assembly.FullName, serverType.FullName);
            server.Config = config;
        }

        protected override void OnClearConfig()
        {
            TryUnloadDomain();
        }

        protected void TryUnloadDomain()
        {
            if (domain != null)
            {
                if (server != null) server.TryStop();
                AppDomain.Unload(domain);
                domain = null;
                server = null;
            }
        }

        public void Host(Type type, Type contract)
        {
            server.AddType(type, contract);
        }

        public void Stop()
        {
            TryUnloadDomain();
        }

        public void Start()
        {
        }


    }
}
