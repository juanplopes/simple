using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using BasicLibrary.Logging;
using System.Runtime.Serialization;

namespace BasicLibrary.ServiceModel
{
    public class KnownTypesLister
    {
        protected static Dictionary<Assembly, IList<Type>> Cache { get; set; }
        static KnownTypesLister()
        {
            Cache = new Dictionary<Assembly, IList<Type>>();
        }


        public static IList<Type> Locate(Assembly asm)
        {
            if (Cache.ContainsKey(asm)) return Cache[asm];

            MainLogger.Default.Debug("Locating known types in " + asm.FullName + "...");

            IList<Type> types = new List<Type>();
            foreach (Type t in asm.GetTypes())
            {
                if (Attribute.IsDefined(t, typeof(DataContractAttribute)) && !t.IsGenericTypeDefinition && !t.IsAbstract)
                {
                    MainLogger.Default.Debug(">" + t.FullName);
                    types.Add(t);
                }
            }
            Cache[asm] = types;
            return types;
        }
    }
}
