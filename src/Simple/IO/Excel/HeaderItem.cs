using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Reflection;
using System.Globalization;

namespace Simple.IO.Excel
{
    public class HeaderItem
    {
        private ISettableMemberInfo member = null;
        private string name = null;
        private IFormatProvider formatter = CultureInfo.InvariantCulture;


        public HeaderItem(ISettableMemberInfo member)
        {
            this.member = member;
            this.name = member.Name;
        }

        public HeaderItem Named(string name)
        {
            this.name = name;
            return this;
        }

        public HeaderItem Formatter(IFormatProvider culture)
        {
            this.formatter = culture;
            return this;
        }

        public HeaderItem Formatter(string culture)
        {
            return Formatter(CultureInfo.GetCultureInfo(culture));
        }

        public void Set(object target, object value)
        {
            if (member.Type.CanAssign(typeof(Enum)) && value is string)
                value = Enum.Parse(member.Type, value as string, true);
            else if (value is IConvertible)
                value = Convert.ChangeType(value, member.Type, formatter);

            member.Set(target, value );
        }
    }
}
