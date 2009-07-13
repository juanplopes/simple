using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simple.Tests.Contracts;
using Simple.DataAccess;
using Simple.Filters;
using NHibernate.Linq;
using Simple.ConfigSource;
using Simple.Remoting;
using Simple.Tests.Service;
using Simple.DataAccess.Context;
using Simple.Server;
using NHibernate;

namespace Simple.Tests.DataAccess
{
    [TestClass]
    public class SimpleEntityFixture
    {


        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            DBEnsurer.Configure(typeof(DBEnsurer));
        }


        IDataContext dtx = null;
        [TestInitialize]
        public void TestSetup()
        {
            dtx = Simply.Get(typeof(DBEnsurer)).EnterContext();
            DBEnsurer.Ensure(typeof(DBEnsurer));
        }

        [TestCleanup]
        public void TestCleanup()
        {
            dtx.Exit();
        }

        [TestMethod]
        public void InsertEmpresa()
        {
            Empresa e = new Empresa()
            {
                Nome = "Whatever"
            };
            e = e.Save();
            Assert.AreNotEqual(0, e.Id);

            Empresa e2 = Empresa.LoadByFilter(Empresa.NomeProperty.Eq("Whatever"));
            Assert.IsNotNull(e2);
            Assert.AreEqual(e.Nome, e2.Nome);
            Assert.AreEqual(e.Id, e2.Id);

            e.Delete();
        }

        [TestMethod]
        public void UpdateEmpresa()
        {
            Empresa e = Empresa.LoadByFilter(BooleanExpression.True);
            Assert.AreEqual(e.Id, DBEnsurer.E1.Id);
            e.Nome = "E2";
            e = e.SaveOrUpdate();

            Assert.AreEqual(e.Nome, Empresa.Load(e.Id).Nome);
        }

        [TestMethod]
        public void FirstLinqTest()
        {
            var queryable = SessionManager.OpenSession(typeof(DBEnsurer)).Linq<Empresa>();
            var query = from e in queryable
                        where e.Nome == "E1"
                        select e.Nome;

            Assert.AreEqual(1, query.Count());

            Assert.AreEqual("E1", query.First());
        }

        [TestMethod]
        public void SecondLinqTest()
        {
            Empresa e = Empresa.Service.GetByNameLinq("E1");

            Assert.IsNotNull(e);
            Assert.AreEqual("E1", e.Nome);
        }


    }
}
