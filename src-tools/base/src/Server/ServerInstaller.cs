using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Hosting;
using System.ComponentModel;
using Simple;
using Sample.Project.Environment;
using Sample.Project.Config;

namespace Sample.Project
{
    [RunInstaller(true)]
    public class ServerInstaller : SimpleInstaller
    {
        public ServerInstaller()
        {
            var cfg = Simply.Do.GetConfig<ApplicationConfig>();
            ServiceName = cfg.Service.Name;
            DisplayName = cfg.Service.DisplayName;
        }
    }
}
