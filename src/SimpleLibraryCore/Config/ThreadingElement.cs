using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasicLibrary.Configuration;

namespace SimpleLibrary.Config
{
    public class ThreadingElement : ConfigElement
    {
        [ConfigElement("lockingProvider", Default=InstanceType.New)]
        public LockingProviderElement LockingProvider { get; set; }
    }
}
