using System;
using SimpleLibrary.Rules;
using SimpleLibrary.ServiceModel;
using System.ServiceModel;

namespace Teste.Ok
{
[ServiceContract, MainContract]
public partial interface IEmpresaFuncionarioRules : IBaseRules<EmpresaFuncionario>
{
}
[ServiceContract, MainContract]
public partial interface IEmpresaRules : IBaseRules<Empresa>
{
[OperationContract]
void TestRules(int a, out int b, ref int c);
}
[ServiceContract, MainContract]
public partial interface IFuncionarioRules : IBaseRules<Funcionario>
{
}
}
