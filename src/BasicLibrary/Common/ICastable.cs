using System;
using System.Collections.Generic;
using System.Text;

namespace Simple.Common
{
    public interface ICastable
    {
        object Cast(Type type);
        bool IsNull { get; }
    }

    public interface ICastable<T> : ICastable
    {

    }
}
