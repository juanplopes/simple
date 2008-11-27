using System;
using System.Collections.Generic;

using System.Text;
using SimpleLibrary.NUnit;
using Sample.BusinessInterface.Domain;
using NUnit.Framework;

namespace Sample.BusinessServer.Tests
{
    public class EmpresaProvider : BaseEntityProvider<Empresa>
    { }


    [TestFixture]
    public class EmpresaTests : BaseTests<Empresa, EmpresaProvider>
    {
    }
}
