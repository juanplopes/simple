using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.IO.Excel
{
    public class SkippingHeaderItem : IHeaderItem
    {
        public IHeaderItem Formatter(string culture)
        {
            return this;
        }

        public IHeaderItem Formatter(IFormatProvider culture)
        {
            return this;
        }

        public IHeaderItem Named(string name)
        {
            return this;
        }

        public void Set(object target, object value)
        {
        }

    }
}
