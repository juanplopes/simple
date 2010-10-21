using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.IO.Excel;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using SharpTestsEx;

namespace Simple.Tests.IO.Excel.Writer
{
    [TestFixture]
    public class NullableFixture
    {
        Workbook data;

        private static IEnumerable<NullableData> EnumerateData()
        {
            yield return new NullableData() { ColunaA = "asd", ColunaB = 123, ColunaC = null, ColunaD = NullableData.Status.Cancelado, ColunaE = true };
            yield return new NullableData() { ColunaA = null, ColunaB = 234, ColunaC = new DateTime(2010, 5, 13), ColunaD = NullableData.Status.Inativo, ColunaE = false };
            yield return new NullableData() { ColunaA = "asd3", ColunaB = null, ColunaC = new DateTime(2011, 12, 31), ColunaD = null, ColunaE = null };
        }

        private static HeaderDefinition<NullableData> CreateHeader()
        {
            var header = new HeaderDefinition<NullableData>();
            header.Register(x => x.ColunaA).Named("A");
            header.Register(x => x.ColunaB).Named("B");
            header.Register(x => x.ColunaC).Formatter("pt-BR");
            header.Register(x => x.ColunaD).Named("D");
            header.Register(x => x.ColunaE);
            return header;
        }

        [TestFixtureSetUp]
        public void Setup()
        {
            var header = CreateHeader();
            var writer = WorkbookWriter.Create(header);

            data = new HSSFWorkbook();
            writer.Write(data, "test123", EnumerateData());
            writer.RemoveTemplate(data);
        }



        [Test]
        public void BookShouldHaveOnlyOneSheetNamed_test123()
        {
            data.NumberOfSheets.Should().Be(1);
            data.GetSheetName(0).Should().Be("test123");
        }

        [Test]
        public void FirstRowMustBeEmpty()
        {
            var sheet = data.GetSheetAt(0);
            var row = sheet.GetRow(0);
            row.GetCell(0).StringCellValue.Should().Be("A");
            row.GetCell(1).StringCellValue.Should().Be("B");
            row.GetCell(2).StringCellValue.Should().Be("ColunaC");
            row.GetCell(3).StringCellValue.Should().Be("D");
            row.GetCell(4).StringCellValue.Should().Be("ColunaE");
        }

        [Test]
        public void SecondRowMustHaveCorrectValuesAndCorrectTypes()
        {
            var sheet = data.GetSheetAt(0);
            var row = sheet.GetRow(1);
            row.GetCell(0).StringCellValue.Should().Be("asd");
            row.GetCell(1).NumericCellValue.Should().Be(123);
            row.GetCell(2).Should().Be.Null();
            row.GetCell(3).StringCellValue.Should().Be("Cancelado");
            row.GetCell(4).BooleanCellValue.Should().Be(true);
        }

        [Test]
        public void ThirdRowMustHaveCorrectValuesAndCorrectTypes()
        {
            var sheet = data.GetSheetAt(0);
            var row = sheet.GetRow(2);
            row.GetCell(0).Should().Be.Null();
            row.GetCell(1).NumericCellValue.Should().Be(234);
            row.GetCell(2).DateCellValue.Should().Be(new DateTime(2010, 5, 13));
            row.GetCell(3).StringCellValue.Should().Be("Inativo");
            row.GetCell(4).BooleanCellValue.Should().Be(false);
        }

        [Test]
        public void FourthRowMustHaveCorrectValuesAndCorrectTypes()
        {
            var sheet = data.GetSheetAt(0);
            var row = sheet.GetRow(3);
            row.GetCell(0).StringCellValue.Should().Be("asd3");
            row.GetCell(1).Should().Be.Null();
            row.GetCell(2).DateCellValue.Should().Be(new DateTime(2011, 12, 31));
            row.GetCell(3).Should().Be.Null();
            row.GetCell(4).Should().Be.Null();
        }

        [Test]
        public void CanReParseWorkbook()
        {
            var result = WorkbookReader.Create(CreateHeader()).Read(data);
            result.Sheets.Count.Should().Be(1);

            var sheet = result["test123"];
            sheet.Errors.Should().Have.Count.EqualTo(0);
            var records = sheet.Records.ToList();
            records.Count.Should().Be(3);
            records[0].AssertWith("asd", 123, null, NullableData.Status.Cancelado, true);
            records[1].AssertWith(null, 234, new DateTime(2010, 5, 13), NullableData.Status.Inativo, false);
            records[2].AssertWith("asd3", null, new DateTime(2011, 12, 31), null, null);

        }

        [Test]
        public void FifthRowMustBeNull()
        {
            var sheet = data.GetSheetAt(0);
            var row = sheet.GetRow(4);
            row.Should().Be.Null();
        }
    }
}
