using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sample.BusinessServer.Tests.Providers;
using SimpleLibrary.NUnit;
using Sample.BusinessInterface.Domain;
using NUnit.Framework;

namespace Sample.BusinessServer.Tests
{
    [TestFixture]
    public class EmpresaTests : BaseTests<Empresa, EmpresaProvider>
    {
    }
}
