using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Simple.Reflection
{
    public class EqualityHelperEntry
    {
        public IProperty Property { get; set; }
        public IEqualityComparer Comparer { get; set; }

        public EqualityHelperEntry(IProperty property)
            : this(property, null)
        {
        }
        public EqualityHelperEntry(IProperty property, IEqualityComparer comparer)
        {
            Property = property;
            Comparer = comparer ?? EqualityComparer<object>.Default;
        }
        public override string ToString()
        {
            return Property.Name;
        }

    }
}
