using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Reflection;
using System.Globalization;

namespace Simple.IO.Excel
{
    public class HeaderItem : Simple.IO.Excel.IHeaderItem
    {
        private ISettableMemberInfo member = null;
        private string name = null;
        private IFormatProvider formatter = CultureInfo.InvariantCulture;


        public HeaderItem(ISettableMemberInfo member)
        {
            this.member = member;
            this.name = member.Name;
        }

        public IHeaderItem Named(string name)
        {
            this.name = name;
            return this;
        }

        public IHeaderItem Formatter(IFormatProvider culture)
        {
            this.formatter = culture;
            return this;
        }

        public IHeaderItem Formatter(string culture)
        {
            return Formatter(CultureInfo.GetCultureInfo(culture));
        }

        public void Set(object target, object value)
        {
            var type = member.Type.GetValueTypeIfNullable();
            if (type.CanAssign(typeof(Enum)) && value is string)
                value = Enum.Parse(type, value as string, true);
            else if (value is IConvertible)
                value = Convert.ChangeType(value, type, formatter);

            member.Set(target, value);
        }
    }
}
