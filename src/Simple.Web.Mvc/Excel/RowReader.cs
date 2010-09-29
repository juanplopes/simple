using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Simple.Web.Mvc.Excel
{
    public class RowReader<T>
    {
        protected HeaderDefinition<T> Header { get; set; }
        public RowReader(HeaderDefinition<T> header)
        {
            this.Header = header;
        }

        public T Read(Row row)
        {
            return default(T);
        }
    }
}
