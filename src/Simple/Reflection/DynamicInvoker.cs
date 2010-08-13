using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Simple.Reflection
{
    public class DynamicInvoker
    {
        public Type Type { get; protected set; }
        protected MethodCache Cache { get; set; }
        public DynamicInvoker(Type type)
        {
            this.Type = type;
            this.Cache = new MethodCache();
        }

        public object Invoke(object target, string name, params object[] args)
        {
            return Cache.GetInvoker(FindBestMethod(name, null, args))(target, args);
        }

        protected MethodBase FindBestMethod(string name, BindingFlags? flags, params object[] args)
        {
            var methods = Type.GetMethods().Where(x => x.Name == name).ToArray();
            object state;

            return Type.DefaultBinder.BindToMethod(
                flags ?? BindingFlags.Default, methods, ref args,
                null, null, null, out state);

        }
    }
}
