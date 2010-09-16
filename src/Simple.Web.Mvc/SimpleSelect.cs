using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcContrib.FluentHtml.Elements;

namespace Simple.Web.Mvc
{
    public class SimpleSelect<T> : Select<T>
    {
        protected override string RenderOptions()
        {
            return base.RenderOptions();
        }
    }
}
