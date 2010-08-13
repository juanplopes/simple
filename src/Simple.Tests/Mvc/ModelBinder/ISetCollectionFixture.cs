using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Iesi.Collections.Generic;
using NUnit.Framework;

namespace Simple.Tests.Mvc.ModelBinder
{
    [TestFixture]
    public class ISetCollectionFixture : BaseCollectionFixture<CompanyISet, ISet<Address>>
    {
    }
}
