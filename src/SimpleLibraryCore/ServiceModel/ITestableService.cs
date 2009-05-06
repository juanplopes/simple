using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;

namespace Simple.ServiceModel
{
    [ServiceContract]
    public interface ITestableService
    {
        [OperationContract]
        bool HeartBeat();
    }
}
