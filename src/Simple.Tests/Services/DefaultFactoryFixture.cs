using System;
using NUnit.Framework;
using SharpTestsEx;

namespace Simple.Tests.Services
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
