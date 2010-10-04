using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.Web.Mvc.Excel;
using System.IO;
using SharpTestsEx;
using System.Globalization;
using NPOI.HSSF.UserModel;

namespace Simple.Tests.Mvc.Excel
{
    [TestFixture, Ignore]
    public class SpreadsheetReaderFixture
    {
        IDictionary<string, IEnumerable<FirstSampleData>> data;

        [TestFixtureSetUp]
        public void Setup()
        {
            using (var memory = new MemoryStream(ExcelFiles.FirstSample))
            {
                var header = new HeaderDefinition<FirstSampleData>();
                header.Register(x => x.ColunaA, "Coluna A");
                header.Register(x => x.ColunaB, "Coluna B");
                header.Register(x => x.ColunaC, "Coluna C").Formatter("pt-BR");
                header.Register(x => x.ColunaD, "Coluna D");
                header.Register(x => x.ColunaE, "Coluna E");

                data = SpreadsheetReader.Create(header)
                    .Read(new HSSFWorkbook(memory));
            }
        }

        [Test]
        public void CanReadWorksheetNames()
        {
            data.Should().Have.Count.EqualTo(3);
            data.Keys.Should().Have.SameValuesAs("Sheet1", "Sheet2", "Sheet3");
        }

        [Test]
        public void Sheet1ShouldHave4Rows()
        {
            data["Sheet1"].Should().Have.Count.EqualTo(4);
        }

        [Test]
        public void CanReadFirstSample()
        {
            var sheet = data["Sheet1"].ToList();

            sheet[0].AssertWith("asd", 123, new DateTime(1915, 04, 13), FirstSampleData.Status.Ativo, true);
            sheet[0].AssertWith("asd2", 234, new DateTime(1915, 04, 14), FirstSampleData.Status.Ativo, false);
            sheet[0].AssertWith("asd3", 345, new DateTime(1915, 04, 15), FirstSampleData.Status.Inativo, true);
            sheet[0].AssertWith("asd4", 456, new DateTime(1915, 04, 16), FirstSampleData.Status.Cancelado, false);
        }
    }
}
