using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Linq.Expressions;
using Simple.Expressions.Editable;
using Simple.Services;
using FluentValidation.Results;
using Simple.Validation;
using Simple.Entities.QuerySpec;
using Simple.Expressions;

namespace Simple.Entities
{
    [ServiceContract]
    public interface IEntityService<T> : IService
    {
        T Load(object id);
        T Refresh(T entity);
        T Reload(T entity);
        T Merge(T entity);
        T Evict(T entity);

        int Count(SpecBuilder<T> map);
        T Find(SpecBuilder<T> map);
        IList<T> List(SpecBuilder<T> map);
        IPage<T> List(SpecBuilder<T> map, SpecBuilder<T> reduce);

        IPage<T> Linq(LazyExpression<Func<IQueryable<T>, IQueryable<T>>> mapExpression, LazyExpression<Func<IQueryable<T>, IQueryable<T>>> reduceExpression);

        void Delete(object id);
        void Delete(T entity);
        int Delete(SpecBuilder<T> map);

        T SaveOrUpdate(T entity);
        T Save(T entity);
        T Update(T entity);
        T Persist(T entity);
        ValidationList Validate(T entity);
        ValidationList ValidateProperty(T entity, params string[] propName);

    }
}
