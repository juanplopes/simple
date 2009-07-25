using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Simple.Tests.Service
{
    public class IpcConstants
    {
        public const string Scheme = "ipc";
        public const string StartPort = "localserver";
    }

    [TestFixture, Ignore("Still not working =/")]
    public class IpcRemotingFactoryFixture : BaseRemotingFactoryFixture
    {
        public override Uri Uri
        {
            get { return Helper.MakeUri(IpcConstants.Scheme, IpcConstants.StartPort + 1); }
        }

    }

    [TestFixture, Ignore("Still not working =/")]
    public class IpcSelfRemotingFactoryFixture : BaseSelfRemotingFactoryFixture
    {
        public override Uri Uri
        {
            get { return Helper.MakeUri(IpcConstants.Scheme, IpcConstants.StartPort + 2); }
        }

    }

    [TestFixture, Ignore("Still not working =/")]
    public class IpcRemotingInterceptorFixture : BaseRemotingInterceptorFixture
    {
        public override Uri Uri
        {
            get { return Helper.MakeUri(IpcConstants.Scheme, IpcConstants.StartPort + 3); }
        }
    }

    [TestFixture, Ignore("Still not working =/")]
    public class IpcSelfRemotingInterceptorFixture : BaseSelfRemotingInterceptorFixture
    {
        public override Uri Uri
        {
            get { return Helper.MakeUri(IpcConstants.Scheme, IpcConstants.StartPort + 4); }
        }

    }

}
