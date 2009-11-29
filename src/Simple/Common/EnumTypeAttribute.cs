using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Simple.Common
{
    [global::System.AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
    public sealed class EnumTypeAttribute : Attribute
    {
        public object Type { get; set; }

        public EnumTypeAttribute(object attribute)
        {
            this.Type = attribute;
        }

        public static IList GetEnumTypes(object enumValue)
        {
            try
            {
                EnumTypeAttribute[] attributes = (EnumTypeAttribute[])enumValue.GetType().GetField(Enum.GetName(enumValue.GetType(), enumValue)).GetCustomAttributes(typeof(EnumTypeAttribute), true);
                IList values = new ArrayList();
                foreach (EnumTypeAttribute attribute in attributes)
                {
                    values.Add(attribute.Type);
                }
                return values;
            }
            catch
            {
                return new ArrayList();
            }
        }

        public static bool IsDefined(object enumValue, object targetAttribute)
        {
            try
            {
                EnumTypeAttribute[] attributes = (EnumTypeAttribute[])enumValue.GetType().GetField(Enum.GetName(enumValue.GetType(), enumValue)).GetCustomAttributes(typeof(EnumTypeAttribute), true);
                IList values = new ArrayList();
                foreach (EnumTypeAttribute attribute in attributes)
                {
                    if (attribute.Type.Equals(targetAttribute)) return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }

}
