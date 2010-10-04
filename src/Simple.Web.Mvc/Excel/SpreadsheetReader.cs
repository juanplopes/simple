using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

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

        public IDictionary<string, IEnumerable<T>> Read(Workbook workbook)
        {
            var dictionary = new Dictionary<string, IEnumerable<T>>();

            for (int i = 0; i < workbook.NumberOfSheets; i++)
            {
                var sheet = workbook.GetSheetAt(i);
                var sheetName = workbook.GetSheetName(i);
                dictionary[sheetName] = Reader.Read(sheet).ToList();
            }
            return dictionary;

        }

    }
}