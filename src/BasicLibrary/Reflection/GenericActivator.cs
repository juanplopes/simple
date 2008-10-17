using System;
using System.Collections.Generic;
using System.Text;

namespace BasicLibrary.Reflection
{
    public class GenericActivator
    {
        public static object CreateInstance(Type type, Type[] parameterTypes, object[] constructor)
        {
            Type lobjRealType = type.MakeGenericType(parameterTypes);
            return Activator.CreateInstance(lobjRealType, constructor);
        }

        public static object CreateInstance(Type type, Type parameterType, object constructor)
        {
            return CreateInstance(type, new Type[] { parameterType }, new object[] { constructor });
        }

    }
}
