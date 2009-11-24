using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Tool.hbm2ddl;
using Simple.DataAccess;
using NUnit.Framework;
using Simple.ConfigSource;
using Simple.Tests.DataAccess;
using Simple.Services.Remoting;
using Simple.Tests.Service;
using Simple.Services.Default;
using Simple.Services;
using Simple.Patterns;
using Simple.Tests.SampleServer;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using System.Reflection;
using System.IO;

namespace Simple.Tests
{
    public class NHConfig1 : Singleton<NHConfig1>
    {
        public const string ConfigKey = "NHConfig1_test";

        Simply simply = null;

        public Simply Simply
        {
            get { return simply; }
        }

        public void Init() { }

        public NHConfig1()
        {
            object key = ConfigKey;
            simply = Simply.Do[key];

            string temp = Path.GetTempFileName();

            File.WriteAllBytes(temp, Database.Northwind);

            simply.Configure
                .NHibernteFluently(x=>
                    x.Database(SQLiteConfiguration.Standard.UsingFile(temp)))
               .MappingFromAssemblyOf<Category.Map>()
               .Validator(typeof(Category.Map).Assembly)
               .DefaultHost();

            simply.AddServerHook(x => new TransactionCallHook(x, key));
            simply.AddServerHook(x => new DefaultCallHook(x, key));
            simply.HostAssemblyOf(typeof(NHConfig1));
            

        }
    }

}
