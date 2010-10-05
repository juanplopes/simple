using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPOI.SS.UserModel;
using System.Collections;

namespace Simple.IO.Excel
{
    public class SheetReader<T>
    {
        internal protected RowReader<T> Reader { get; set; }
        internal SheetReader(RowReader<T> reader)
        {
            Reader = reader;
        }

        public SheetResult<T> Read(Sheet sheet)
        {
            try
            {
                var results = ReadInternal(sheet);
                return new SheetResult<T>(sheet.SheetName,
                    results.Select(x => x.Result)
                    , results.SelectMany(x => x.Errors));
            }
            catch (Exception e)
            {
                return new SheetResult<T>(sheet.SheetName, new T[0], new[] { new SheetError(0, e.Message) });
            }
        }

        public IEnumerable<RowReader<T>.RowResult> ReadInternal(Sheet sheet)
        {
            var first = sheet.FirstRowNum;
            var indexes = Reader.ReadHeader(sheet.GetRow(first));
            for (int i = first + 1; i <= sheet.LastRowNum; i++)
            {
                yield return Reader.Read(sheet.GetRow(i), indexes);
            }
        }

    }
}
