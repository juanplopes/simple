using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Simple.Tests.Mvc.ModelBinder
{
    [TestFixture]
    public class ArrayCollectionFixture : BaseCollectionFixture<CompanyArray, Address[]>
    {
    }
}
