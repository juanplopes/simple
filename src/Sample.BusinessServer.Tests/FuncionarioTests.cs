using System;
using System.Collections.Generic;
using System.Text;
using Sample.BusinessInterface.Domain;
using SimpleLibrary.NUnit;
using NUnit.Framework;

namespace Sample.BusinessServer.Tests
{
    public class FuncionarioProvider : BaseEntityProvider<Funcionario>
    {
        public FuncionarioProvider() : base(true) { }
    }

    [TestFixture]
    public class FuncionarioTests : BaseTests<Funcionario, FuncionarioProvider>
    {
    }
}
