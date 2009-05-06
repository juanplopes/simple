using System;
using System.Collections.Generic;
using System.Text;

namespace Simple.Configuration
{
    public interface ILocalizationProvider
    {
        string GetLocalization(Type type);
    }
}
