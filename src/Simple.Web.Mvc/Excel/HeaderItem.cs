using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Reflection;
using System.Globalization;

namespace Simple.Web.Mvc.Excel
{
    public class HeaderItem
    {
        private ISettableMemberInfo member = null;
        private string name = null;
        private IFormatProvider formatter = CultureInfo.InvariantCulture;


        public HeaderItem(ISettableMemberInfo member, string name)
        {
            this.member = member;
            this.name = name;
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
            if (member.Type == typeof(bool))
                value = Convert.ToBoolean(Convert.ToInt32(value));
            else if (value is IConvertible)
                value = Convert.ChangeType(value, member.Type, formatter);

            member.Set(target, value );
        }
    }
}
