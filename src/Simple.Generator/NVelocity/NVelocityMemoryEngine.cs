using System;
using System.Collections;
using System.IO;
using NVelocity.Context;
using NVelocity.Exception;
using NVelocity;
using NVelocity.App;

namespace Simple.NVelocity
{
    public class NVelocityMemoryEngine : VelocityEngine
    {
        public NVelocityMemoryEngine(bool cacheTemplate)
            : base()
        {
            this.SetProperty("assembly.resource.loader.cache", cacheTemplate.ToString().ToLower());
        }


        public string Process(IContext context, string template)
        {
            StringWriter writer = new StringWriter();
            this.Evaluate(context, writer, "mystring", template);
            return writer.ToString();
        }

        public void Process(IContext context, TextWriter writer, string template)
        {
            try
            {
                this.Evaluate(context, writer, "mystring", template);
            }
            catch (ParseErrorException pe)
            {
                writer.Write(pe.Message);
            }
            catch (MethodInvocationException mi)
            {
                writer.Write(mi.Message);
            }
        }
    }
}
