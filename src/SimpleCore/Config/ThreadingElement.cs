using System;
using System.Collections.Generic;

using System.Text;
using Simple.Configuration;

namespace Simple.Config
{
    public class ThreadingElement : ConfigElement
    {
        [ConfigElement("lockingProvider", Default=InstanceType.New)]
        public LockingProviderElement LockingProvider { get; set; }
    }
}
