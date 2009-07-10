using System;
using System.Collections.Generic;

using System.Text;

namespace SimpleLibrary.ServiceModel
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class ServiceEnableAttribute : Attribute
    {
    }
}
