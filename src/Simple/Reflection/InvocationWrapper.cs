using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Simple.Reflection
{


    [Serializable]
    public class InvocationWrapper : ISettableMemberInfo
    {
        [NonSerialized]
        protected static MethodCache Cache = new MethodCache();

        public InvocationDelegate Function { get; protected set; }
        public MemberInfo Member { get { return Function.Method; } }

        public InvocationWrapper(InvocationDelegate func)
        {
            this.Function = func;
        }

        public string Name
        {
            get { return "(Invocation)"; }
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
            return Function(target, index);
        }

        public object Get(object target)
        {
            return Get(target, null);
        }

        public Type Type
        {
            get { return Function.Method.ReturnType; }
        }

        public Type DeclaringType
        {
            get { return Function.Method.DeclaringType; }
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
