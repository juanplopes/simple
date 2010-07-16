using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace Simple.Web.Mvc
{
    public class NoticeActionResult : ActionResult
    {
        protected ActionResult InnerAction { get; set; }
        public Func<ControllerContext, NoticeDefinition> Injector { get; set; }
        public NoticeActionResult(ActionResult innerAction, Func<ControllerContext, NoticeDefinition> injector)
        {
            this.InnerAction = innerAction;
            this.Injector = injector;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            Injector(context);
            InnerAction.ExecuteResult(context);
        }

        public NoticeActionResult WithTitle(string title)
        {
            var func = Injector;
            Injector = x => func(x).WithTitle(title);
            return this;
        }

        public NoticeActionResult AddItems(params string[] item)
        {
            var func = Injector;
            Injector = x => func(x).AddItems(item);
            return this;
        }

    }
}
