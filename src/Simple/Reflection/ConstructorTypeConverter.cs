using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Simple.Reflection
{
    public class ConstructorTypeConverter<T> : TypeConverter
    {
        ConversionConstructors ctors = new ConversionConstructors();
        MethodCache methodCache = new MethodCache();

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {

            var ctor = ctors.GetBest(typeof(T));
            var paramType = ctor.GetParameters().Single().ParameterType;
            return TypeDescriptor.GetConverter(paramType).CanConvertFrom(sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            var ctor = ctors.GetBest(typeof(T));
            var paramType = ctor.GetParameters().Single().ParameterType;

            if (!paramType.IsInstanceOfType(value))
                value = TypeDescriptor.GetConverter(paramType).ConvertFrom(value);

            return methodCache.GetInvoker(ctor)(null, value);
        }
    }
}
