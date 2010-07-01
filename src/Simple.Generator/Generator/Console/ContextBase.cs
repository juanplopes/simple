using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace Simple.Generator.Console
{
    public abstract class ContextBase : MarshalByRefObject
    {
        CommandResolver resolver = null;
        protected abstract CommandResolver Configure();
        private ILog logger = null;

        protected bool OverrideLogConfig { get { return true; } }
        protected string ProjectText { get; set; }

        protected ContextBase(string projectText)
        {
            ProjectText = projectText;
            Init();
        }

        protected void Init()
        {
            this.logger = Simply.Do.Log(this);

            resolver = Configure();
            if (OverrideLogConfig)
                Simply.Do.Configure.Log4net().FromXmlString(DefaultConfig.Log4net);

            logger.InfoFormat("Simple.Net v{0} [{1}]", Simply.Do.GetVersion(), ProjectText);
        }

        public void Execute(string command)
        {
            try
            {
                resolver.Resolve(command).Execute();
            }
            catch (ParserException e)
            {
                logger.Warn(e.Message);
            }
            catch (Exception e)
            {
                logger.Error(e.Message, e);
            }
        }
    }
}
