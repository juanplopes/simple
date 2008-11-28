using System;
using System.Collections.Generic;

using System.Text;
using NHibernate.Transform;
using System.Reflection;
using NHibernate;

namespace SimpleLibrary.DataAccess
{
    public class TupleToPropertiesTransformer : IResultTransformer
    {
        public Type ResultType { get; set; }
        public Dictionary<string, PropertyInfo> Properties { get; set; }
        public TupleToPropertiesTransformer(Type t)
        {
            ResultType = t;
            Properties = new Dictionary<string, PropertyInfo>(StringComparer.OrdinalIgnoreCase);
            foreach (PropertyInfo prop in t.GetProperties())
            {
                Properties[prop.Name] = prop;
            }
        }

        #region IResultTransformer Members

        public System.Collections.IList TransformList(System.Collections.IList collection)
        {
            return collection;
        }

        public object TransformTuple(object[] tuple, string[] aliases)
        {
            int bias;
            if (tuple.Length == aliases.Length) bias = 0;
            else if (tuple.Length - 1 == aliases.Length) bias = -1;
            else throw new InvalidOperationException("Unknown difference between tuple and aliases");

            object result = Activator.CreateInstance(ResultType);

            for (int i = 0; i < tuple.Length; i++)
            {
                object obj = tuple[i];
                if (obj != null && obj.GetType().IsAssignableFrom(ResultType))
                {
                    foreach (PropertyInfo prop in obj.GetType().GetProperties())
                    {
                        PropertyInfo target = Properties[prop.Name];
                        target.SetValue(result, prop.GetValue(obj, null), null);
                    }
                }
                else
                {
                    PropertyInfo target = Properties[aliases[i + bias]];
                    target.SetValue(result, obj, null);
                }
            }

            return result;
        }

        #endregion
    }
}
