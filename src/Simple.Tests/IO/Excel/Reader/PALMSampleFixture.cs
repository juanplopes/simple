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
    public class PALMSampleFixture
    {
        class Sample
        {
            public DateTime DateProp { get; set; }
        }

        SheetResultCollection<Sample> data;

        [TestFixtureSetUp]
        public void Setup()
        {
            using (var memory = new MemoryStream(ExcelFiles.SamplePALM))
            {
                var header = new HeaderDefinition<Sample>();
                header.Skip(28);
                header.Register(x => x.DateProp);
                header.SkippingRows(7);

                data = WorkbookReader.Create(header)
                    .Read(new HSSFWorkbook(memory));
            }
        }

       

        [Test]
        public void Sheet1ShouldHave4Rows()
        {
            data[0].Records.Should().Have.Count.EqualTo(19);
        }


        [Test]
        public void ShouldHave8RecordsWithDateSeptember20th()
        {
            var sheet = data[0].Records.ToList();

            sheet.Count(x => x.DateProp == new DateTime(2010, 9, 20)).Should().Be(8);
        }
    }
}
