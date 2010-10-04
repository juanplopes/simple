using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.IO.Excel
{
    [Serializable]
    public class SheetResult<T>
    {
        public string Name { get; protected set; }
        public IEnumerable<T> Records { get; protected set; }
        public SheetResult(string name, IEnumerable<T> records)
        {
            this.Name = name;
            this.Records = records;
        }
    }
}
