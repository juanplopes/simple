using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Simple.Entities.QuerySpec;
using Simple.Expressions.Editable;
using Simple.Services;
using Simple.Validation;

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

        int Delete(object id);
        int Delete(T entity);
        int Delete(SpecBuilder<T> map);

        T SaveOrUpdate(T entity);
        T Save(T entity);
        T Update(T entity);
        ValidationList Validate(T entity);
        ValidationList ValidateProperty(T entity, params string[] propName);

    }
}
