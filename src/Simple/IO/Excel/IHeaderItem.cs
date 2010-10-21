using System;
namespace Simple.IO.Excel
{
    public interface IHeaderItem
    {
        IHeaderItem Formatter(string culture);
        IHeaderItem Formatter(IFormatProvider culture);
        IHeaderItem Named(string name);

        bool Exportable { get; }
        string Name { get; }
        void Set(object target, object value);
        object Get(object target);
    }
}
