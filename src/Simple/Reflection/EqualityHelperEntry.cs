using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Simple.Reflection
{
    public class EqualityHelperEntry : IEqualityComparer
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


        public bool Equals(object x, object y)
        {
            object value1 = Property.Get(x);
            object value2 = Property.Get(y);

            return Comparer.Equals(value1, value2);
        }

        public int GetHashCode(object obj)
        {
            var value = Property.Get(obj);
            return Comparer.GetHashCode(value);
        }
    }
}
