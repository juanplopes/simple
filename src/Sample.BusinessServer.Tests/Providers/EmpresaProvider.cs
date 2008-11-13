using System;
using System.Collections.Generic;

using System.Text;
using Sample.BusinessInterface.Domain;
using SimpleLibrary.NUnit;
using SimpleLibrary.DataAccess;

namespace Sample.BusinessServer.Tests.Providers
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
}
