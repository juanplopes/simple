using System;
using System.Collections.Generic;
using System.Text;
using Simple.DataAccess;
using Simple.Reflection;
using Simple.ConfigSource;
using System.Linq.Expressions;
using Simple.Expressions;

namespace Simple.Entities
{
    [Serializable]
    public class Entity<T, R>
        where T : Entity<T, R>
        where R : class, IEntityService<T>
    {
        public static EntityAccessor<T, R> Do
        {
            get
            {
                return EntityAccessor<T, R>.Do;
            }
        }

        public static R Service()
        {
            return Service(null);
        }

        public static R Service(object key)
        {
            return EntityAccessor<T, R>.Do[key].GetService();
        }

        protected static void EnsureThisType(Entity<T, R> entity)
        {
            if (!(entity is T)) throw new InvalidOperationException("Assertion failure. No idea what's happening.");
        }

        protected virtual T ThisAsT
        {
            get
            {
                EnsureThisType(this);
                return this as T;
            }
        }

        public virtual T Persist()
        {
            return Do.Persist(ThisAsT);
        }

        public virtual T Save()
        {
            return Do.Save(ThisAsT);
        }

        public virtual T Update()
        {
            return Do.Update(ThisAsT);
        }

        public virtual void Delete()
        {
            Do.Delete(ThisAsT);
        }

        public virtual T SaveOrUpdate()
        {
            return Do.SaveOrUpdate(ThisAsT);
        }
    }
}
