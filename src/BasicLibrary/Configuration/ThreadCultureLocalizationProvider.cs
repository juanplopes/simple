using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Threading;

namespace BasicLibrary.Configuration
{
    public class ThreadCultureLocalizationProvider : ILocalizationProvider
    {
        public string GetLocalization(Type type)
        {
            CultureInfo culture = Thread.CurrentThread.CurrentCulture;
            try
            {
                return new RegionInfo(culture.LCID).EnglishName;
            }
            catch { return null; }
        }
    }
}
