
using Simple.ServiceModel;
using System.ServiceModel;
using Simple.Rules;
namespace Simple.Tests.Contracts
{
    [ServiceContract, MainContract]
    public partial interface IEmpresaFuncionarioRules : IBaseRules<EmpresaFuncionario>
    {
    }
}