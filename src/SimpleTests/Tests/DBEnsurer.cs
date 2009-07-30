using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Tool.hbm2ddl;
using Simple.DataAccess;
using Simple.Tests.Contracts;
using NUnit.Framework;
using Simple.Filters;
using Simple.ConfigSource;
using Simple.Tests.DataAccess;
using Simple.Services.Remoting;
using Simple.Tests.Service;
using Simple.Services.Default;
using Simple.Services;

namespace Simple.Tests
{
    public class DBEnsurer
    {
        private static bool _alreadyExecuted = false;
        private static object _lock = new object();

        public static void Ensure(object key)
        {
            lock (_lock)
                if (!_alreadyExecuted)
                {
                    DBEnsurer.Configure(typeof(DBEnsurer));
                    SchemaExport exp = new SchemaExport(Simply.Get(key).GetNHibernateConfig());
                    exp.Drop(true, true);
                    exp.Create(true, true);
                    _alreadyExecuted = true;
                }
        }

        public static void Configure(object key)
        {
            NHibernateSimply.Do.Configure(key,
               XmlConfig.LoadXml<NHibernateConfig>(NHConfigurations.NHConfig1));
            NHibernateSimply.Do.MapAssemblyOf<Empresa.Map>(key);

            DefaultHostSimply.Do.Configure(key);
            Simply.Get(key).HostAssemblyOf(typeof(DBEnsurer));
            Simply.Get(key).AddServerHook(x => new DefaultCallHook(x, key));
        }
    }
}
