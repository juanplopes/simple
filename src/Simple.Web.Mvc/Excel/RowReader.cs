using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Reflection;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

namespace Simple.Web.Mvc.Excel
{
    public class RowReader<T>
    {
        protected HeaderDefinition<T> Header { get; set; }
        public RowReader(HeaderDefinition<T> header)
        {
            this.Header = header;
        }

        public int[] ReadHeader(Row row)
        {
            return Enumerable.Range(0, Header.Count).ToArray();
        }

        public T Read(Row row, int[] indexes)
        {
            return default(T);
        }
    }
}
