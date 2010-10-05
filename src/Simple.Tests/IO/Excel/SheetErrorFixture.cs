using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.IO.Excel;
using SharpTestsEx;

namespace Simple.Tests.IO.Excel
{
    [TestFixture]
    public class SheetErrorFixture
    {
        [Test]
        public void CanShowMessageWithRowAndColumn()
        {
            var error = new SheetError(1, 4, "test");
            error.DisplayMessage.Should().Be("Cell E2: test");
        }

        [Test]
        public void CanShowMessageWithRowOnly()
        {
            var error = new SheetError(1, "test");
            error.DisplayMessage.Should().Be("Row 2: test");
        }
    }
}
