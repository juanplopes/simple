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
        public class RowResult
        {
            public T Result { get; set; }
            internal IList<SheetError> PrivateErrors { get; set; }
            public IEnumerable<SheetError> Errors { get { return PrivateErrors; } }

            public RowResult() { PrivateErrors = new List<SheetError>(); }
        }

        protected HeaderDefinition<T> Header { get; set; }
        public RowReader(HeaderDefinition<T> header)
        {
            this.Header = header;
        }

        public int[] ReadHeader(Row row)
        {
            return Enumerable.Range(0, Header.Count).ToArray();
        }

        public RowResult Read(Row row, int[] indexes)
        {
            var target = new RowResult { Result = Header.CreateInstance() };
            if (row == null) return target;

            for (int i = 0; i < indexes.Length; i++)
            {
                try
                {
                    var column = Header[i];
                    column.Set(target.Result, GetCellValue(row.GetCell(i), null));
                }
                catch (Exception e)
                {
                    target.PrivateErrors.Add(new SheetError(row.RowNum, i, e.Message));
                }
            }
            return target;
        }

        public object GetCellValue(Cell cell, CellType? type)
        {
            if (cell == null) return null;
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
