using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Generator.Console
{
    public interface IContext
    {
        void Init(string name, bool defaultContext);
        void Execute(string command);
    }
}
