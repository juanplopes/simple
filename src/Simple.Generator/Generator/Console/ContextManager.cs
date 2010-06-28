using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Generator.Console
{
    public class ContextManager<C> : Simple.Generator.Console.IContextManager
        where C : ContextBase, new()
    {
        public string PromptContext { get; set; }

        private object NullKey = new object();
        IDictionary<object, IContext> contexts = new Dictionary<object, IContext>();
        string contextFreeIdentifier;

        public ContextManager(string contextFreeIdentifier)
        {
            this.contextFreeIdentifier = contextFreeIdentifier;
        }

        public ContextManager()
            : this("@")
        {

        }

        public IContext Get(string key)
        {
            if (!contexts.ContainsKey(key ?? NullKey))
            {
                IContext ctx = null;
                if (key == null)
                {
                    ctx = new C();
                }
                else
                {
                    var domain = AppDomain.CreateDomain(key);
                    var type = typeof(C);
                    ctx = (C)domain.CreateInstanceAndUnwrap(type.Assembly.GetName().Name, type.FullName);
                }
                ctx.Init(key, key == null);
                return contexts[key ?? NullKey] = ctx;
            }

            return contexts[key ?? NullKey];
        }

        public string[] Names
        {
            get
            {
                return contexts.Keys.Select(x => (x == NullKey ? "<default>" : x.ToString())).ToArray();
            }
        }

        public void Execute(string command)
        {
            try
            {
                var context = (command.Trim().StartsWith(contextFreeIdentifier)) ? Get(null) : Get(PromptContext);
                context.Execute(command);
            }
            catch (Exception e)
            {
                Simply.Do.Log(this).Fatal(e.Message, e);
            }
        }

    }
}
