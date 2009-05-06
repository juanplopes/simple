using System;
using System.Collections.Generic;
using System.Text;

namespace Simple.Configuration
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ConfigFilesIgnoreAttribute : Attribute
    {
    }
}
