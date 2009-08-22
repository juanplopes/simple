using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Simple.Expressions.Editable
{
    public static class ReflectionExtensions
    {
        public static string ToSerializableForm(this Type type)
        {
            return type.AssemblyQualifiedName;
        }

        public static Type FromSerializableForm(this Type type, string serializedValue)
        {
            return Type.GetType(serializedValue);
        }

        public static string ToSerializableForm(this MethodInfo method)
        {
            string serializableName = method.DeclaringType.AssemblyQualifiedName + Environment.NewLine;
            if (!method.IsGenericMethod)
            {
                serializableName += method.ToString();
            }
            else
            {
                serializableName += method.GetGenericMethodDefinition().ToString() + Environment.NewLine +
                    String.Join(Environment.NewLine, method.GetGenericArguments().Select(ty => ty.ToSerializableForm()).ToArray());
            }
            return serializableName;
        }

        public static MethodInfo FromSerializableForm(this MethodInfo methodInfo, string serializedValue)
        {
            string[] fullName = SplitString(serializedValue);
            string name = fullName[1];            
            MethodInfo method = (from m in Type.GetType(fullName[0]).GetMethods()
                                 where m.ToString() == name
                                 select m).First();

            if (method.IsGenericMethod)
            {
                method = method.MakeGenericMethod(fullName.Skip(2).Select(s => typeof(string).FromSerializableForm(s)).ToArray());
            }
            return method;

        }

        public static string ToSerializableForm(this MemberInfo member)
        {
            return member.DeclaringType.AssemblyQualifiedName + Environment.NewLine + member.ToString();
        }

        public static MemberInfo FromSerializableForm(this MemberInfo memberInfo, string serializedValue)
        {
            string[] fullName = SplitString(serializedValue);
            string name = fullName[1];
            MemberInfo member = (from m in Type.GetType(fullName[0]).GetMembers()
                                     where m.ToString() == name
                                     select m).First();
            return member;

        }

        public static string ToSerializableForm(this ConstructorInfo obj)
        {
            if (obj == null)
                return null;
            else
                return obj.DeclaringType.AssemblyQualifiedName + Environment.NewLine + obj.ToString();
        }

        public static ConstructorInfo FromSerializableForm(this ConstructorInfo obj, string serializedValue)
        {
            if (serializedValue == null)
                return null;
            else
            {
                string[] fullName = SplitString(serializedValue);
                string name = fullName[1];
                ConstructorInfo newObj = (from m in Type.GetType(fullName[0]).GetConstructors()
                                          where m.ToString() == name
                                          select m).First();
                return newObj;
            }
        }

        private static String[] SplitString(string str)
        {
            if (str.Contains(Environment.NewLine))
            {
                return str.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            }
            else
            {
                return str.Split(new string[] { "\n" }, StringSplitOptions.None);
            }

        }

    }
}
