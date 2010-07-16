using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Web.Mvc
{
    public class NoticeDefinition
    {
        public string Title { get; protected set; }
        public List<string> Items { get; protected set; }
        public NoticeDefinition()
        {
            Items = new List<string>();
        }

        public NoticeDefinition WithTitle(string title)
        {
            this.Title = title;
            return this;
        }

        public NoticeDefinition AddItems(params string[] item)
        {
            this.Items.AddRange(item);
            return this;
        }

    }
}
