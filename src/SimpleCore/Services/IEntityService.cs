using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Simple.DataAccess;
using Simple.Filters;

namespace Simple.Services
{
    [ServiceContract]
    public interface IEntityService<T> : IService
    {
        [OperationContract]
        T LoadByExample(T example);

        [OperationContract]
        T Load(object id);

        [OperationContract]
        T LoadByFilter(Filter filter);

        [OperationContract]
        IList<T> ListAll(OrderByCollection order);

        [OperationContract]
        IList<T> ListByExample(T example);

        [OperationContract]
        IList<T> ListByFilter(Filter filter, OrderByCollection order);

        [OperationContract]
        int CountByFilter(Filter filter);

        [OperationContract]
        Page<T> PaginateByFilter(Filter filter, OrderByCollection order, int skip, int take);

        [OperationContract]
        void SaveOrUpdate(T entity);

        [OperationContract]
        void Save(T entity);

        [OperationContract]
        void Update(T entity);

        [OperationContract]
        T Persist(T entity);

        [OperationContract]
        void Delete(T entity);

        [OperationContract]
        void DeleteById(object id);

        [OperationContract]
        int DeleteByFilter(Filter filter);
    }
}
