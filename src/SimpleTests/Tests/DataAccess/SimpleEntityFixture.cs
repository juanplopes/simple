using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simple.Tests.Contracts;
using Simple.TestBase;
using Simple.DataAccess;
using Simple.Filters;
using NHibernate.Linq;

namespace Simple.Tests.DataAccess
{
    [TestClass]
    public class SimpleEntityFixture
    {
        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            DBEnsurer.Ensure();
        }

        [TestMethod]
        public void InsertEmpresa()
        {
            Empresa e = new Empresa()
            {
                Nome = "Whatever"
            };
            e.Save();

            Empresa.Delete(e);
        }

        [TestMethod]
        public void UpdateEmpresa()
        {
            Empresa e = Empresa.LoadByFilter(BooleanExpression.True);
            Assert.AreEqual(e.Id, DBEnsurer.E1.Id);
            e.Nome = "E2";
            e.SaveOrUpdate();

            Assert.AreEqual(e.Nome, Empresa.Load(e.Id).Nome);
        }

        [TestMethod]
        public void FirstLinqTest()
        {
            var queryable = SessionManagerOld.GetSession().Linq<Empresa>();
            var query = from e in queryable
                        where e.Nome == "E1"
                        select e.Nome;

            Assert.AreEqual(1, query.Count());

            Assert.AreEqual("E1", query.First());
        }

        [TestMethod]
        public void SecondLinqTest()
        {
            Empresa e = Empresa.Rules.GetByNameLinq("E1");

            Assert.IsNotNull(e);
            Assert.AreEqual("E1", e.Nome);
        }


    }
}
