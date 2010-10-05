using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Simple.Reflection
{
    [Serializable]
    public class PropertyInfoWrapper : ISettableMemberInfo
    {
        [NonSerialized]
        protected static MethodCache Cache = new MethodCache();

        public PropertyInfo InternalMember { get; protected set; }
        public MemberInfo Member { get { return InternalMember; } }

        public PropertyInfoWrapper(PropertyInfo property)
        {
            this.InternalMember = property;
        }


        public string Name
        {
            get { return InternalMember.Name; }
        }

        public void Set(object target, object value, params object[] index)
        {
            IEnumerable<object> args = new[] { value };
            if (index != null) args = args.Union(index);
            if (target != null && !target.GetType().CanAssign(InternalMember.DeclaringType))
                throw new TargetException("Expected {0}".AsFormat(InternalMember.DeclaringType.GetRealClassName()));

            Cache.GetSetter(InternalMember)(target, args.ToArray());
        }

        public void Set(object target, object value)
        {
            Set(target, value, null);
        }

        public object Get(object target, params object[] index)
        {
            return Cache.GetGetter(InternalMember)(target, index);
        }

        public object Get(object target)
        {
            return Get(target, null);
        }

        public Type Type
        {
            get { return InternalMember.PropertyType; }
        }

        public Type DeclaringType
        {
            get { return InternalMember.DeclaringType; }
        }

        public bool CanRead
        {
            get { return InternalMember.CanRead; }
        }

        public bool CanWrite
        {
            get { return InternalMember.CanWrite; }
        }

    }
}
