using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Reflection
{
    public interface IProperty
    {
        Type Type { get; }

        bool CanRead { get; }
        bool CanWrite { get; }

        void Set(object target, object value, params object[] index);
        void Set(object target, object value);

        object Get(object target, params object[] index);
        object Get(object target);
    }
}
