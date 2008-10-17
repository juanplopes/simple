using System;
using System.Collections.Generic;
using System.Text;
using BasicLibrary.Configuration;

namespace BasicLibrary.LibraryConfig
{
    public class ThreadingConfig : ConfigElement
    {
        [ConfigElement("defaultLockingProvider")]
        public SqlLockingElement DefaultLockingProvider { get; set; }
    }
}
