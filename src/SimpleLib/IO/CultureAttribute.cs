using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace Simple.IO
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false)]
    public sealed class CultureAttribute : Attribute
    {
        public string CultureName { get; private set; }
        public CultureInfo Culture { get {return CultureInfo.GetCultureInfo(CultureName);}}

        public CultureAttribute(string cultureName)
        {
            CultureName = cultureName;
        }
    }
}
