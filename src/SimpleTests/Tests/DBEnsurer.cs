using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Tool.hbm2ddl;
using Simple.DataAccess;
using Simple.Tests.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simple.Filters;

namespace Simple.Tests
{
    public class DBEnsurer
    {
        public static void Ensure()
        {
            SchemaExport exp = new SchemaExport(SessionManager.GetConfig());
            exp.Drop(true, true);
            exp.Create(true, true);
            InsertTestData();
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
            E1.Save();
            

            F1 = new Funcionario()
            {
                Nome = "F1"
            };
            F1.Save();

            EF1 = new EmpresaFuncionario()
            {
                Empresa = E1,
                Funcionario = F1
            };
            EF1.Save();
        }

        public static void AssertStillOk()
        {
            Assert.AreEqual(1, Empresa.CountByFilter(BooleanExpression.True));
            Assert.AreEqual(1, Funcionario.CountByFilter(BooleanExpression.True));
            Assert.AreEqual(1, EmpresaFuncionario.CountByFilter(BooleanExpression.True));
        }


    }
}
