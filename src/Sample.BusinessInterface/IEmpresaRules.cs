using System;
using System.Collections.Generic;

using System.Text;
using System.ServiceModel;
using Sample.BusinessInterface.Domain;
using SimpleLibrary.Rules;
using SimpleLibrary.ServiceModel;
using SimpleLibrary.DataAccess;

namespace Sample.BusinessInterface
{
    [ServiceContract]
    [MainContract]
    public interface IEmpresaRules : IBaseRules<Empresa>
    {
        [OperationContract]
        IList<Empresa> GetAllWithQuery();

        [OperationContract]
        IList<Empresa> GetAllWithSQLQuery();

        [OperationContract]
        IList<Empresa> GetAllWithCriteria();

        [OperationContract]
        Empresa GetOne();
    }
}
