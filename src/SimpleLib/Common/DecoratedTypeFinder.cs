using System;
using System.Collections.Generic;

using System.Text;
using System.Reflection;
using Simple.Logging;
using log4net;

namespace Simple.Common
{
    public static class DecoratedTypeFinder
    {
        private static ILog Logger = SimpleLogger.Get(MethodInfo.GetCurrentMethod().DeclaringType);

        public static IList<Type> Locate(Assembly assembly, Type attributeAttribute, bool mustBeConcrete)
        {
            Logger.Debug("Locating types decorated with " + attributeAttribute.Name + " in " + assembly.GetName().Name + "...");

            IList<Type> types = new List<Type>();
            foreach (Type t in assembly.GetTypes())
            {
                if (Attribute.IsDefined(t, attributeAttribute) && !t.IsGenericTypeDefinition && (!mustBeConcrete || !t.IsAbstract))
                {
                    Logger.Debug(">" + t.FullName);
                    types.Add(t);
                }
            }
            return types;
        }

    }
}
