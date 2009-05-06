using System;
using System.Collections.Generic;
using System.Text;

namespace Simple.ServiceModel
{
    public interface IHostingHelper : IDisposable
    {
        void Register(Type ptypServiceType);
        void Execute();
    }
}
