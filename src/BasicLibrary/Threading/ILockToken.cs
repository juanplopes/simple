using System;
using System.Collections.Generic;
using System.Text;

namespace BasicLibrary.Threading
{
    public interface ILockToken
    {
        bool ValidState { get; }
        string Type { get; }
        int Id { get; }
        int ConnectedClients { get; set; }
    }
}
