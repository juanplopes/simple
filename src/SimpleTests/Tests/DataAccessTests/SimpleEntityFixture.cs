using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.Tests.Contracts;
using Simple.NUnit;
using Simple.DataAccess;
using Simple.Filters;
using NHibernate.Linq;

namespace Simple.Tests.DataAccessTests
{
    [TestFixture]
    public class SimpleEntityFixture
    {
        [TestFixtureSetUp]
        public void Setup()
        {
            DBEnsurer.Ensure();
        }

        [Test]
        public void InsertEmpresa()
        {
            using (SessionManager.BeginTransaction())
            {
                Empresa e = new Empresa()
                {
                    Nome = "Whatever"
                };
                e.Save();
            }
        }

        [Test]
        public void UpdateEmpresa()
        {
            using (SessionManager.BeginTransaction())
            {
                Empresa e = Empresa.LoadByFilter(BooleanExpression.True);
                Assert.AreEqual(e.Id, DBEnsurer.E1.Id);
                e.Nome = "E2";
                e.SaveOrUpdate();

                Assert.AreEqual(e.Nome, Empresa.Load(e.Id).Nome);
            }

        }

        [Test]
        public void FirstLinqTest()
        {
            var queryable = SessionManager.GetSession().Linq<Empresa>();
            var query = from e in queryable
                        where e.Nome == "E1"
                        select e.Nome;

            Assert.AreEqual(1, query.Count());

            Assert.AreEqual("E1", query.First());
        }

        [Test]
        public void SecondLinqTest()
        {
            Empresa e = Empresa.Rules.GetByNameLinq("E1");
            
            Assert.IsNotNull(e);
            Assert.AreEqual("E1", e.Nome);
        }

        [Test]
        public void ExternalLinqTest()
        {
            var query = from e in Empresa.Rules.Linq()
                        where e.Nome == "E1"
                        select e;

            Assert.AreEqual(1, query.Count());
            Assert.IsNotNull(query.First());
            Assert.AreEqual("E1", query.First().Nome);

            
        }

    }
}
