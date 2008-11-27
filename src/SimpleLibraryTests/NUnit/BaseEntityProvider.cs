using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using SimpleLibrary.DataAccess;
using NHibernate.Mapping;

namespace SimpleLibrary.NUnit
{
    public class BaseEntityProvider<T> : IEntityProvider<T>
        where T : new()
    {
        protected string SessionName { get; set; }
        public bool SkipID { get; set; }

        protected BaseEntityProvider(string sessionName, bool skipId)
        {
            SessionName = sessionName;
            SkipID = skipId;
        }

        protected BaseEntityProvider(bool skipId)
            : this(null, skipId) { }

        public virtual T Populate(int seed)
        {
            return DefaultPopulate(new BaseTypeSeeder(), seed);
        }

        public virtual bool Compare(T e1, T e2)
        {
            return DefaultCompare(e1, e2);
        }

        protected T DefaultPopulate(ITypeSeeder seeder, int seed)
        {
            T e = new T();
            Dictionary<string, bool> dic = new Dictionary<string, bool>();

            PersistentClass cls = SessionManager.GetConfig(SessionName).GetClassMapping(typeof(T));
            if (SkipID)
            {
                if (cls.Identifier is Component)
                {
                    foreach (Property prop in ((Component)cls.Identifier).PropertyIterator)
                    {
                        dic[prop.Name] = true;
                    }
                }
                else
                {
                    dic[cls.IdentifierProperty.Name] = true;
                }
            }

            foreach (PropertyInfo prop in typeof(T).GetProperties())
            {
                if (!dic.ContainsKey(prop.Name))
                    prop.SetValue(e, seeder.GetValue(prop.PropertyType, seed), null);
            }

            return e;
        }

        protected bool DefaultCompare(T e1, T e2)
        {
            foreach (PropertyInfo prop in typeof(T).GetProperties())
            {
                object v1 = prop.GetValue(e1, null);
                object v2 = prop.GetValue(e2, null);

                if ((v1 == null || v2 == null) && v1 != v2) return false;
                if (v1 != null && v2 != null)
                {
                    if (!v1.Equals(v2)) return false;
                }
            }
            return true;
        }
    }
}
