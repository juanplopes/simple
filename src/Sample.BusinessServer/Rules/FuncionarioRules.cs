using System;
using Sample.BusinessInterface.Rules;
using Sample.BusinessInterface.Domain;
using Sample.BusinessServer.DataAccess;
using Simple.Rules;
using Simple.ServiceModel;

namespace Sample.BusinessServer.Rules
{
    public partial class FuncionarioRules : BaseRules<Funcionario>, IFuncionarioRules
    {
    }
}