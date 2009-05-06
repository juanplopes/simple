using System;
using Sample.BusinessInterface.Domain;
using Simple.Rules;
using Simple.ServiceModel;
using System.ServiceModel;

namespace Sample.BusinessInterface.Rules
{
    [ServiceContract, MainContract]
    public partial interface IEmpresaFuncionarioRules : IBaseRules<EmpresaFuncionario>
    {
    }
}