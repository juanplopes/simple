using System;
namespace Simple.IO.Excel
{
    public interface IHeaderItem
    {
        IHeaderItem Formatter(string culture);
        IHeaderItem Formatter(IFormatProvider culture);
        IHeaderItem Named(string name);
        void Set(object target, object value);
    }
}
