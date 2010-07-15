using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.Web.Mvc;
using System.Web.Mvc;

namespace Simple.Tests.Mvc
{
    public class SelectListFixture
    {
        [Test]
        public void CanCreateFromIntegerList()
        {
            var items = new[] { 1, 3, 5 };
            var list = items.ToSelectList(x => x, x => x + 1).ToArray();
            AssertItem("1", "2", false, list[0]);
            AssertItem("3", "4", false, list[1]);
            AssertItem("5", "6", false, list[2]);

        }


        [Test]
        public void CanCreateFromIntegerListAndSelectSomeItem()
        {
            var items = new[] { 1, 3, 5 };
            var list = items.ToSelectList(x => x, x => x + 1, 1).ToArray();
            AssertItem("1", "2", true, list[0]);
            AssertItem("3", "4", false, list[1]);
            AssertItem("5", "6", false, list[2]);

        }

        [Test]
        public void CanCreateFromIntegerListAndSelectSomeInvalidItem()
        {
            var items = new[] { 1, 3, 5 };
            var list = items.ToSelectList(x => x, x => x + 1, 2).ToArray();
            AssertItem("1", "2", false, list[0]);
            AssertItem("3", "4", false, list[1]);
            AssertItem("5", "6", false, list[2]);

        }


        protected void AssertItem(string value, string text, bool selected, SelectListItem list)
        {
            Assert.AreEqual(text, list.Text);
            Assert.AreEqual(value, list.Value);
            Assert.AreEqual(selected, list.Selected);
        }
    }
}
