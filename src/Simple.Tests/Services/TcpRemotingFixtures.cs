using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Simple.Tests.Services
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
    public class TcpRemotingServerInterceptorFixture : BaseRemotingServerInterceptorFixture
    {
        public override Uri Uri
        {
            get { return Helper.MakeUri(TcpConstants.Scheme, TcpConstants.StartPort + 3); }
        }

    }

    [TestFixture]
    [Explicit("not stable")]
    public class TcpRemotingClientInterceptorFixture : BaseRemotingClientInterceptorFixture
    {
        public override Uri Uri
        {
            get { return Helper.MakeUri(TcpConstants.Scheme, TcpConstants.StartPort + 4); }
        }

    }

    [TestFixture]
    [Explicit("not stable")]
    public class TcpSelfRemotingServerInterceptorFixture : BaseSelfRemotingServerInterceptorFixture
    {
        public override Uri Uri
        {
            get { return Helper.MakeUri(TcpConstants.Scheme, TcpConstants.StartPort + 5); }
        }

    }
    [TestFixture]
    [Explicit("not stable")]
    public class TcpSelfRemotingClientInterceptorFixture : BaseSelfRemotingClientInterceptorFixture
    {
        public override Uri Uri
        {
            get { return Helper.MakeUri(TcpConstants.Scheme, TcpConstants.StartPort + 6); }
        }

    } 

}
