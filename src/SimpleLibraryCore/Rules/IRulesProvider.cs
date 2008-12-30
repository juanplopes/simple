using System;
using System.Collections.Generic;

using System.Text;
using SimpleLibrary.ServiceModel;

namespace SimpleLibrary.Rules
{
    public interface IRulesProvider<T>
        where T : ITestableService
    {
        T Create();
    }
}
