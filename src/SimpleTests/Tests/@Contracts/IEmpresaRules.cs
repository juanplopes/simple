using System.ServiceModel;
using System.Collections.Generic;
using Simple.Services;
using Simple.ServiceModel;

namespace Simple.Tests.Contracts
{
    [ServiceContract, MainContract]
    public partial interface IEmpresaRules : IEntityService<Empresa>
    {
        [OperationContract]
        Empresa GetByNameLinq(string name);
    }
}