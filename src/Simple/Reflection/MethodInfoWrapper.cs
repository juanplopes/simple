using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Simple.Reflection
{


    [Serializable]
    public class MethodInfoWrapper : ISettableMemberInfo
    {
        [NonSerialized]
        protected static MethodCache Cache = new MethodCache();

        public MethodInfo InternalMember { get; protected set; }
        public MemberInfo Member { get { return InternalMember; } }
        public object[] Arguments { get; set; }

        public MethodInfoWrapper(MethodInfo method, params object[] args)
        {
            this.InternalMember = method;
            this.Arguments = args ?? new object[0];
        }

        public string Name
        {
            get { return InternalMember.Name; }
        }

        public void Set(object target, object value, params object[] index)
        {
            throw new NotSupportedException();
        }

        public void Set(object target, object value)
        {
            Set(target, value, null);
        }

        public object Get(object target, params object[] index)
        {
            index = Arguments.Union(index ?? new object[0]).ToArray();
            return Cache.GetInvoker(InternalMember)(target, index);
        }

        public object Get(object target)
        {
            return Get(target, null);
        }

        public Type Type
        {
            get { return InternalMember.ReturnType; }
        }

        public Type DeclaringType
        {
            get { return InternalMember.DeclaringType; }
        }

        public bool CanRead
        {
            get { return true; }
        }

        public bool CanWrite
        {
            get { return false; }
        }

    }
}
