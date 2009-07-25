using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.Services.Default;

namespace Simple.Tests.Service
{
    [TestFixture]
    public class DefaultInterceptorFixture : BaseInterceptorFixture
    {
        protected override Guid Configure()
        {
            Guid guid = Guid.NewGuid();
            DefaultHostSimply.Do.Configure(guid);
            Simply.Get(guid).Host(typeof(TestService), new Interceptor());
            return guid;
        }

        protected override void Release(Guid guid)
        {
        }
    }
}
