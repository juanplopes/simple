using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple;
using Simple.Services;

namespace Conspirarte.Environment.Development
{
    public class Default
    {
        public static Simply ConfigureClient()
        {
            Simply.Do.Configure
                .Log4net().FromFile(@"Environment\Development\Log4net.config")
                .Remoting().FromFile(@"Environment\Development\Remoting.config");
            return Simply.Do;
        }

        public static Simply ConfigureServer()
        {
            ConfigureClient();

            Simply.Do.Configure
               .NHibernate().FromFile(@"Environment\Development\NHibernate.config");

            Simply.Do.AddServerHook(x => new DefaultCallHook(x, null));
            return Simply.Do;

        }
    }

}
