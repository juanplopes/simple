using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Reflection;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

namespace Simple.IO.Excel
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
            var target = Header.CreateInstance();
            for (int i = 0; i < indexes.Length; i++)
            {
                var column = Header[i];
                column.Set(target, GetCellValue(row.GetCell(i), null));
            }
            return target;
        }

        public object GetCellValue(Cell cell, CellType? type)
        {
            switch (type ?? cell.CellType)
            {
                case CellType.BLANK:
                    return string.Empty;
                case CellType.BOOLEAN:
                    return cell.BooleanCellValue;
                case CellType.ERROR:
                    return cell.ErrorCellValue;
                case CellType.FORMULA:
                    return GetCellValue(cell, cell.CachedFormulaResultType);
                case CellType.NUMERIC:
                    return cell.NumericCellValue;
                case CellType.STRING:
                    return cell.StringCellValue;
                default:
                    return cell.DateCellValue;
            }
        }
    }
}
