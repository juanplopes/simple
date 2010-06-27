using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using NVelocity;
using System.IO;
using NVelocity.App;
using NVelocity.Exception;
using Simple.Reflection;
using System.Linq.Expressions;

namespace Simple.NVelocity
{
    public class SimpleTemplate : VelocityContext
    {
        private string _template = null;
        public string Template
        {
            get
            {
                return _template ?? string.Empty;
            }
            set
            {
                _template = value ?? string.Empty;
            }
        }

        public virtual object this[string key]
        {
            get
            {
                return this.Get(key);
            }
            set
            {
                this.Put(key, value);
            }
        }

        public virtual SimpleTemplate SetMany(object anonymous)
        {
            var dic = DictionaryHelper.FromAnonymous(anonymous);
            foreach (var item in dic)
                this[item.Key] = item.Value;
            return this;
        }

        public virtual SimpleTemplate SetMany(params Expression<Func<object, object>>[] expr)
        {
            var dic = DictionaryHelper.FromExpressions(expr);
            foreach (var item in dic)
                this[item.Key] = item.Value;
            return this;
        }

        public SimpleTemplate() { }

        public SimpleTemplate(string template)
            : this()
        {
            this.Template = template;
        }

        public override string ToString()
        {
            return this.Render();
        }

        public string Render()
        {
            var writer = new StringWriter();
            Render(writer);
            return writer.ToString();
        }

        public void Render(TextWriter writer)
        {
            var engine = EngineWrapper.Do.Get();
            try
            {
                engine.Evaluate(this, writer, this.GetType().FullName, Template);
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
