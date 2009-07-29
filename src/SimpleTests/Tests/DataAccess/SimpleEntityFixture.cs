using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.Tests.Contracts;
using Simple.DataAccess;
using Simple.Filters;
using NHibernate.Linq;
using Simple.ConfigSource;
using Simple.Services.Remoting;
using Simple.Tests.Service;
using Simple.DataAccess.Context;
using NHibernate;
using Simple.Tests.DataAccess.Samples;

namespace Simple.Tests.DataAccess
{
    [TestFixture]
    public class SimpleEntityFixture : BaseDataFixture
    {
        [Test]
        public void InsertEmpresa()
        {
            var sample = new SimpleFourEntityInsert();

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

        [Test]
        public void TestEntityDeletion()
        {
            var sample = new SimpleFourEntityInsert();
            Assert.AreEqual(1, Empresa.CountByFilter(BooleanExpression.True));
            BaseDataFixture.DeleteFromAllTables(typeof(DBEnsurer));
            Assert.AreEqual(0, Empresa.CountByFilter(BooleanExpression.True));
        }

        [Test]
        public void UpdateEmpresa()
        {
            var sample = new SimpleFourEntityInsert();

            Empresa e = Empresa.LoadByFilter(BooleanExpression.True);
            Assert.AreEqual(e.Id, sample.Empresa.Id);
            e.Nome = "Empresa2";
            e = e.SaveOrUpdate();

            Assert.AreEqual(e.Nome, Empresa.Load(e.Id).Nome);
        }

        [Test]
        public void ImplicitJoinLinqTest()
        {
            var sample = new SimpleFourEntityInsert();
            var queryable = GetSimply().OpenSession().Linq<EmpresaFuncionario>();
            var query = from ef in queryable
                        where ef.Funcionario.Nome == "Funcionario1" &&
                        ef.Empresa.Nome == "Empresa1"
                        select ef.Funcionario;

            Assert.AreEqual(1, query.Count());
            Assert.AreEqual("Funcionario1", query.First().Nome);
        }

        [Test]
        public void ImplicitJoinLinqTest2()
        {
            var sample = new SimpleFourEntityInsert();
            var queryable = GetSimply().OpenSession().Linq<RelEmpresaFuncionario>();
            var query = from ef in queryable
                        where ef.Relacao.Funcionario.Nome == "Funcionario1" &&
                        ef.Relacao.Empresa.Nome == "Empresa1"
                        select ef.Relacao;

            Assert.AreEqual(1, query.Count());
            Assert.AreEqual(sample.Mapping.Id, query.First().Id);
        }

        [Test]
        public void ImplicitJoinLinqTestNoRecords()
        {
            var sample = new SimpleFourEntityInsert();
            var queryable = GetSimply().OpenSession().Linq<EmpresaFuncionario>();
            var query = from ef in queryable
                        where ef.Funcionario.Nome == "Funcionario1" &&
                        ef.Empresa.Nome == "Empresa1000"
                        select ef.Funcionario;

            Assert.AreEqual(0, query.Count());
        }

        [Test]
        public void ImplicitJoinLinqTestNoRecords2()
        {
            var sample = new SimpleFourEntityInsert();
            var queryable = GetSimply().OpenSession().Linq<RelEmpresaFuncionario>();
            var query = from ef in queryable
                        where ef.Relacao.Funcionario.Nome == "Funcionario1" &&
                        ef.Relacao.Empresa.Nome == "Empresa1000"
                        select ef.Relacao;

            Assert.AreEqual(0, query.Count());
        }

        [Test]
        public void FirstLinqTest()
        {
            var sample = new SimpleFourEntityInsert();

            var queryable = GetSimply().OpenSession().Linq<Empresa>();
            var query = from e in queryable
                        where e.Nome == "Empresa1"
                        select e.Nome;

            Assert.AreEqual(1, query.Count());

            Assert.AreEqual("Empresa1", query.First());
        }

        [Test]
        public void SecondLinqTest()
        {
            var sample = new SimpleFourEntityInsert();

            Empresa e = Empresa.Do.GetByNameLinq("Empresa1");

            Assert.IsNotNull(e);
            Assert.AreEqual("Empresa1", e.Nome);
        }


    }
}