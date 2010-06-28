using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Generator.Console
{
    public class ContextManager<C> : IContextManager
        where C : ContextBase, new()
    {
        private object NullKey = new object();
        IDictionary<object, IContext> contexts = new Dictionary<object, IContext>();

        LinkedList<string> stack = new LinkedList<string>();

        string contextFreeIdentifier;

        public ContextManager(string contextFreeIdentifier)
        {
            this.contextFreeIdentifier = contextFreeIdentifier;
            Push(null);
        }

        public ContextManager()
            : this("@")
        {

        }

        protected IContext Get(string key)
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

        public string[] ContextNames
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
                var context = (command.Trim().StartsWith(contextFreeIdentifier)) ? Root : Current;
                context.Execute(command);
            }
            catch (Exception e)
            {
                Simply.Do.Log(this).Fatal(e.Message, e);
            }
        }




        #region IContextManager Members

        public void Push(string key)
        {
            stack.AddLast(key);
        }

        public bool Pop()
        {
            stack.RemoveLast();
            return stack.Count > 0;

        }

        public IContext Current
        {
            get { return Get(stack.Last.Value); }
        }

        public IContext Root
        {
            get { return Get(stack.First.Value); }
        }

        #endregion

        #region IContextManager Members


        public IEnumerable<string> Stack
        {
            get { return stack; }
        }

        #endregion
    }
}
