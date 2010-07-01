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
            try
            {
                resolver = Configure();
                ConfigureLogging();
            }
            catch (Exception e)
            {
                ConfigureLogging();
                logger.WarnFormat("Failed to configure.", e);
            }
            logger.InfoFormat("Simple.Net v{0} [{1}]", Simply.Do.GetVersion(), ProjectText);
        }

        private void ConfigureLogging()
        {
            try
            {
                if (OverrideLogConfig)
                    Simply.Do.Configure.Log4net().FromXmlString(DefaultConfig.Log4net);
            }
            catch(Exception e)
            {
                System.Console.WriteLine("Error configuring logging: {0}", e.Message);
            }
        }

        public virtual void Execute(string command, bool continueOnError)
        {
            try
            {
                resolver.Resolve(command).Execute();
            }
            catch (ParserException e)
            {
                logger.Warn(e.Message);
                if (!continueOnError) Environment.Exit(1);
            }
            catch (Exception e)
            {
                logger.Error(e.Message, e);
                if (!continueOnError) Environment.Exit(1);
            }
        }
    }
}
