using System;
using Sample.BusinessInterface.Rules;
using Sample.BusinessInterface.Domain;
using Sample.BusinessServer.DataAccess;
using SimpleLibrary.Rules;
using SimpleLibrary.ServiceModel;

namespace Sample.BusinessServer.Rules
{
    public partial class EmpresaRules : BaseRules<Empresa>, IEmpresaRules
    {
        public void TestRules()
        {
            IFuncionarioRules rules = RulesFactory.Create<IFuncionarioRules>();
            rules.Load(1);
        }
    }
}