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
    public class ComplexDataFixture
    {
        SheetResultCollection<ComplexData> data;

        [TestFixtureSetUp]
        public void Setup()
        {
            using (var memory = new MemoryStream(ExcelFiles.FirstSample))
            {
                var header = new HeaderDefinition<ComplexData>();
                header.Register(x => x.PropA.ColunaA);
                header.Register(x => x.PropA.ColunaB);
                header.Register(x => x.PropB.PropC.ColunaC).Formatter("pt-BR");
                header.Register(x => x.PropB.PropC.ColunaD);
                header.Register(x => x.PropB.PropC.ColunaE);

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
        public void Sheet2ShouldHave0Rows()
        {
            data["Sheet2"].Records.Should().Have.Count.EqualTo(0);
            data["Sheet2"].Errors.Should().Have.Count.EqualTo(0);
        }

        [Test]
        public void Sheet3ShouldHave0Rows()
        {
            data["Sheet3"].Records.Should().Have.Count.EqualTo(0);
            data["Sheet3"].Errors.Should().Have.Count.EqualTo(0);
        }


        [Test]
        public void Sheet1DataIsCorrect()
        {
            var sheet = data[0].Records.ToList();

            sheet[0].AssertWith("asd", 123, new DateTime(1915, 04, 13), ComplexData.Status.Ativo, true);
            sheet[1].AssertWith("asd2", 234, new DateTime(1915, 04, 14), ComplexData.Status.Ativo, false);
            sheet[2].AssertWith("asd3", 345, new DateTime(1915, 04, 15), ComplexData.Status.Inativo, true);
            sheet[3].AssertWith("asd4", 456, new DateTime(1915, 04, 16), ComplexData.Status.Cancelado, false);
        }
    }
}
