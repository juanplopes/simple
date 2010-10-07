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
        public bool HasValue { get; internal set; }

        public T Result { get; protected set; }
        public int Row { get; protected set; }
        internal IList<SheetError> PrivateErrors { get; private set; }
        public IEnumerable<SheetError> Errors { get { return HasValue ? PrivateErrors : new SheetError[0]; } }

        public RowResult() { PrivateErrors = new List<SheetError>(); }

        public RowResult(int row, T target)
            : this()
        {
            this.Row = row;
            this.Result = target;
        }
    }
}
