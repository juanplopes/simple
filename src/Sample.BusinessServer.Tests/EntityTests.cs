using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Simple.NUnit;
using Sample.BusinessInterface.Domain;

namespace Sample.BusinessServer.Tests
{
    [TestFixture]
    public class EntityTests : EntityTestsOrchestrator
    {
        public EntityTests()
        {
            AddType(typeof(Empresa), true);
            AddType(typeof(Funcionario), true);
        }
    }
}
