
using Simple.ServiceModel;
using System.ServiceModel;
using Simple.Services;
namespace Simple.Tests.Contracts
{
    [ServiceContract]
    public partial interface IEmpresaFuncionarioRules : IEntityService<EmpresaFuncionario>
    {
    }
}