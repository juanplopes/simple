using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.IO;
using Simple.IO;
using SharpTestsEx;

namespace Simple.Tests.IO
{
    [TestFixture]
    public class ConsoleRedirectionFixture
    {
        [Test]
        public void CanRedirectConsoleToTextWriter()
        {
            var text = new StringWriter();
            using (new ConsoleRedirection(text))
                Console.WriteLine("teste");

            Console.WriteLine("abc");
            text.Flush();
            text.ToString().Should().Be("teste" + Environment.NewLine);
                
        }
    }
}
