using System;
using System.Collections.Generic;
using System.Text;
using NHibernate.Transform;

namespace Simple.Data
{
    public class TupleToDictionaryTransformer : IResultTransformer
    {
        #region IResultTransformer Members

        public System.Collections.IList TransformList(System.Collections.IList collection)
        {
            return collection;
        }

        public object TransformTuple(object[] tuple, string[] aliases)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

            if (tuple.Length != aliases.Length) throw new InvalidOperationException("Cannot transform entity mappings with this transformer");

            for (int i = 0; i < tuple.Length; i++)
            {
                dic[aliases[i]] = tuple[i];
            }

            return dic;
        }

        #endregion
    }
}
