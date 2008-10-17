using System;
using System.Collections.Generic;
using System.Text;

namespace BasicLibrary.Configuration
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ConfigFilesIgnoreAttribute : Attribute
    {
    }
}
