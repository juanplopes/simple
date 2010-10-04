using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPOI.SS.UserModel;
using System.Collections;

namespace Simple.Web.Mvc.Excel
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
            var enumerator = sheet.GetRowEnumerator();
            while(enumerator.MoveNext())
            {
                yield return Reader.Read(enumerator.Current as Row, null);
            }
        }

    }
}
