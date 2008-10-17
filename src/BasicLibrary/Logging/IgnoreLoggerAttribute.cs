using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasicLibrary.Logging
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method, Inherited=true)]
    public sealed class IgnoreLoggerAttribute : Attribute
    {
    }
}
