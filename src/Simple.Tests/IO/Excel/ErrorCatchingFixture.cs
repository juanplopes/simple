using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.IO.Excel;
using System.IO;
using NPOI.HSSF.UserModel;
using SharpTestsEx;

namespace Simple.Tests.IO.Excel
{
    [TestFixture]
    public class ErrorCatchingFixture
    {
        [Test]
        public void CanGetFormattingErrors()
        {
            using (var memory = new MemoryStream(ExcelFiles.FirstSample))
            {
                var header = new HeaderDefinition<FirstSampleData>();
                header.Skip(2);
                header.Register(x => x.ColunaC).Formatter("en-US");

                var data = WorkbookReader.Create(header)
                    .Read(new HSSFWorkbook(memory))[0];

                data.Records.Count.Should().Be(4);
                data.Errors.Count.Should().Be(4);
                data.Errors.Select(x => x.Column).Should().Have.SameSequenceAs(2, 2, 2, 2);
            }

        }

       
        [Test]
        public void CanGetNonNullableColumnsError()
        {
            using (var memory = new MemoryStream(ExcelFiles.MultiSheetsSample))
            {
                var header = new HeaderDefinition<FirstSampleData>();
                header.Skip(2);
                header.Register(x => x.ColunaC).Formatter("pt-BR");

                var data = WorkbookReader.Create(header)
                    .Read(new HSSFWorkbook(memory))[1];

                data.Records.Count.Should().Be(4);
                data.Errors.Count.Should().Be(1);
                data.Errors.Select(x => x.Column).Should().Have.SameSequenceAs(2);
                data.Errors.Select(x => x.Row).Should().Have.SameSequenceAs(3);
            }

        }

        [Test]
        public void CanStopWhenNoMoreLinesFound()
        {
            using (var memory = new MemoryStream(ExcelFiles.MultiSheetsSample))
            {
                var header = new HeaderDefinition<FirstSampleData>();
                header.Skip(2);
                header.Register(x => x.ColunaC).Formatter("pt-BR");

                var data = WorkbookReader.Create(header, 2)
                    .Read(new HSSFWorkbook(memory))["TestD"];

                data.Records.Count.Should().Be(0);
            }

        }
    }
}
