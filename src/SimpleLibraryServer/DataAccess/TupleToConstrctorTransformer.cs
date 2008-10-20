using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Transform;
using System.Reflection;

namespace SimpleLibrary.DataAccess
{
    public class TupleToConstrctorTransformer : IResultTransformer
    {
        public Type ResultType { get; set; }
        public TupleToConstrctorTransformer(Type t)
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
