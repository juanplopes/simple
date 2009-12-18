using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Migrator.Framework;
using Rhino.Mocks;

namespace Migrator.Fluent.Tests.Basics
{
    [TestFixture]
    public class TableInstructionsFixture : BaseFixture
    {
        [Test]
        public void CanCreateTableWith1IntColumn()
        {
            var provider = Mocks.StrictMock<ITransformationProvider>();
            
            var builder = new SchemaBuilder(provider);
            builder.AddTable("whatever", false, t => t.AddInt32("testInt"));
        }
    }
}
