using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simple.Tests.Contracts;

namespace Simple.Tests.Service
{
    [TestClass]
    public class ServiceConnectionFixture
    {
        [TestMethod]
        public void TestConnectServices()
        {
            Assert.IsTrue(Funcionario.Rules.HeartBeat());
            Assert.IsTrue(Empresa.Rules.HeartBeat());
            Assert.IsTrue(EmpresaFuncionario.Rules.HeartBeat());
        }
    }
}
