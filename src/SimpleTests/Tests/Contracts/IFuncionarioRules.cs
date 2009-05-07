using System.ServiceModel;
using Simple.Rules;
using Simple.ServiceModel;

namespace Simple.Tests.Contracts
{
    [ServiceContract, MainContract]
    public partial interface IFuncionarioRules : IBaseRules<Funcionario>
    {
    }
}