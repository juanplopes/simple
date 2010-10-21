using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using log4net;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.IO;

namespace Simple.IO.Excel
{
    public class SheetWriter<T>
    {
        private static ILog log = Simply.Do.Log(MethodBase.GetCurrentMethod());

        public HeaderDefinition<T> Header { get; protected set; }
        public RowWriter<T> Writer { get; protected set; }

        public SheetWriter(HeaderDefinition<T> header)
        {
            Header = header;
            Writer = new RowWriter<T>(header);
        }

        public void Write(Sheet sheet, IEnumerable<T> items)
        {
            var current = Header.SkipRows;
            var row =  sheet.CreateRow(current);
            Writer.WriteHeader(row);

            foreach (var item in items)
            {
                row = sheet.CreateRow(++current);
                Writer.Write(current, row, item);
            }
        }



    }
}
