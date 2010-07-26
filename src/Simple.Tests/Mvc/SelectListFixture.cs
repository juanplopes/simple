using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.Web.Mvc;
using System.Web.Mvc;

namespace Simple.Tests.Mvc
{
    [TestFixture]
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
        public void CanCreateFromIntegerListAndSelectOneItem()
        {
            var items = new[] { 1, 3, 5 };
            var list = items.ToSelectList(x => x, x => x + 1).Select(1);
            AssertItem("1", "2", true, list[0]);
            AssertItem("3", "4", false, list[1]);
            AssertItem("5", "6", false, list[2]);

        }

        [Test]
        public void CanCreateFromIntegerListAndSelectTwoItems()
        {
            var items = new[] { 1, 3, 5 };
            var list = items.ToSelectList(x => x, x => x + 1).Select(1, 5);
            AssertItem("1", "2", true, list[0]);
            AssertItem("3", "4", false, list[1]);
            AssertItem("5", "6", true, list[2]);
        }

        [Test]
        public void CanCreateFromIntegerListAndSelectTwoItemsByObject()
        {
            var items = new[] { 1, 3, 5 };
            var list = items.ToSelectList(x => x, x => x + 1).SelectValue("1", 3);
            AssertItem("1", "2", true, list[0]);
            AssertItem("3", "4", true, list[1]);
            AssertItem("5", "6", false, list[2]);
        }

        [Test]
        public void CanCreateFromIntegerListAndSelectNoneFullOfItems()
        {
            var items = new[] { 1, 3, 5 };
            var list = items.ToSelectList(x => x, x => x + 1).SelectValue(null, null, null);
            AssertItem("1", "2", false, list[0]);
            AssertItem("3", "4", false, list[1]);
            AssertItem("5", "6", false, list[2]);
        }

        [Test]
        public void CanCreateFromIntegerListAndSelectSomeInvalidItem()
        {
            var items = new[] { 1, 3, 5 };
            var list = items.ToSelectList(x => x, x => x + 1).Select(2);
            AssertItem("1", "2", false, list[0]);
            AssertItem("3", "4", false, list[1]);
            AssertItem("5", "6", false, list[2]);

        }

        [Test]
        public void CanCreateFromNullableIntegerListAndSelectNull()
        {
            var items = new int?[] { 1, 3, 5 };
            var list = items.ToSelectList(x => x, x => x + 1).Select(null);
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
