using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple;
using Simple.Services;
using System.Reflection;
using Simple.ConfigSource;

namespace Sample.Project.Environment.Development
{
    public class Default
    {
        public static Simply ConfigureClient()
        {
            Simply.Do.Configure
                .Log4net().FromFile(@"Environment\Development\Log4net.config")
                .Remoting().FromFile(@"Environment\Development\Remoting.config");

            Simply.Do.AddClientHook(x => new HttpIdentityInjector(x));

            return Simply.Do;
        }

        public static Simply ConfigureServer()
        {
            ConfigureClient();

            Simply.Do.Configure
               .NHibernate().FromFile(@"Environment\Development\NHibernate.config")
               .Validator(Assembly.GetExecutingAssembly());
               

            Simply.Do.AddServerHook(x => new TransactionCallHook(x));
            Simply.Do.AddServerHook(x => new DefaultCallHook(x, null));

            return Simply.Do;

        }
    }

   

}
