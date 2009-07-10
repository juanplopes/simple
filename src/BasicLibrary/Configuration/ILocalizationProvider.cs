using System;
using System.Collections.Generic;
using System.Text;

namespace BasicLibrary.Configuration
{
    public interface ILocalizationProvider
    {
        string GetLocalization(Type type);
    }
}
