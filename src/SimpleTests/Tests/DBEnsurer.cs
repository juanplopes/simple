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
using Simple.Server;
using Simple.Services.Default;

namespace Simple.Tests
{
    public class DBEnsurer
    {
        public static void Ensure(object key)
        {
            SchemaExport exp = new SchemaExport(SessionManager.GetConfig(key));
            exp.Drop(true, true);
            exp.Create(true, true);
            InsertTestData();
        }

        public static void Configure(object key)
        {
            NHibernateSimply.Do.Configure(key,
               XmlConfig.LoadXml<NHibernateConfig>(NHConfigurations.NHConfig1));
            NHibernateSimply.Do.MapAssemblyOf<Empresa.Map>(key);

            DefaultHostSimply.Do.Configure(key, new DirectConfigSource<DefaultHostConfig>());
            Simply.Get(key).HostAssemblyOf(typeof(DBEnsurer));
        }

        public static Empresa E1 = null;
        public static Funcionario F1 = null;
        public static EmpresaFuncionario EF1 = null;

        public static void InsertTestData()
        {
            E1 = new Empresa()
            {
                Nome = "E1"
            };
            E1 = E1.Save();
            

            F1 = new Funcionario()
            {
                Nome = "F1"
            };
            F1 = F1.Save();

            EF1 = new EmpresaFuncionario()
            {
                Empresa = E1,
                Funcionario = F1
            };
            EF1 = EF1.Save();
        }

        public static void AssertStillOk()
        {
            Assert.AreEqual(1, Empresa.CountByFilter(BooleanExpression.True));
            Assert.AreEqual(1, Funcionario.CountByFilter(BooleanExpression.True));
            Assert.AreEqual(1, EmpresaFuncionario.CountByFilter(BooleanExpression.True));
        }



    }
}
