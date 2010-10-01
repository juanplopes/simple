using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Simple.Services
{
    public class HttpIdentityInjector : BaseCallHook
    {
        public HttpIdentityInjector(CallHookArgs args) : base(args) { }
        public override void Before()
        {
            try
            {
                var ident = HttpContext.Current.User.Identity;

                SimpleContext.Get().Username = ident.IsAuthenticated ?
                    ident.Name : null;
            }
            catch (NullReferenceException)
            {
                Simply.Do.Log("NullReference skipped");
            }


        }
    }
}
