using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Simple.IO.Excel
{
    [Serializable]
    public class SheetResultCollection<T> : IEnumerable<SheetResult<T>>
    {
        public IList<SheetResult<T>> Sheets { get; protected set; }
        private IDictionary<string, SheetResult<T>> namedResults = null;

        public SheetResultCollection()
        {

        }

        public SheetResultCollection(IEnumerable<SheetResult<T>> results)
        {
            this.Sheets = results.ToList();
            RefreshNames();
        }

        public void RefreshNames()
        {
            this.namedResults = this.Sheets.ToDictionary(x => x.Name);
        }

        public SheetResult<T> this[string name]
        {
            get { return GetSheet(name); }
        }

        public SheetResult<T> GetSheet(string name)
        {
            return namedResults[name];
        }

        public SheetResult<T> this[int index]
        {
            get { return GetSheet(index); }
        }

        public SheetResult<T> GetSheet(int index)
        {
            return Sheets[index];
        }

        public IEnumerator<SheetResult<T>> GetEnumerator()
        {
            return Sheets.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Sheets.GetEnumerator();
        }
    }
}
