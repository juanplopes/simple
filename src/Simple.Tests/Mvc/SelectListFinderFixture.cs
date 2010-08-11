using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Web.Mvc;
using Moq;
using System.Globalization;
using Simple.Web.Mvc;

namespace Simple.Tests.Mvc
{
    [TestFixture]
    public class SelectListFinderFixture
    {
        [Test]
        public void CanFindSingleIntItemOnlyFromModelState()
        {
            var view = new Mock<IViewDataContainer>();
            view.SetupGet(x => x.ViewData).Returns(
                GetViewData("list",
                new ModelSelectList<Test>(Values(1, 2, 3, 4, 5), x => x.A, x => x.ToString()), "B",
                GetModelState(2)));

            var list = view.Object.FindSelectList<TestParent, Test>(x => x.B, "list");
            Assert.AreEqual(5, list.Count);
            Assert.AreEqual(true, list[1].Selected);
        }

        [Test]
        public void CanFindSingleIntItemOnlyFromModelItSelf()
        {
            var view = new Mock<IViewDataContainer>();
            view.SetupGet(x => x.ViewData).Returns(
                GetViewData("list",
                new ModelSelectList<Test>(Values(1, 2, 3, 4, 5), x => x.A, x => x.ToString()),
                new TestParent() { B = new Test() { A = 2 } }));

            var list = view.Object.FindSelectList<TestParent, Test>(x => x.B, "list");
            Assert.AreEqual(5, list.Count);
            Assert.AreEqual(true, list[1].Selected);
        }

        [Test]
        public void WhenTheresNothingInViewDataWontCauseError()
        {
            var view = new Mock<IViewDataContainer>();
            view.SetupGet(x => x.ViewData).Returns(new ViewDataDictionary());
            var list = view.Object.FindSelectList<TestParent, Test>(x => x.B, "list");
            Assert.IsNotNull(list);
            Assert.IsEmpty(list);
        }



        public class TestParent
        {
            public Test B { get; set; }
        }

        public class Test
        {
            public int A { get; set; }
        }

        protected IEnumerable<Test> Values(params int[] values)
        {
            foreach (var v in values)
                yield return new Test() { A = v };
        }

        protected ModelState GetModelState(object value)
        {
            return new ModelState() { Value = new ValueProviderResult(value.ToString(), value.ToString(), CultureInfo.InvariantCulture) };
        }

        protected ViewDataDictionary GetViewData(string key, object value, string modelKey, ModelState modelState)
        {
            var dic = new ViewDataDictionary();
            dic[key] = value;
            dic.ModelState.Add(modelKey, modelState);
            return dic;
        }

        protected ViewDataDictionary GetViewData(string key, object value, object model)
        {
            var dic = new ViewDataDictionary(model);
            dic[key] = value;
            return dic;
        }
    }
}
