using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.IO.Excel;
using System.IO;
using SharpTestsEx;
using System.Globalization;
using NPOI.HSSF.UserModel;


namespace Simple.Tests.IO.Excel.Reader
{
    [TestFixture]
    public class BasicSkippingFixture
    {
        SheetResultCollection<BasicData> data;

        [TestFixtureSetUp]
        public void Setup()
        {
            using (var memory = new MemoryStream(ExcelFiles.FirstSample))
            {
                var header = new HeaderDefinition<BasicData>();
                header.Skip(2);
                header.Register(x => x.ColunaC).Formatter("pt-BR");
                header.Register(x => x.ColunaD);
                header.Register(x => x.ColunaE);

                data = WorkbookReader.Create(header)
                    .Read(new HSSFWorkbook(memory));
            }
        }

        [Test]
        public void CanReadWorksheetNames()
        {
            data.Should().Have.Count.EqualTo(3);
            data.Select(x=>x.Name).Should().Have.SameSequenceAs("Sheet1", "Sheet2", "Sheet3");
        }

        [Test]
        public void Sheet1ShouldHave4Rows()
        {
            data["Sheet1"].Records.Should().Have.Count.EqualTo(4);
            data["Sheet1"].Errors.Should().Have.Count.EqualTo(0);
        }

        [Test]
        public void Sheet1DataIsCorrect()
        {
            var sheet = data[0].Records.ToList();

            sheet[0].AssertWith(null, 0, new DateTime(1915, 04, 13), BasicData.Status.Ativo, true);
            sheet[1].AssertWith(null, 0, new DateTime(1915, 04, 14), BasicData.Status.Ativo, false);
            sheet[2].AssertWith(null, 0, new DateTime(1915, 04, 15), BasicData.Status.Inativo, true);
            sheet[3].AssertWith(null, 0, new DateTime(1915, 04, 16), BasicData.Status.Cancelado, false);
        }
    }
}
