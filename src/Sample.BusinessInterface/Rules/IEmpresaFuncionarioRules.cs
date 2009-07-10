using System;
using Sample.BusinessInterface.Domain;
using SimpleLibrary.Rules;
using SimpleLibrary.ServiceModel;
using System.ServiceModel;

namespace Sample.BusinessInterface.Rules
{
    [ServiceContract, MainContract]
    public partial interface IEmpresaFuncionarioRules : IBaseRules<EmpresaFuncionario>
    {
    }
}