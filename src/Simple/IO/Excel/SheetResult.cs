using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Simple.IO.Excel
{
    [Serializable]
    public class SheetResult<T>
    {
        public string Name { get; protected set; }

        internal IList<SheetError> PrivateErrors { get; private set; }

        public IList<RowResult<T>> Rows { get; protected set; }
        public IEnumerable<T> Records { get { return Rows.Where(x => x.HasValue).Select(x => x.Result); } }
        public IEnumerable<SheetError> Errors { get { return 
            PrivateErrors.Union(
            Rows.Where(x => x.HasValue).SelectMany(x => x.Errors)); } }

        public SheetResult(string name, IEnumerable<RowResult<T>> rows)
        {
            this.PrivateErrors = new List<SheetError>();
            this.Name = name;
            this.Rows = rows.ToList();
        }
    }
}
