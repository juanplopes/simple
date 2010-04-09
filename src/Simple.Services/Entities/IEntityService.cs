using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Linq.Expressions;
using Simple.Expressions.Editable;
using Simple.Services;
using NHibernate.Validator.Engine;

namespace Simple.Entities
{
    [ServiceContract]
    public interface IEntityService<T> : IService
    {
        [OperationContract]
        T Load(object id);

        [OperationContract]
        T Refresh(T entity);

        [OperationContract]
        T Reload(T entity);

        [OperationContract]
        T Merge(T entity);

        [OperationContract]
        T Evict(T entity);
        
        [OperationContract]
        T FindByFilter(EditableExpression filter, OrderBy<T> orderBy);

        [OperationContract]
        IList<T> List(OrderBy<T> order);

        [OperationContract]
        IList<T> ListByFilter(EditableExpression filter, OrderBy<T> order);

        [OperationContract]
        int Count();

        [OperationContract]
        int CountByFilter(EditableExpression filter);

        [OperationContract]
        IPage<T> Paginate(OrderBy<T> order, int? skip, int? take);

        [OperationContract]
        IPage<T> PaginateByFilter(EditableExpression filter, OrderBy<T> order, int? skip, int? take);

        [OperationContract]
        IPage<T> PaginateWithLinq(EditableExpression mapExpression, EditableExpression reduceExpression);

        [OperationContract]
        void DeleteById(object id);

        [OperationContract]
        int DeleteByFilter(EditableExpression filter);

        [OperationContract]
        T SaveOrUpdate(T entity);

        [OperationContract]
        T Save(T entity);

        [OperationContract]
        T Update(T entity);

        [OperationContract]
        T Persist(T entity);

        [OperationContract]
        void Delete(T entity);

        [OperationContract]
        IList<InvalidValue> Validate(T entity);

        [OperationContract]
        IList<InvalidValue> ValidateProperty(string propName, object value);

    }
}
