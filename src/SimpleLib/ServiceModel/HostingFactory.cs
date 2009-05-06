using System;
using System.Collections.Generic;
using System.Text;

namespace Simple.ServiceModel
{
    public class HostingFactory
    {
        public static IHostingHelper Create()
        {
            return new WCFHostingHelper();
        }
    }
}
