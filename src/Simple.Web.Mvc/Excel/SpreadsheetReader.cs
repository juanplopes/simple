using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Simple.Web.Mvc.Excel
{
    public class SpreadsheetReader
    {
        public static SpreadsheetReader<T> Create<T>(HeaderDefinition<T> header)
        {
            return new SpreadsheetReader<T>(
                new SheetReader<T>(
                    new RowReader<T>(header)));
        }
    }

    public class SpreadsheetReader<T>
    {
        protected SheetReader<T> Reader { get; set; }

        public SpreadsheetReader(SheetReader<T> reader) 
        {
            Reader = reader;
        }

        public IDictionary<string, IEnumerable<T>> Read(SpreadsheetDocument document)
        {
            var dictionary = new Dictionary<string, IEnumerable<T>>();

            foreach (var sheet in document.WorkbookPart.Workbook.Descendants<Sheet>())
            {
                var worksheet = document.WorkbookPart.GetPartById(sheet.Id) as WorksheetPart;
                if (worksheet != null)
                    dictionary[sheet.Name] = Reader.Read(document.WorkbookPart.SharedStringTablePart.SharedStringTable,
                        worksheet).ToList();
            }
            return dictionary;
            
        }

    }
}