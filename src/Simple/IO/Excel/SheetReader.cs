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
        protected RowReader<T> Reader { get; set; }
        public SheetReader(RowReader<T> reader)
        {
            Reader = reader;
        }

        public IEnumerable<T> Read(Sheet sheet)
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
