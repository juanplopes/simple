using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.IO.Serialization;
using System.IO;

namespace Simple.Tests.Metadata
{

    [TestFixture, Explicit]
    public class RunAndRollBackFixture
    {
        public IList<DatabasesXml.Entry> TestCases()
        {
            var xml = SimpleSerializer.Xml<DatabasesXml>().DeserializeTypedFromString(Database.Databases);
            return xml.Entries;
        }

        [TestCaseSource("TestCases")]
        public void AllTypesTest(DatabasesXml.Entry entry)
        {
            new AllTypesTest.Run(entry).ExecuteAll();
        }

        [TestCaseSource("TestCases")]
        public void ChangeColumnTest(DatabasesXml.Entry entry)
        {
            new ChangeColumnTest.Run(entry).ExecuteAll();
        }

        [TestCaseSource("TestCases")]
        public void ChangeTableTest(DatabasesXml.Entry entry)
        {
            new ChangeTableTest.Run(entry).ExecuteAll();
        }

        [TestCaseSource("TestCases")]
        public void DoubleForeignKeyTest(DatabasesXml.Entry entry)
        {
            new DoubleForeignKeyTest.Run(entry).ExecuteAll();
        }

        [TestCaseSource("TestCases")]
        public void SimpleTableTest(DatabasesXml.Entry entry)
        {
            new SimpleTableTest.Run(entry).ExecuteAll();
        }
    }
}
