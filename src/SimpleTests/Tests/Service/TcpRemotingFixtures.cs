using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Simple.Tests.Service
{
    public class TcpConstants
    {
        public const string Scheme = "tcp";
        public const int StartPort = 5000;
    }

    [TestFixture]
    [Explicit("not stable")]
    public class TcpRemotingFactoryFixture : BaseRemotingFactoryFixture
    {
        public override Uri Uri
        {
            get { return Helper.MakeUri(TcpConstants.Scheme, TcpConstants.StartPort + 1); }
        }
    }

    [TestFixture]
    [Explicit("not stable")]
    public class TcpSelfRemotingFactoryFixture : BaseSelfRemotingFactoryFixture
    {
        public override Uri Uri
        {
            get { return Helper.MakeUri(TcpConstants.Scheme, TcpConstants.StartPort + 2); }
        }

    }

    [TestFixture]
    [Explicit("not stable")]
    public class TcpRemotingInterceptorFixture : BaseRemotingInterceptorFixture
    {
        public override Uri Uri
        {
            get { return Helper.MakeUri(TcpConstants.Scheme, TcpConstants.StartPort + 3); }
        }

    }

    [TestFixture]
    [Explicit("not stable")]
    public class TcpSelfRemotingInterceptorFixture : BaseSelfRemotingInterceptorFixture
    {
        public override Uri Uri
        {
            get { return Helper.MakeUri(TcpConstants.Scheme, TcpConstants.StartPort + 4); }
        }

    }

}
