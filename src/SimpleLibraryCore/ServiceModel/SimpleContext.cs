using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasicLibrary.Persistence;
using System.Security.Principal;
using System.Web;

namespace SimpleLibrary.ServiceModel
{
    [Serializable]
    public class SimpleContext : ExecutionContext<SimpleContext>
    {
        public string ClientUsername { get; set; }
        public Dictionary<string, object> CustomData { get; set; }

        public bool PopulateFromHttpContext()
        {
            if (HttpContext.Current == null) return false;
            if (HttpContext.Current.User == null) return false;
            if (HttpContext.Current.User.Identity == null) return false;
            ClientUsername = HttpContext.Current.User.Identity.Name;
            return true;
        }

        public bool PopulateFromWindowsIdentity()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            if (identity == null) return false;
            ClientUsername = identity.Name;
            return true;
        }

        public override void Init()
        {
            base.Init();
            CustomData = new Dictionary<string, object>();
        }
    }
}
