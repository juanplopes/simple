using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Simple.Web.Mvc.Excel
{
    public class SheetReader<T>
    {
        protected RowReader<T> Reader { get; set; }
        public SheetReader(RowReader<T> reader) 
        {
            Reader = reader;
        }

        public IEnumerable<T> Read(SharedStringTable strings, WorksheetPart worksheet)
        {
            var data = worksheet.Worksheet.OfType<SheetData>().FirstOrDefault();
            if (data == null) yield break;

            var rows = data.Descendants<Row>().OrderBy(x=>x.RowIndex.Value);

            var indices = Reader.ReadHeader(rows.FirstOrDefault());
            foreach (var row in rows.Skip(1))
                yield return Reader.Read(strings, row, indices);
        }
    }
}
