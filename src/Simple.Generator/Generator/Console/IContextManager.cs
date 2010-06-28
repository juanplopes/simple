using System;
using System.Collections.Generic;
namespace Simple.Generator.Console
{
    public interface IContextManager
    {
        void Push(string key);
        bool Pop();
        IEnumerable<string> Stack { get; }
        IContext Current { get; }
        IContext Root { get; }

        void Execute(string command);
        string[] ContextNames { get; }
    }
}
