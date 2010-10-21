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

        public IList<SheetError> SheetErrors { get; private set; }

        public IList<RowResult<T>> Rows { get; protected set; }
        public IEnumerable<T> Records { get { return Rows.Where(x => x.HasValue).Select(x => x.Result); } }
        
        public IEnumerable<SheetError> Errors
        {
            get
            {
                return
                    SheetErrors.Union(
                    Rows.Where(x => x.HasValue).SelectMany(x => x.Errors));
            }
        }

        public SheetResult()
        {
            this.SheetErrors = new List<SheetError>();
        }

        public SheetResult(string name, IEnumerable<RowResult<T>> rows)
            : this()
        {
            this.Name = name;
            this.Rows = rows.ToList();
        }
    }
}
