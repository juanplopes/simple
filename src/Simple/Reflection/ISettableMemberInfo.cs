using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Simple.Reflection
{
    public interface ISettableMemberInfo : IProperty
    {
        MemberInfo Member { get; }
        Type DeclaringType { get; }

    }
}
