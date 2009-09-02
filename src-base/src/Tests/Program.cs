using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Sample.Project.Environment.Test;
using Simple;

namespace Tests
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}

[SetUpFixture]
public class SetupFixture
{
    [SetUp]
    public void FixtureSetup()
    {
        Default.ConfigureServer();
        Simply.Do.InitServer(typeof(Server.ServerStarter).Assembly, false);
    }
}