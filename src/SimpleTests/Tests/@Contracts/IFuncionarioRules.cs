using System.ServiceModel;
using Simple.Services;
using Simple.ServiceModel;

namespace Simple.Tests.Contracts
{
    [ServiceContract, MainContract]
    public partial interface IFuncionarioRules : IEntityService<Funcionario>
    {
    }
}