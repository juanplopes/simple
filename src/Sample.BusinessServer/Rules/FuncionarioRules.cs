using System;
using System.Collections.Generic;

using System.Text;
using Sample.BusinessInterface;
using Sample.BusinessInterface.Domain;
using SimpleLibrary.ServiceModel;
using Sample.BusinessServer.DataAccess;
using SimpleLibrary.DataAccess;
using SimpleLibrary.Rules;

namespace Sample.BusinessServer.Rules
{
    public class FuncionarioRules : BaseRules<Funcionario, FuncionarioDao>, IFuncionarioRules
    {

    }
}
