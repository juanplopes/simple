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
        private IFormatProvider formatter = CultureInfo.InvariantCulture;
        
        public bool Exportable { get { return true; } }
        public string Name { get; set; }

        public HeaderItem(ISettableMemberInfo member)
        {
            this.member = member;
            this.Name = member.Name;
        }

        public IHeaderItem Named(string name)
        {
            this.Name = name;
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

            if (type.CanAssign<Enum>() && value is string)
                value = Enum.Parse(type, value as string, true);
            if (type.CanAssign<DateTime>() && value is double)
                value = ConvertExcelCrazyness(value);
            if (!type.CanAssign<string>() && (value as string) == "")
                value = null;
            else if (value is IConvertible)
                value = Convert.ChangeType(value, type, formatter);

            member.Set(target, value);
        }

        private static object ConvertExcelCrazyness(object value)
        {
            var dValue = (double)value - 1;
            var date = new DateTime(1900, 1, 1).AddDays(dValue);

            for (int i = 1900; i <= date.Year; i += 100)
                if (i % 400 != 0 && date > new DateTime(i, 2, 28)) date = date.AddDays(-1);

            return date;
        }


        public object Get(object target)
        {
            try
            {
                return member.Get(target);
            }
            catch (NullReferenceException)
            {
                return member.Type.GetBoxedDefaultInstance();
            }
        }

    }
}
