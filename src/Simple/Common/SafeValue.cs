using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Common
{
    public class SafeValue<T>
    {
        public bool Found { get; protected set; }
        public T Value { get; protected set; }

        public object ObjectValue
        {
            get
            {
                if (Found)
                    return Value;
                else
                    return null;
            }
        }

        public SafeValue(T value, bool found)
        {
            this.Value = value;
            this.Found = found;
        }

        public static implicit operator T(SafeValue<T> value)
        {
            return value.Value;
        }

        public override bool Equals(object obj)
        {
            return EqualityComparer<object>.Default.Equals(Value, obj);
        }

        public override int GetHashCode()
        {
            return EqualityComparer<object>.Default.GetHashCode(Value);
        }

        public override string ToString()
        {
            return Convert.ToString(Value);
        }

    }
}
