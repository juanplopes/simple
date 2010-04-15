using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Validation
{
    [Serializable]
    public class ValidationList : List<ValidationItem>
    {
        public ValidationList() : base() { }
        public ValidationList(IEnumerable<ValidationItem> items) : base(items) { }

        public bool IsValid
        {
            get { return this.Count == 0; }
        }
    }
}
