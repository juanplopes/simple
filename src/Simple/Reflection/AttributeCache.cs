using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Simple.Reflection
{
    public class AttributeCache
    {
        protected Dictionary<MemberInfo, Dictionary<Type, IList<Attribute>>> Cache =
            new Dictionary<MemberInfo, Dictionary<Type, IList<Attribute>>>();
        protected AttributeCache() { }

        protected class Nested
        {
            public static AttributeCache Instance = new AttributeCache();
        }
        public static AttributeCache Do { get { return Nested.Instance; } }

        public IEnumerable<T> Enumerate<T>(MemberInfo member, bool inherit)
            where T : Attribute
        {
            lock (Cache)
            {
                Dictionary<Type, IList<Attribute>> l2ndLvlCache;
                bool couldGet2 = Cache.TryGetValue(member, out l2ndLvlCache);

                if (!couldGet2)
                {
                    l2ndLvlCache = Cache[member] = new Dictionary<Type, IList<Attribute>>();
                }

                IList<Attribute> l3rdLvlCache;
                bool couldGet3 = l2ndLvlCache.TryGetValue(typeof(T), out l3rdLvlCache);

                if (!couldGet3)
                {
                    l3rdLvlCache = l2ndLvlCache[typeof(T)] = new List<Attribute>(
                        (Attribute[])member.GetCustomAttributes(typeof(T), true));
                }

                return l3rdLvlCache.Select(x => (T)x);
            }
        }

        public IEnumerable<T> Enumerate<T>(MemberInfo member)
            where T : Attribute
        {
            return Enumerate<T>(member, true);
        }

        public T First<T>(MemberInfo member, bool inherit)
            where T : Attribute
        {
            return Enumerate<T>(member, inherit).FirstOrDefault();
        }

        public T First<T>(MemberInfo member)
            where T : Attribute
        {
            return First<T>(member, true);
        }

    }
}
