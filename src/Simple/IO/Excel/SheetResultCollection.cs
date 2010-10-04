using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.IO.Excel
{
    public class SheetResultCollection<T> : IEnumerable<SheetResult<T>>
    {
        private IList<SheetResult<T>> results = null;
        private IDictionary<string, SheetResult<T>> namedResults = null;

        public SheetResultCollection(IEnumerable<SheetResult<T>> results)
        {
            this.results = results.ToList();
            this.namedResults = this.results.ToDictionary(x => x.Name);
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
            return results[index];
        }

        public IEnumerator<SheetResult<T>> GetEnumerator()
        {
            return results.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return results.GetEnumerator();
        }
    }
}
