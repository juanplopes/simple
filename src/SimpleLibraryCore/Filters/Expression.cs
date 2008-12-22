using System;
using System.Collections.Generic;

using System.Text;
using SimpleLibrary.Filters;
using System.Runtime.Serialization;

namespace SimpleLibrary.Filters
{
    [DataContract]
    public abstract class Expression : Filter
    {
    }
}
