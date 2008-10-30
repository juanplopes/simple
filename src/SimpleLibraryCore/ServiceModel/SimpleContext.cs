using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasicLibrary.Persistence;
using System.Security.Principal;
using System.Web;
using System.Globalization;
using System.Threading;

namespace SimpleLibrary.ServiceModel
{
    [Serializable]
    public class SimpleContext : ExecutionContext<SimpleContext>
    {
        public string ClientHttpUsername { get; protected set; }
        public string ClientWindowsUsername { get; protected set; }
        public string ClientThreadCulture { get; protected set; }
        public string ClientUICulture { get; protected set; }
        
        public Dictionary<string, object> CustomData { get; set; }

        protected bool PopulateFromHttpContext()
        {
            if (HttpContext.Current == null) return false;
            if (HttpContext.Current.User == null) return false;
            if (HttpContext.Current.User.Identity == null) return false;
            ClientHttpUsername = HttpContext.Current.User.Identity.Name;
            return true;
        }

        protected bool PopulateFromWindowsIdentity()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            if (identity == null) return false;
            ClientWindowsUsername = identity.Name;
            return true;
        }

        public bool PopulateCultures()
        {
            if (Thread.CurrentThread.CurrentCulture != null)
                ClientThreadCulture = Thread.CurrentThread.CurrentCulture.Name;
            if (Thread.CurrentThread.CurrentUICulture != null)
                ClientUICulture = Thread.CurrentThread.CurrentUICulture.Name;

            return true;
        }

        public void Refresh(bool isClientSide)
        {
            if (isClientSide)
            {
                PopulateFromHttpContext();
                PopulateFromWindowsIdentity();
                PopulateCultures();
            }
        }

        public override void Init()
        {
            base.Init();
            CustomData = new Dictionary<string, object>();
        }
    }
}
