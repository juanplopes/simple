using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Simple.Reflection
{
    public class ConversionConstructors : Dictionary<Type, ConstructorInfo>
    {
        public ConstructorInfo GetBest(Type t)
        {
            lock (this)
            {
                ConstructorInfo ctor = null;
                if (!this.TryGetValue(t, out ctor))
                {
                    var ctors = t.GetConstructors();
                    foreach (var c in ctors)
                    {
                        var args = c.GetParameters();
                        if (args.Length == 1 && args[0].ParameterType.CanAssign(typeof(IConvertible)))
                        {
                            this[t] = ctor = c;
                            break;
                        }
                    }
                }
                return ctor;
            }
        }
    }
}
