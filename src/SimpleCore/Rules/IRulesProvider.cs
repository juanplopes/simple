using System;
using System.Collections.Generic;

using System.Text;
using Simple.ServiceModel;

namespace Simple.Rules
{
    public interface IRulesProvider<T>
        where T : ITestableService
    {
        T Create();
    }
}
