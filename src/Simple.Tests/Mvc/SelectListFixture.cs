using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SharpTestsEx;
using Simple.Web.Mvc;
using System.Web.Mvc;
using Moq;
using System.Globalization;
using MvcContrib.FluentHtml;
using System.Linq.Expressions;

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

        [Test]
        public void CanCreateSelectControlFromIntListAndSelectFromExistingModel()
        {
            var dic = new ViewDataDictionary();
            dic.Add("Items", new[] { 1, 3, 5 }.ToSelectList(x => x, x => x + 1));
            dic.ModelState.SetModelValue("Item", new ValueProviderResult(1, "3", CultureInfo.InvariantCulture));

            var view = new Mock<IViewModelContainer<SampleModel>>();
            view.SetupGet(x => x.ViewData).Returns(dic);

            var select = view.Object.AutoSelect(x => x.Item, "Items");

            var selectedItem = select.SelectedValues.Cast<string>().Single();
            selectedItem.Should().Be("3");
        }


        [Test]
        public void CanCreateSelectControlFromSampleListAndSelectFromExistingModel()
        {
            var dic = new ViewDataDictionary();
            dic.Add("Items", new[] { 1, 3, 5 }.Select(x => new SampleModel { Item = x }).ToSelectList(x => x.Item, x => x.Item + 1));
            dic.ModelState.SetModelValue("Model", new ValueProviderResult(1, "3", CultureInfo.InvariantCulture));

            var view = new Mock<IViewModelContainer<SampleContainer>>();
            view.SetupGet(x => x.ViewData).Returns(dic);

            var select = view.Object.AutoSelect(x => x.Model, "Items");

            var selectedItem = select.SelectedValues.Cast<string>().Single();
            selectedItem.Should().Be("3");

        }

        [Test]
        public void CanCreateSelectControlFromSampleListAndSelectFromExistingModelWithAutoFind()
        {
            var dic = new ViewDataDictionary();
            dic.Add("Model", new[] { 1, 3, 5 }.Select(x => new SampleModel { Item = x }).ToSelectList(x => x.Item, x => x.Item + 1));
            dic.ModelState.SetModelValue("Model", new ValueProviderResult(1, "3", CultureInfo.InvariantCulture));

            var view = new Mock<IViewModelContainer<SampleContainer>>();
            view.SetupGet(x => x.ViewData).Returns(dic);

            var select = view.Object.AutoSelect(x => x.Model);

            var selectedItem = select.SelectedValues.Cast<string>().Single();
            selectedItem.Should().Be("3");

        }

        public class SampleContainer
        {
            public SampleModel Model { get; set; }
        }

        public class SampleModel
        {
            public int Item { get; set; }
        }


        protected void AssertItem(string value, string text, bool selected, SelectListItem list)
        {
            list.Text.Should().Be(text);
            list.Value.Should().Be(value);
            list.Selected.Should().Be(selected);
        }
    }
}
