using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleLibrary.DataAccess
{
    public class SimpleTransformers
    {
        public static TupleToConstrctorTransformer ByConstructor(Type t)
        {
            return new TupleToConstrctorTransformer(t);
        }
        public static TupleToConstrctorTransformer ByConstructor<T>()
        {
            return ByConstructor(typeof(T));
        }

        public static TupleToPropertiesTransformer ByProperties(Type t)
        {
            return new TupleToPropertiesTransformer(t);
        }
        public static TupleToPropertiesTransformer ByProperties<T>()
        {
            return ByProperties(typeof(T));
        }

    }

}
