using System.IO;
using FluentNHibernate.Cfg.Db;
using Simple.Patterns;
using Simple.Services;
using Simple.Tests.Resources;
using NHibernate.ByteCode.LinFu;

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
                    x.Database(SQLiteConfiguration.Standard.UsingFile(temp)
                     .ProxyFactoryFactory<ProxyFactoryFactory>()))
               .MappingFromAssemblyOf<Category.Map>()
               .Validator(typeof(Category.Map).Assembly)
               .DefaultHost();

            simply.AddServerHook(x => new TransactionCallHook(x, key));
            simply.AddServerHook(x => new DefaultCallHook(x, key));
            simply.HostAssemblyOf(typeof(NHConfig1));
            

        }
    }

}
