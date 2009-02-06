using System;
using Sample.BusinessInterface.Domain;
using SimpleLibrary.Rules;
using SimpleLibrary.ServiceModel;
using System.ServiceModel;

namespace Sample.BusinessInterface.Rules
{
    [ServiceContract, MainContract]
    public partial interface IEmpresaRules : IBaseRules<Empresa>
    {
        [OperationContract]
        void TestRules(int a, out int b, ref int c);
    }
}