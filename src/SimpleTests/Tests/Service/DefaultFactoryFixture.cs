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
        protected override Guid GetSource()
        {
            Guid guid = new Guid();
            DefaultHostSimply.Do.Configure(guid);
            Simply.Get(guid).Host(typeof(SimpleService));
            return guid;
        }

        protected override void ReleaseSource(Guid guid)
        {
            SourceManager.Do.Remove<DefaultHostConfig>(guid);
            SourceManager.Do.Remove<IServiceHostProvider>(guid);
            SourceManager.Do.Remove<IServiceClientProvider>(guid);
        }
    }
}
