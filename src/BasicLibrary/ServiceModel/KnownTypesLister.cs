using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Runtime.Serialization;
using BasicLibrary.Common;
using log4net;
using BasicLibrary.Logging;

namespace BasicLibrary.ServiceModel
{
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

            IList<Type> list = DecoratedTypeFinder.Locate(asm, typeof(DataContractAttribute), true);

            return (Cache[asm] = list);
        }
    }
}
