using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Threading;
using System.Collections;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using Simple.Patterns;

namespace Simple
{
    [Serializable]
    public class SimpleContext : ILogicalThreadAffinative
    {
        static SimpleContext()
        {
            SwitchProvider(null);
        }

        public static IContextProvider Provider { get; protected set; }
        public static ContextData Data { get { return new ContextData(() => Provider); } }

        public static void SwitchProvider(IContextProvider newProvider)
        {
            newProvider = newProvider ?? new ThreadDataProvider();

            if (Provider != null)
                newProvider.SetStorage(Provider.GetStorage());

            Provider = newProvider;
        }

        public static bool Exists()
        {
            return Data.ContainsSingleton<SimpleContext>();
        }

        public static SimpleContext Get()
        {
            return Data.Singleton<SimpleContext>();
        }

        public static void Force(SimpleContext context)
        {
            Data.SetSingleton(context);
        }

        public SimpleContext()
        {
            ExtendedInfo = new Hashtable();
        }

        public IDisposable WithUsername(string username)
        {
            var oldUser = Username;
            Username = username;
            return new DisposableAction(() => Username = oldUser);
        }

        public string Username { get; set; }
        public IDictionary ExtendedInfo { get; protected set; }
    }
}
