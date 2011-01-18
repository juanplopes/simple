using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;

namespace Simple.Web.Mvc
{
    public class HtmlTagBuilder : TagBuilder, IHtmlString
    {
        public HtmlTagBuilder(string tag) : base(tag) { }

        public string ToHtmlString()
        {
            return this.ToString();
        }
    }
}
