using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Simple.Threading
{
    public class HttpContextProvider : GenericContextProvider
    {
        public HttpContextProvider()
            : base(() => HttpContext.Current.Items)
        {
        }
    }
}
