using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Simple.DataAccess;
using Simple.Reflection;
using Simple.ConfigSource;
using System.Linq.Expressions;
using Simple.Expressions.Editable;
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

        public static R Service
        {
            get
            {
                return Do.GetService();
            }
        }

        public static R GetService(object key)
        {
            return EntityAccessor<T, R>.Do[key].GetService();
        }

        #region Expressions
        public static Expression<Func<T, bool>> Expr(bool value)
        {
            return x => value;
        }

        public static Expression<Func<T, bool>> Expr(Expression<Func<T, bool>> expr)
        {
            return expr;
        }

        public static Expression<Func<T, bool>> And(Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        {
            return PredicateBuilder.And(left, right);
        }

        public static Expression<Func<T, bool>> Or(Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        {
            return PredicateBuilder.Or(left, right);
        }


        #endregion

        protected static void EnsureThisType(Entity<T, R> entity)
        {
            if (!(entity is T)) throw new InvalidOperationException("Assertion failure. No idea what's happening. This must be T");
        }

        protected virtual T ThisAsT
        {
            get
            {
                EnsureThisType(this);
                return this as T;
            }
        }

        public virtual T Clone()
        {
            EnsureThisType(this);
            return (T)this.MemberwiseClone();
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
