using System;
using System.Collections.Generic;
using System.Text;
using Sample.BusinessInterface.Domain;
using SimpleLibrary.NUnit;
using NUnit.Framework;

namespace Sample.BusinessServer.Tests
{
    public class FuncionarioProvider : IEntityProvider<Funcionario>
    {
        #region IEntityProvider<Funcionario> Members

        public Funcionario Populate(int seed)
        {
            return new Funcionario()
            {
                Nome = typeof(Funcionario).GUID.ToString() + seed
            };
        }

        public bool Compare(Funcionario e1, Funcionario e2)
        {
            return e1.Nome == e2.Nome;
        }

        #endregion
    }
    [TestFixture]
    public class FuncionarioTests : BaseTests<Funcionario, FuncionarioProvider>
    {
    }
}
