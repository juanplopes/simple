using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace SimpleLibrary.DataAccess
{
    public class EntityTransformer<T>
    {
        protected IDictionary<string, PropertyDescriptionMapping> Mappings { get; set; }

        public EntityTransformer(IList<PropertyDescriptionMapping> mappings)
        {
            Mappings = new Dictionary<string, PropertyDescriptionMapping>();
            foreach (PropertyDescriptionMapping mapping in mappings)
            {
                Mappings[mapping.SourceProperty] = mapping;
            }
        }

        public IDictionary<string, object> ToDictionary(T entity)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            foreach (PropertyInfo info in typeof(T).GetProperties())
            {
                object value = info.GetValue(entity, null);
                if (value != null)
                {
                    if (Mappings.ContainsKey(info.Name))
                    {
                        PropertyDescriptionMapping mapping = Mappings[info.Name];
                        object entValue = value.GetType().GetProperty(mapping.DestProperty).GetValue(value, null);
                        dic[info.Name] = new MappedProperty(value, entValue);
                    }
                    else
                    {
                        dic[info.Name] = value;
                    }
                }
                else
                {
                    dic[info.Name] = null;
                }
            }

            return dic;
        }

        public IEnumerable<IDictionary<string, object>> ToEnumerableDictionary(IEnumerable<T> entities)
        {
            foreach (T entity in entities)
            {
                yield return ToDictionary(entity);
            }
        }
    }
}
