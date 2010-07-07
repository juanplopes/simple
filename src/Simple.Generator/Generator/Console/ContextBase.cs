using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple;
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
                logger.Warn("Failed to configure: {0}".AsFormat(e.Message) , e);
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
            catch (Exception e)
            {
                System.Console.WriteLine("Error configuring logging: {0}", e.Message);
            }
        }

        protected virtual void OnBeforeParse(string command, bool interactive) { }
        protected virtual void OnBeforeExecute(ICommand commandObject, string command, bool interactive)
        {
            if (interactive && commandObject is IUnsafeCommand)
                throw new ParserException("Command not allowed in interactive mode.");
        }
        protected virtual void OnAfterExecute(ICommand commandObject, string command, bool interactive) { }

        public virtual void Execute(string command, bool interactive)
        {
            try
            {
                OnBeforeParse(command, interactive);

                var commandObject = resolver.Resolve(command);

                OnBeforeExecute(commandObject, command, interactive);

                commandObject.Execute();

                OnAfterExecute(commandObject, command, interactive);
            }
            catch (ParserException e)
            {
                logger.Warn(e.Message);
                if (!interactive) Environment.Exit(1);
            }
            catch (Exception e)
            {
                logger.Error(e.Message, e);
                if (!interactive) Environment.Exit(1);
            }
        }
    }
}
