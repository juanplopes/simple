using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Reflection;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using log4net;
using System.Reflection;

namespace Simple.IO.Excel
{
    public class RowReader<T>
    {
        private static ILog log = Simply.Do.Log(MethodBase.GetCurrentMethod());

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

        public RowResult Read(int rowNum, Row row, int[] indexes)
        {
            log.DebugFormat("Reading row {0}", rowNum);

            var target = new RowResult { Result = Header.CreateInstance() };
            if (row == null)
            {
                log.DebugFormat("Row {0} was null, skipping", rowNum);
                return null;
            }

            for (int i = 0; i < indexes.Length; i++)
            {
                log.DebugFormat("Reading cell {0}", i);
                try
                {
                    var column = Header[i];
                    column.Set(target.Result, GetCellValue(row.GetCell(i), null));
                }
                catch (Exception e)
                {
                    log.Debug("There was an error, recording", e);
                    target.PrivateErrors.Add(new SheetError(row.RowNum, i, e.Message));
                }
            }
            return target;
        }

        public object GetCellValue(Cell cell, CellType? type)
        {
            log.DebugFormat("Reading cell value");
            if (cell == null)
            {
                log.DebugFormat("Cell value was null, skipping");
                return null;
            }
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
