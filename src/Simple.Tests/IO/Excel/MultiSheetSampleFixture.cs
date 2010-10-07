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

namespace Simple.Tests.IO.Excel
{
    [TestFixture]
    public class MultiSheetSampleFixture
    {
        SheetResultCollection<NullableSampleData> data;

        [TestFixtureSetUp]
        public void Setup()
        {
            using (var memory = new MemoryStream(ExcelFiles.MultiSheetsSample))
            {
                var header = new HeaderDefinition<NullableSampleData>();
                header.Register(x => x.ColunaA);
                header.Register(x => x.ColunaB);
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
            data.Select(x => x.Name).Should().Have.SameSequenceAs("TestA", "TestB", "TestD");
        }

        [Test]
        public void Sheet1ShouldHave4Rows()
        {
            data["TestA"].Records.Should().Have.Count.EqualTo(4);
            data["TestA"].Errors.Should().Have.Count.EqualTo(0);

        }

        [Test]
        public void Sheet2ShouldHave4Rows()
        {
            data["TestB"].Records.Should().Have.Count.EqualTo(4);
            data["TestB"].Errors.Should().Have.Count.EqualTo(0);
        }

        [Test]
        public void Sheet3ShouldHave4RowsWith3ErrorsBecauseOfMissingRows()
        {
            data["TestD"].Records.Should().Have.Count.EqualTo(1);
            data["TestD"].Errors.Should().Have.Count.EqualTo(0);
        }

        [Test]
        public void Sheet1DataIsCorrect()
        {
            var sheet = data[0].Records.ToList();

            sheet[0].AssertWith("asd", 123, new DateTime(1915, 04, 13), NullableSampleData.Status.Ativo, true);
            sheet[1].AssertWith("asd2", 234, new DateTime(1915, 04, 14), NullableSampleData.Status.Ativo, false);
            sheet[2].AssertWith("asd3", 345, new DateTime(1915, 04, 15), NullableSampleData.Status.Inativo, true);
            sheet[3].AssertWith("asd4", 456, new DateTime(1915, 04, 16), NullableSampleData.Status.Cancelado, false);
        }

        [Test]
        public void Sheet2DataIsCorrect()
        {
            var sheet = data[1].Records.ToList();

            sheet[0].AssertWith(null, 123, new DateTime(1915, 04, 13), NullableSampleData.Status.Ativo, true);
            sheet[1].AssertWith("asd2", null, new DateTime(1915, 04, 14), NullableSampleData.Status.Ativo, false);
            sheet[2].AssertWith("asd3", 345, null, NullableSampleData.Status.Inativo, null);
            sheet[3].AssertWith("asd4", 456, new DateTime(1915, 04, 16), null, false);
        }

        [Test]
        public void Sheet3DataIsCorrect()
        {
            var sheet = data[2].Records.ToList();

            sheet[0].AssertWith("asd4", 456, new DateTime(1915, 04, 16), NullableSampleData.Status.Cancelado, false);
        }

    }
}
