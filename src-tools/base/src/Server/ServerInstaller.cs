using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Hosting;
using System.ComponentModel;

namespace Sample.Project
{
    [RunInstaller(true)]
    public class ServerInstaller : SimpleInstaller
    {
        public const string MainServiceName = "sampleprojectsvc";
        public ServerInstaller()
        {
            ServiceName = MainServiceName;
            DisplayName = "SampleProject Business Server";
        }
    }
}
