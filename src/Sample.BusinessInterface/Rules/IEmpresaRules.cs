using System;
using Sample.BusinessInterface.Domain;
using Simple.Rules;
using Simple.ServiceModel;
using System.ServiceModel;
using System.Collections.Generic;

namespace Sample.BusinessInterface.Rules
{
    [ServiceContract, MainContract]
    public partial interface IEmpresaRules : IBaseRules<Empresa>
    {
        [OperationContract]
        void TestRules(IList<Funcionario> a, out Funcionario b, ref int c);
    }
}