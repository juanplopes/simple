using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Services
{
    public interface IControlledService : IService
    {
        bool HeartBeat();
        void BeforeCall(string operationName, object[] parameters);
        void AfterCall(string operationName, object[] parameters);
    }
}
