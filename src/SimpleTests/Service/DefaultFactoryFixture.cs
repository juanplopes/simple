using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Services.Default;
using Simple.Config;
using Simple.Services;
using NUnit.Framework;

namespace Simple.Tests.Service
{
    [TestFixture]
    public class DefaultFactoryFixture : BaseFactoryFixture
    {
        protected override Guid Configure()
        {
            Guid guid = new Guid();

            Simply.Do[guid].Configure.DefaultHost();

            Simply.Do[guid].Host(typeof(SimpleService));
            return guid;
        }

        protected override void Release(Guid guid)
        {
            Simply.Do[guid].StopServer();
            
            Simply.Do[guid].Release.DefaultHost();
        }
    }
}
