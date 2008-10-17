using System;
using System.Collections.Generic;
using System.Text;

namespace BasicLibrary.Configuration
{
    public class NopLocalizationProvider : ILocalizationProvider
    {
        public string GetLocalization(Type type)
        {
            return null;
        }
    }
}
