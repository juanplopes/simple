using System;
using System.Collections.Generic;

using System.Text;
using SimpleLibrary.NUnit;
using Sample.BusinessInterface.Domain;
using NUnit.Framework;

namespace Sample.BusinessServer.Tests
{
    public class EmpresaProvider : IEntityProvider<Empresa>
    {
        #region IEntityProvider<Empresa> Members

        public Empresa Populate(int seed)
        {
            return new Empresa()
            {
                Nome = typeof(Empresa).GUID.ToString() + seed
            };
        }

        public bool Compare(Empresa e1, Empresa e2)
        {
            return e1.Nome == e2.Nome;
        }

        #endregion
    }

    [TestFixture]
    public class EmpresaTests : BaseTests<Empresa, EmpresaProvider>
    {
    }
}
