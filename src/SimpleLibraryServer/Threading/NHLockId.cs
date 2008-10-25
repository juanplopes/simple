using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleLibrary.Threading
{
    public class NHLockId
    {
        public virtual string Type { get; set; }
        public virtual int Id { get; set; }
        public NHLockId(string type, int id)
        {
            this.Type = type;
            this.Id = id;
        }
        public NHLockId() { }

        public override bool Equals(object obj)
        {
            if (obj is NHLockId)
            {
                NHLockId id = (NHLockId)obj;
                return this.Id.Equals(id.Id) && this.Type.Equals(id.Type);
            }
            else return false;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode() * this.Type.GetHashCode();
        }
    }
}
