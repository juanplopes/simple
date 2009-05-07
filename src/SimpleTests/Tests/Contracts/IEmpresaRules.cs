using System.ServiceModel;
using System.Collections.Generic;
using Simple.Rules;
using Simple.ServiceModel;

namespace Simple.Tests.Contracts
{
    [ServiceContract, MainContract]
    public partial interface IEmpresaRules : IBaseRules<Empresa>
    {
    }
}