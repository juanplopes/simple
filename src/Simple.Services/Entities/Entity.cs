using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Simple.DataAccess;
using Simple.Reflection;
using Simple.Config;
using System.Linq.Expressions;
using Simple.Expressions.Editable;
using Simple.Expressions;
using NHibernate.Validator.Engine;
using Simple.Patterns;

namespace Simple.Entities
{
    [Serializable]
    public class Entity<T> : Entity<T, IEntityService<T>>
        where T : class, IEntity<T>
    {

    }

    [Serializable]
    public partial class Entity<T, R> : IEntity<T>
        where T : class, IEntity<T>
        where R : class, IEntityService<T>
    {
        public static EntityHelper<T> Identifiers
        {
            get
            {
                return Singleton<EntityHelper<T>>.Instance;
            }
        }


        public override int GetHashCode()
        {
            if (Identifiers.HasIdentifiers)
                return Identifiers.ObjectGetHashCode(this);
            else
                return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (Identifiers.HasIdentifiers)
                return Identifiers.ObjectEquals(this, obj);
            else
                return base.Equals(obj);
        }

        public override string ToString()
        {
            if (Identifiers.HasIdentifiers)
                return Identifiers.ObjectToString(this);
            else
                return base.ToString();
        }

        #region Expressions
        public static string Prop<P>(Expression<Func<T, P>> expr)
        {
            return ExpressionHelper.GetMemberName(expr);
        }

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

        public virtual T Refresh()
        {
            return Service.Refresh(ThisAsT);
        }

        public virtual T Merge()
        {
            return Service.Merge(ThisAsT);
        }

        public virtual T Evict()
        {
            return Service.Evict(ThisAsT);
        }

        public virtual T Persist()
        {
            return Service.Persist(ThisAsT);
        }

        public virtual T Save()
        {
            return Service.Save(ThisAsT);
        }

        public virtual T Update()
        {
            return Service.Update(ThisAsT);
        }

        public virtual void Delete()
        {
            Service.Delete(ThisAsT);
        }

        public virtual T SaveOrUpdate()
        {
            return Service.SaveOrUpdate(ThisAsT);
        }

        public virtual IList<InvalidValue> Validate()
        {
            return Service.Validate(ThisAsT);
        }

        public virtual IList<InvalidValue> Validate(string propName)
        {
            return Service.ValidateProperty(propName, MethodCache.Do.GetGetter(typeof(T).GetProperty(propName))
                (this, null));
        }

        public virtual IList<InvalidValue> Validate<P>(Expression<Func<T, P>> expr)
        {
            return Service.ValidateProperty(ExpressionHelper.GetMemberName(expr), expr.Compile()(ThisAsT));
        }
    }
}
