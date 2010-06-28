using System;
namespace Simple.Generator.Console
{
    public interface IContextManager
    {
        void Execute(string command);
        IContext Get(string key);
        string[] Names { get; }
        string PromptContext { get; set; }
    }
}
