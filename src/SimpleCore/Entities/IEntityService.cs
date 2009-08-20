using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Simple.DataAccess;
using System.Linq.Expressions;
using Simple.Expressions;
using Simple.Services;

namespace Simple.Entities
{
    [ServiceContract]
    public interface IEntityService<T> : IService
    {
        [OperationContract]
        T Load(object id);

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
        Page<T> Paginate(OrderBy<T> order, int? skip, int? take);

        [OperationContract]
        Page<T> PaginateByFilter(EditableExpression filter, OrderBy<T> order, int? skip, int? take);

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
    }
}
