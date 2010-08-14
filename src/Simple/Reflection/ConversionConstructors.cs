using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Simple.Reflection
{
    public class ConversionConstructor
    {
        public Func<object, object> Converter { get; set; }
        public Type ExpectedType { get; set; }
    }

    public class ConversionConstructors : Dictionary<Type, ConversionConstructor>
    {
        protected MethodCache Cache = new MethodCache();

        public ConversionConstructor MakeConversionPlan(Type t)
        {
            ConversionConstructor plan = null;

            lock (this)
            {
                if (!this.TryGetValue(t, out plan))
                {
                    if (t.CanAssign(typeof(IConvertible)))
                        plan = IConvertiblePlan(t);
                    else
                    {
                        var ctors = t.GetConstructors();
                        var args = ctors.ToDictionary(x => x, x => x.GetParameters());

                        foreach (var ctor in ctors)
                        {
                            var ctorArgs = args[ctor];
                            if (ctorArgs.Length == 1 && ctorArgs[0].ParameterType.CanAssign(typeof(IConvertible)))
                                plan = ConstructorPlan(ctor, ctorArgs[0].ParameterType);
                        }

                        if (plan == null)
                        {
                            foreach (var ctor in ctors)
                            {
                                var ctorArgs = args[ctor];
                                if (ctorArgs.Length == 1)
                                    plan = ConstructorPlan(ctor, ctorArgs[0].ParameterType);
                            }
                        }
                    }
                }

            }
            return plan;
        }

        private ConversionConstructor ConstructorPlan(ConstructorInfo ctor, Type argType)
        {
            var method = Cache.GetInvoker(ctor);
            var converter = MakeConversionPlan(argType);
            return new ConversionConstructor()
            {
                Converter = x => method(null, converter.Converter(x)),
                ExpectedType = converter.ExpectedType
            };
        }

        private static ConversionConstructor IConvertiblePlan(Type t)
        {
            return new ConversionConstructor()
            {
                Converter = x => System.Convert.ChangeType(x, t),
                ExpectedType = t
            };
        }
    }
}
