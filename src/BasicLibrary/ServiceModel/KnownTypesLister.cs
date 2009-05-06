using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Runtime.Serialization;
using Simple.Common;
using log4net;
using Simple.Logging;

namespace Simple.ServiceModel
{
    [global::System.AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public sealed class BasicKnownTypeAttribute : Attribute
    {

    }

    public class KnownTypesLister
    {
        protected static Dictionary<Assembly, IList<Type>> Cache { get; set; }
        protected static ILog Logger = MainLogger.Get(MethodInfo.GetCurrentMethod().DeclaringType);

        static KnownTypesLister()
        {
            Cache = new Dictionary<Assembly, IList<Type>>();
        }

        public static IList<Type> Locate(Assembly asm)
        {
            if (Cache.ContainsKey(asm)) return Cache[asm];

            IList<Type> list = new List<Type>(Enumerable.EnumerateN(
                DecoratedTypeFinder.Locate(asm, typeof(DataContractAttribute), true),
                DecoratedTypeFinder.Locate(asm, typeof(BasicKnownTypeAttribute), true)));

            return (Cache[asm] = list);
        }
    }
}
