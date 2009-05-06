using System;
using System.Collections.Generic;
using System.Text;

namespace Simple.Threading
{
    public interface ILockToken
    {
        bool ValidState { get; }
        string Type { get; }
        int Id { get; }
        int ConnectedClients { get; set; }
    }
}
