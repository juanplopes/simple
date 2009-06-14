using System;
using System.Collections.Generic;
using System.Text;

namespace Simple.Configuration2
{
    public class NopLocalizationProvider : ILocalizationProvider
    {
        public string GetLocalization(Type type)
        {
            return null;
        }
    }
}
