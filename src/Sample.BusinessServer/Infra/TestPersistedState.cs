using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleLibrary.Threading;
using BasicLibrary.Persistence;

namespace Sample.BusinessServer.Infra
{
    [Serializable]
    public class TestPersistedState : GenericInstanceState<TestPersistedState, NHLockingProvider, NHLockToken>
    {
        public string Ola { get; set; }
    }
}
