using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;

namespace SimpleLibrary.ServiceModel
{
    [ServiceContract]
    public interface ITestableService
    {
        [OperationContract]
        bool HeartBeat();
    }
}
