using System;

namespace Simple.Tests.Services
{
    public class Helper
    {
        public static string MakeConfig(Uri uri)
        {
            return RemotingConfigs.SimpleRemotingAny
                .Replace("%%URL%%", uri.ToString());
        }

        public static Uri MakeUri(string scheme, int? port)
        {
            string uri = scheme + "://localhost";
            if (port != null)
            {
                uri += ":" + port.ToString();
            }
            return new Uri(uri);
        }

        public static Uri MakeUri(string scheme, string ipcPort)
        {
            return new Uri(scheme + "://" + ipcPort);
        }
    }
}
