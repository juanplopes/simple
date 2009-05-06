using System;
using System.Collections.Generic;

using System.Text;

namespace Simple.ServiceModel
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class ServiceEnableAttribute : Attribute
    {
    }
}
