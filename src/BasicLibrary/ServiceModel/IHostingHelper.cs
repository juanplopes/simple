using System;
using System.Collections.Generic;
using System.Text;

namespace BasicLibrary.ServiceModel
{
    public interface IHostingHelper : IDisposable
    {
        void Register(Type ptypServiceType);
        void Execute();
    }
}
