using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sample.BusinessInterface;
using Sample.BusinessInterface.Domain;
using Sample.BusinessServer.DataAccess;
using SimpleLibrary.Rules;
using SimpleLibrary.ServiceModel;

namespace Sample.BusinessServer.Rules
{
    public class EmpresaFuncionarioRules :
        BaseRules<EmpresaFuncionario, EmpresaFuncionarioDao>,
        IEmpresaFuncionarioRules
    {
    }
}
