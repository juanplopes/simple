using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.Services.Default;
using Simple.Services;

namespace Simple.Tests.Service
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
            Simply.Get(guid).Configure.DefaultHost();

            ConfigureSvcsWithoutHooks(guid);

            return guid;
        }

        protected override void Release(Guid guid)
        {
        }
    }
}
