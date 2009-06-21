using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.Tests.Contracts;

namespace Simple.Tests.Service
{
    [TestFixture]
    public class ServiceConnectionFixture
    {
        [Test]
        public void TestConnectServices()
        {
            Assert.IsTrue(Funcionario.Rules.HeartBeat());
            Assert.IsTrue(Empresa.Rules.HeartBeat());
            Assert.IsTrue(EmpresaFuncionario.Rules.HeartBeat());
        }
    }
}
