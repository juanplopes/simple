using System;
using System.Collections.Generic;
using System.Text;

namespace Simple.Configuration2
{
    public interface ILocalizationProvider
    {
        string GetLocalization(Type type);
    }
}
