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
        public ReadOnlyCollection<T> Records { get; protected set; }
        public ReadOnlyCollection<SheetError> Errors { get; protected set; }

        public SheetResult(string name, IEnumerable<T> records, IEnumerable<SheetError> errors)
        {
            this.Name = name;
            this.Records = new ReadOnlyCollection<T>(records.ToList());
            this.Errors = new ReadOnlyCollection<SheetError>(errors.ToList());
        }
    }
}
