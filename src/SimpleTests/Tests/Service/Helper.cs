using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Tests.Service
{
    public class Helper
    {
        public static string MakeConfig(int port)
        {
            return RemotingConfigs.SimpleRemotingAny.Replace("%%PORT%%", port.ToString());
        }
    }
}
