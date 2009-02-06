using System;
using Sample.BusinessInterface.Rules;
using Sample.BusinessInterface.Domain;
using Sample.BusinessServer.DataAccess;
using SimpleLibrary.Rules;
using SimpleLibrary.ServiceModel;

namespace Sample.BusinessServer.Rules
{
    public partial class EmpresaFuncionarioRules : BaseRules<EmpresaFuncionario>, IEmpresaFuncionarioRules
    {
    }

    public partial class EmpresaFuncionarioRules : SimpleLibrary.Rules.BaseRules<EmpresaFuncionario>
    {

    }
}