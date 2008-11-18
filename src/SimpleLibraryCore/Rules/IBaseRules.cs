using System;
using System.Collections.Generic;

using System.Text;
using System.ServiceModel;
using SimpleLibrary.Filters;
using SimpleLibrary.DataAccess;

namespace SimpleLibrary.Rules
{
    [ServiceContract]
    public interface IBaseRules<T>
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
        Page<T> PageByFilter(Filter filter, OrderByCollection order, int skip, int take);

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

        [OperationContract]
        object TestMethod(object obj);
    }
}
