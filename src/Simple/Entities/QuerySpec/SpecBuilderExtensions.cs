using System;
using System.Linq;
using Simple.Entities.QuerySpec;

namespace Simple
{
    public static class SpecBuilderExtensions
    {
        public static SpecBuilder<T> ApplyFuncs<T>(this SpecBuilder<T> builder, params Func<SpecBuilder<T>, SpecBuilder<T>>[] options)
        {
            return options.Aggregate(builder, (x, func) => func(x));
        }
    }
}
