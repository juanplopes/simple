using System;
using System.Collections.Generic;

using System.Text;
using NHibernate.Transform;
using System.Reflection;

namespace Simple.Data
{
    public class TupleToConstructorTransformer : IResultTransformer
    {
        public Type ResultType { get; set; }
        public TupleToConstructorTransformer(Type t)
        {
            ResultType = t;
        }

        public System.Collections.IList TransformList(System.Collections.IList collection)
        {
            return collection;
        }

        public object TransformTuple(object[] tuple, string[] aliases)
        {
            Type[] types = new Type[tuple.Length];
            for (int i = 0; i < tuple.Length; i++)
            {
                types[i] = tuple[i].GetType();
            }
            ConstructorInfo constructor = ResultType.GetConstructor(types);
            return constructor.Invoke(tuple);
        }
    }
}
