using System;
using System.Collections.Generic;

using System.Text;

namespace SimpleLibrary.Rules
{
    public interface IRulesProvider<T>
    {
        T Create();
    }
}
