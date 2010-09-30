using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DocumentFormat.OpenXml.Spreadsheet;
using Simple.Reflection;

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

        public T Read(SharedStringTable strings, Row row, int[] indices)
        {
            var cells = row.Descendants<Cell>().Take(indices.Length);
            var instance = Header.CreateInstance();

            int idx = 0;
            foreach (var cell in cells)
            {
                bool shared = cell.DataType == "s";
                string strValue = cell.CellValue.Text;
                if (shared)
                {
                    int stringIdx = Convert.ToInt32(strValue);
                    strValue = strings.Skip(stringIdx).OfType<SharedStringItem>().FirstOrDefault().Text.Text;
                }
                var target = Header[idx];
                target.Set(target, strValue);

                idx++;
            }

            return (T)instance;
        }
    }
}
