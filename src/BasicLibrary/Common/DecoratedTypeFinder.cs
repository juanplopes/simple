using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using BasicLibrary.Logging;

namespace BasicLibrary.Common
{
    public static class DecoratedTypeFinder
    {
        public static IList<Type> Locate(Assembly assembly, Type attributeAttribute, bool mustBeConcrete)
        {
            MainLogger.Default.Debug("Locating types decorated with " + attributeAttribute.Name + " in " + assembly.GetName().Name + "...");

            IList<Type> types = new List<Type>();
            foreach (Type t in assembly.GetTypes())
            {
                if (Attribute.IsDefined(t, attributeAttribute) && !t.IsGenericTypeDefinition && (!mustBeConcrete || !t.IsAbstract))
                {
                    MainLogger.Default.Debug(">" + t.FullName);
                    types.Add(t);
                }
            }
            return types;
        }

    }
}
