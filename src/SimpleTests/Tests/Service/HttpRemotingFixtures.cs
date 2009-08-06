using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Simple.Tests.Service
{
    public class HttpConstants
    {
        public const string Scheme = "http";
        public const int StartPort = 4000;
    }

    [TestFixture]
    public class HttpRemotingFactoryFixture : BaseRemotingFactoryFixture
    {
        public override Uri Uri
        {
            get { return Helper.MakeUri(HttpConstants.Scheme, HttpConstants.StartPort + 1); }
        }

    }

    [TestFixture]
    public class HttpSelfRemotingFactoryFixture : BaseSelfRemotingFactoryFixture
    {
        public override Uri Uri
        {
            get { return Helper.MakeUri(HttpConstants.Scheme, HttpConstants.StartPort + 2); }
        }

    }

    [TestFixture]
    public class HttpRemotingServerInterceptorFixture : BaseRemotingServerInterceptorFixture
    {
        public override Uri Uri
        {
            get { return Helper.MakeUri(HttpConstants.Scheme, HttpConstants.StartPort + 3); }
        }
    }

    [TestFixture]
    public class HttpRemotingClientInterceptorFixture : BaseRemotingClientInterceptorFixture
    {
        public override Uri Uri
        {
            get { return Helper.MakeUri(HttpConstants.Scheme, HttpConstants.StartPort + 4); }
        }
    }

    [TestFixture]
    public class HttpSelfRemotingServerInterceptorFixture : BaseSelfRemotingServerInterceptorFixture
    {
        public override Uri Uri
        {
            get { return Helper.MakeUri(HttpConstants.Scheme, HttpConstants.StartPort + 5); }
        }

    }

    [TestFixture]
    public class HttpSelfRemotingClientInterceptorFixture : BaseSelfRemotingClientInterceptorFixture
    {
        public override Uri Uri
        {
            get { return Helper.MakeUri(HttpConstants.Scheme, HttpConstants.StartPort + 6); }
        }
    }


}
