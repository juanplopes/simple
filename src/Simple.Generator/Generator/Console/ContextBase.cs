using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Generator.Console
{
    public abstract class ContextBase : MarshalByRefObject, IContext
    {
        GeneratorResolver resolver = null;
        protected abstract GeneratorResolver Configure(string name, bool defaultContext);

        public void Init(string name, bool defaultContext)
        {
            resolver = Configure(name, defaultContext);
        }


        public void Execute(string command)
        {
            try
            {
                resolver.Resolve(command).Execute();
            }
            catch (GeneratorException e)
            {
                Simply.Do.Log(this).Warn(e.Message);
            }
            catch (Exception e)
            {
                Simply.Do.Log(this).Error(e.Message, e);
            }
        }
    }
}
