using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Simple.DataAccess;
using NHibernate.Mapping;
using Simple.Reflection;

namespace Simple.TestBase
{
    public class BaseEntityProvider : IEntityProvider
    {
        public Type EntityType { get; set; }
        protected string FactoryName { get; set; }
        public bool SkipID { get; set; }
        protected EntityHelper Helper { get; set; }
        public BaseEntityProvider(Type entityType, string factoryName, bool skipId)
        {
            FactoryName = factoryName;
            SkipID = skipId;
            EntityType = entityType;
            Helper = new EntityHelper(entityType);
            Helper.AddAllProperties();
        }

        public BaseEntityProvider(Type entityType, bool skipId)
            : this(entityType, null, skipId) { }

        public virtual object Populate(int seed)
        {
            return DefaultPopulate(new BaseTypeSeeder(), seed);
        }

        public virtual bool Compare(object e1, object e2)
        {
            return DefaultCompare(e1, e2);
        }

        protected object DefaultPopulate(ITypeSeeder seeder, int seed)
        {
            object e = Activator.CreateInstance(EntityType);
            Dictionary<string, bool> dic = new Dictionary<string, bool>();

            PersistentClass cls = SessionManagerOld.GetConfig(FactoryName).GetClassMapping(EntityType);
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

            foreach (PropertyInfo prop in EntityType.GetProperties())
            {
                if (!dic.ContainsKey(prop.Name))
                    prop.SetValue(e, seeder.GetValue(prop.PropertyType, seed), null);
            }

            return e;
        }

        protected bool DefaultCompare(object e1, object e2)
        {
            return Helper.ObjectEquals(e1, e2);
        }
    }
}
