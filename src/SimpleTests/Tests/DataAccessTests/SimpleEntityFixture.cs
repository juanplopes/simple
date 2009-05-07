using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.Tests.Contracts;
using Simple.NUnit;
using Simple.DataAccess;
using Simple.Filters;

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

    }
}
