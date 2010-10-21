using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Simple.IO.Excel
{
    [Serializable]
    public class RowResult<T>
    {
        public bool HasValue { get; set; }

        public T Result { get; protected set; }
        public int Row { get; protected set; }
        public IList<SheetError> Errors { get; protected set; }

        public RowResult()
        {
            this.Errors = new List<SheetError>();
        }

        public RowResult(int row, T target)
            : this()
        {
            this.Row = row;
            this.Result = target;
        }
    }
}
