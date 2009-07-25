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
    public class TcpRemotingFactoryFixture : BaseRemotingFactoryFixture
    {
        public override Uri Uri
        {
            get { return Helper.MakeUri(TcpConstants.Scheme, TcpConstants.StartPort + 1); }
        }
    }

    [TestFixture]
    public class TcpSelfRemotingFactoryFixture : BaseSelfRemotingFactoryFixture
    {
        public override Uri Uri
        {
            get { return Helper.MakeUri(TcpConstants.Scheme, TcpConstants.StartPort + 2); }
        }

    }

    [TestFixture]
    public class TcpRemotingInterceptorFixture : BaseRemotingInterceptorFixture
    {
        public override Uri Uri
        {
            get { return Helper.MakeUri(TcpConstants.Scheme, TcpConstants.StartPort + 3); }
        }

    }

    [TestFixture]
    public class TcpSelfRemotingInterceptorFixture : BaseSelfRemotingInterceptorFixture
    {
        public override Uri Uri
        {
            get { return Helper.MakeUri(TcpConstants.Scheme, TcpConstants.StartPort + 4); }
        }

    }

}
