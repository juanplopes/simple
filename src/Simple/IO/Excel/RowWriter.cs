using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using System.Reflection;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

namespace Simple.IO.Excel
{
    public class RowWriter<T>
    {
        private static ILog log = Simply.Do.Log(MethodBase.GetCurrentMethod());

        protected HeaderDefinition<T> Header { get; set; }
        public RowWriter(HeaderDefinition<T> header)
        {
            this.Header = header;
        }

        public void WriteHeader(Row row)
        {
            for (int i = 0; i < Header.Count; i++)
            {
                var header = Header[i];
                if (!header.Exportable) continue;

                var cell = row.GetCell(i, MissingCellPolicy.CREATE_NULL_AS_BLANK);
                
                cell.SetCellType(CellType.STRING);
                cell.SetCellValue(header.Name);
            }
        }

        public void Write(int rowNum, Row row, T record)
        {
            for (int i = 0; i < Header.Count; i++)
            {
                var header = Header[i];
                if (!header.Exportable) continue;

                var obj = header.Get(record);
                if (obj == null) continue;

                var cell = row.GetCell(i, MissingCellPolicy.CREATE_NULL_AS_BLANK);
                WriteCell(cell, obj);
            }
        }

        protected void WriteCell(Cell cell, object value)
        {
            if (value == null) value = "";
            var type = value.GetType();

            if (type.IsNumericType(false))
            {
                cell.SetCellType(CellType.NUMERIC);
                cell.SetCellValue(Convert.ToDouble(value));
            }
            else if (type.CanAssign<DateTime>())
            {
                cell.SetCellType(CellType.NUMERIC);
                cell.SetCellValue((DateTime)value);
            }
            else if (type.CanAssign<bool>())
            {
                cell.SetCellType(CellType.BOOLEAN);
                cell.SetCellValue((bool)value);
            }
            else
            {
                cell.SetCellType(CellType.STRING);
                cell.SetCellValue(value.ToString());
            }
        }
    }
}
