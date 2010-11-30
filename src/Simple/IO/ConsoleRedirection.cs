using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Patterns;
using System.IO;

namespace Simple.IO
{
    public class ConsoleRedirection : DisposableAction
    {
        public ConsoleRedirection(TextWriter writer)
            : base(GetAction())
        {
            Console.SetOut(writer);
        }

        public static Action GetAction()
        {
            var console = Console.Out;
            return () => Console.SetOut(console);
        }
    }
}
