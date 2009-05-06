using System;
using System.Collections.Generic;
using System.Text;

namespace Simple.Threading
{
    public abstract class BaseLockToken : ILockToken
    {
        public int ConnectedClients { get; set; }
        public string Type { get; set; }
        public int Id { get; set; }

        public abstract bool ValidState { get; }

        public BaseLockToken(string type, int id)
        {
            this.Type = type;
            this.Id = id;
            ConnectedClients = 0;
        }
    }
}
