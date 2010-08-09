using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.IO.Serialization;
using System.IO;

namespace Simple.Tests.Metadata
{
    [Explicit]
    [TestFixture("System.Data.SqlClient")]
    [TestFixture("System.Data.OracleClient")]
    [TestFixture("Oracle.DataAccess.Client")]
    public class MetadataFixture
    {
        protected static DatabasesXml Databases =
            SimpleSerializer.Xml<DatabasesXml>().DeserializeTypedFromString(Database.Databases);

        public DatabasesXml.Entry Entry { get; set; }
        public MetadataFixture(string providerName)
        {
            this.Entry = Databases.Entries.FirstOrDefault(x => x.Provider == providerName);
            if (this.Entry == null)
                Assert.Ignore("Connection string not found");
        }

        [Test]
        public void AllTypesTest()
        {
            new AllTypesTest.Run(Entry).ExecuteAll();
        }

        [Test]
        public void ChangeColumnTest()
        {
            new ChangeColumnTest.Run(Entry).ExecuteAll();
        }

        [Test]
        public void ChangeTableTest()
        {
            new ChangeTableTest.Run(Entry).ExecuteAll();
        }

        [Test]
        public void DoubleForeignKeyTest()
        {
            new DoubleForeignKeyTest.Run(Entry).ExecuteAll();
        }

        [Test]
        public void SimpleTableTest()
        {
            new SimpleTableTest.Run(Entry).ExecuteAll();
        }
    }
}

