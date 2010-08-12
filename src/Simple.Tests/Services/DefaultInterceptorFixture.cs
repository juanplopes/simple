using System;
using NUnit.Framework;
using SharpTestsEx;

namespace Simple.Tests.Services
{
    [TestFixture]
    public class DefaultServerInterceptorFixture : DefaultInterceptorFixture
    {
        protected override Guid Configure()
        {
            Guid guid =  base.Configure();
            ConfigureServerHooks(guid);
            ConfigureClientServerHooks(guid);
            return guid;
        }
    }

    [TestFixture]
    public class DefaultClientInterceptorFixture : DefaultInterceptorFixture
    {
        protected override Guid Configure()
        {
            Guid guid = base.Configure();
            ConfigureClientHooks(guid);
            return guid;
        }
    }

    public abstract class DefaultInterceptorFixture : BaseInterceptorFixture
    {
        protected override Guid Configure()
        {
            Guid guid = Guid.NewGuid();
            Simply.Do[guid].Configure.DefaultHost();

            ConfigureSvcsWithoutHooks(guid);

            return guid;
        }

        protected override void Release(Guid guid)
        {
        }
       
    }
}
