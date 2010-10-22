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
    public class DictionaryNullableFixture
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
            header.Register(x => x.ColunaD).Named("D");
            header.Register(x => x.GetWhatever()[1].ToString()).Named("H");
            header.Register(x => x.GetWhatever()[3].ToString()).Named("I");
            return header;
        }

        [TestFixtureSetUp]
        public void Setup()
        {
            var header = CreateHeader();
            var writer = WorkbookWriter.Create(header);

            var bytes = writer.WriteBytes("test123", EnumerateData());
            data = new HSSFWorkbook(new MemoryStream(bytes));
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
            row.GetCell(1).StringCellValue.Should().Be("D");
            row.GetCell(2).StringCellValue.Should().Be("H");
            row.GetCell(3).StringCellValue.Should().Be("I");
        }

        [Test]
        public void SecondRowMustHaveCorrectValuesAndCorrectTypes()
        {
            var sheet = data.GetSheetAt(0);
            var row = sheet.GetRow(1);
            row.GetCell(0).StringCellValue.Should().Be("asd");
            row.GetCell(1).StringCellValue.Should().Be("Cancelado");
            row.GetCell(2).StringCellValue.Should().Be("asd");
            row.GetCell(3).StringCellValue.Should().Be("Cancelado");
        }

        [Test]
        public void ThirdRowMustHaveCorrectValuesAndCorrectTypes()
        {
            var sheet = data.GetSheetAt(0);
            var row = sheet.GetRow(2);
            row.GetCell(0).Should().Be.Null();
            row.GetCell(1).StringCellValue.Should().Be("Inativo");
            row.GetCell(2).Should().Be.Null();
            row.GetCell(3).StringCellValue.Should().Be("Inativo");
        }

        [Test]
        public void FourthRowMustHaveCorrectValuesAndCorrectTypes()
        {
            var sheet = data.GetSheetAt(0);
            var row = sheet.GetRow(3);
            row.GetCell(0).StringCellValue.Should().Be("asd3");
            row.GetCell(1).Should().Be.Null();
            row.GetCell(2).StringCellValue.Should().Be("asd3");
            row.GetCell(3).Should().Be.Null();
        }

    }
}
