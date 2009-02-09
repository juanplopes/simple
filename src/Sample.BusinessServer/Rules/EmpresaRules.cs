using System;
using Sample.BusinessInterface.Rules;
using Sample.BusinessInterface.Domain;
using Sample.BusinessServer.DataAccess;
using SimpleLibrary.Rules;
using SimpleLibrary.ServiceModel;
using System.Collections.Generic;

namespace Sample.BusinessServer.Rules
{
    public partial class EmpresaRules : BaseRules<Empresa>, IEmpresaRules
    {
        public void TestRules(IList<Funcionario> a, out Funcionario b, ref int c)
        {
            IFuncionarioRules rules = RulesFactory.Create<IFuncionarioRules>();
            b = new Funcionario();
            rules.Load(1);
        }
    }
}