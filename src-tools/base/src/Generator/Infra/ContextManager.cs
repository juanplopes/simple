using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample.Project.Generator.Infra
{
    public class ContextManager
    {
        public string PromptContext { get; set; }

        private object NullKey = new object();
        IDictionary<object, Context> contexts = new Dictionary<object, Context>();
        string contextFreeIdentifier;

        public ContextManager(string contextFreeIdentifier)
        {
            this.contextFreeIdentifier = contextFreeIdentifier;
        }

        public ContextManager()
            : this("@")
        {

        }

        public Context Get(string key)
        {
            if (!contexts.ContainsKey(key ?? NullKey))
            {
                Context ctx = null;
                if (key == null)
                {
                    ctx = new Context();
                }
                else
                {
                    var domain = AppDomain.CreateDomain(key);
                    var type = typeof(Context);
                    ctx = (Context)domain.CreateInstanceAndUnwrap(type.Assembly.GetName().Name, type.FullName);
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
                Context context = (command.Trim().StartsWith(contextFreeIdentifier)) ? Get(null) : Get(PromptContext);
                context.Execute(command);
            }
            catch (Exception e)
            {
                Console.WriteLine("FATAL: {0}", e.Message);
                Console.WriteLine(e.StackTrace);
            }
            Console.WriteLine();
        }

    }
}
