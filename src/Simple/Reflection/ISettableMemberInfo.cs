using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Simple.Reflection
{
    public interface ISettableMemberInfo
    {
        MemberInfo Member { get; }
        string Name { get; }
        Type Type { get; }
        Type DeclaringType { get; }
        bool CanRead { get; }
        bool CanWrite { get; }


        void Set(object target, object value, params object[] index);
        void Set(object target, object value);

        object Get(object target, params object[] index);
        object Get(object target);
    }
}
