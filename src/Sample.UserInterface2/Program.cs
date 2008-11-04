using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Sample.BusinessInterface;
using Sample.BusinessInterface.Domain;
using SimpleLibrary.Config;
using SimpleLibrary.Filters;
using SimpleLibrary.Rules;
using BasicLibrary.Configuration;
using BasicLibrary.Logging;
using System.IO;
using System.Diagnostics;
using SimpleLibrary.Threading;

namespace Sample.UserInterface2
{
    public class TestClass : SimpleInstanceState<TestClass>
    {
        public string SomeStringValue { get; set; }
        public int SomeIntValue { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            using (TestClass obj = TestClass.Get(123))
            {
                obj.SomeIntValue = 42;
                obj.SomeStringValue = "forty-two";
            }
       }
    }
}

