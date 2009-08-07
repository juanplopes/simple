using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Services.Default;
using Simple.ConfigSource;
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

            Simply.Get(guid).Configure.DefaultHost();

            Simply.Get(guid).Host(typeof(SimpleService));
            return guid;
        }

        protected override void Release(Guid guid)
        {
            Simply.Get(guid).StopServer();
            
            Simply.Get(guid).Release.DefaultHost();
        }
    }
}
