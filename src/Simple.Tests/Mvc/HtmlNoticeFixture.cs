using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SharpTestsEx;
using Simple.Web.Mvc;
using System.Web.Mvc;
using Moq;

namespace Simple.Tests.Mvc
{
    [TestFixture]
    public class HtmlNoticeFixture
    {
        [Test]
        public void GivenGenericDictionaryCreateCorrectEntryForTestKey()
        {
            var dic = new Dictionary<string, object>();
            dic.Notify("test");

            var item = dic[HtmlNotice.DefaultNotificationFormat.AsFormat("test")] as NoticeDefinition;
            Assert.IsNotNull(item);
            item.Title.Should().Be(null);
            CollectionAssert.IsEmpty(item.Items);
        }

        [Test]
        public void GivenGenericDictionaryCreateCorrectEntryForSuccessMethod()
        {
            var dic = new Dictionary<string, object>();
            dic.NotifySuccess("some text");

            var item = dic[HtmlNotice.DefaultNotificationFormat.AsFormat(HtmlNotice.DefaultSucessClass)] as NoticeDefinition;
            Assert.IsNotNull(item);
            item.Title.Should().Be("some text");
            CollectionAssert.IsEmpty(item.Items);
        }


        [Test]
        public void GivenGenericDictionaryCreateCorrectEntryForErrorMethod()
        {
            var dic = new Dictionary<string, object>();
            dic.NotifyError("some text");

            var item = dic[HtmlNotice.DefaultNotificationFormat.AsFormat(HtmlNotice.DefaultErrorClass)] as NoticeDefinition;
            Assert.IsNotNull(item);
            item.Title.Should().Be("some text");
            CollectionAssert.IsEmpty(item.Items);
        }


        [Test]
        public void GivenGenericDictionaryCreateCorrectEntryWithTitle()
        {
            var dic = new Dictionary<string, object>();
            dic.Notify("test").WithTitle("some title");

            var item = dic[HtmlNotice.DefaultNotificationFormat.AsFormat("test")] as NoticeDefinition;
            item.Title.Should().Be("some title");
        }

        [Test]
        public void GivenGenericDictionaryCreateCorrectEntryWithItems()
        {
            var dic = new Dictionary<string, object>();
            dic.Notify("test").AddItems("item1").AddItems("item2");

            var item = dic[HtmlNotice.DefaultNotificationFormat.AsFormat("test")] as NoticeDefinition;
            CollectionAssert.AreEqual(new[] { "item1", "item2" }, item.Items);
        }

        [Test]
        public void GivenGenericDictionaryCreateCorrectEntryWithItemsOnSameMethod()
        {
            var dic = new Dictionary<string, object>();
            dic.Notify("test").AddItems("item1", "item2");

            var item = dic[HtmlNotice.DefaultNotificationFormat.AsFormat("test")] as NoticeDefinition;
            CollectionAssert.AreEqual(new[] { "item1", "item2" }, item.Items);
        }

        [Test]
        public void GivenActionCanSetTitle()
        {
            var context = new Mock<ControllerContext>();
            var action = new Mock<ActionResult>();
            var controller = new Mock<Controller>();

            context.SetupGet(x => x.Controller).Returns(controller.Object);

            new NoticeActionResult(action.Object, x => x.Controller.ViewData.NotifyError("test")).ExecuteResult(context.Object);

            action.Verify(x => x.ExecuteResult(context.Object), Times.Exactly(1));

            var item = controller.Object.ViewData[HtmlNotice.DefaultNotificationFormat.AsFormat(HtmlNotice.DefaultErrorClass)] as NoticeDefinition;
            item.Title.Should().Be("test");
        }

        [Test]
        public void GivenActionCanSetTitleAndItems()
        {
            var context = new Mock<ControllerContext>();
            var action = new Mock<ActionResult>();
            var controller = new Mock<Controller>();

            context.SetupGet(x => x.Controller).Returns(controller.Object);

            new NoticeActionResult(action.Object, x => x.Controller.TempData.NotifySuccess("test"))
                .AddItems("test1", "test2")
                .ExecuteResult(context.Object);

            action.Verify(x => x.ExecuteResult(context.Object), Times.Exactly(1));

            var item = controller.Object.TempData[HtmlNotice.DefaultNotificationFormat.AsFormat(HtmlNotice.DefaultSucessClass)] as NoticeDefinition;
            item.Title.Should().Be("test");
            CollectionAssert.AreEqual(new[] { "test1", "test2" }, item.Items);
        }

        [Test]
        public void CanRenderNoticeThatDoesnotExist()
        {
            var data = new ViewDataDictionary();
            var html = data.Notice("test");
            Assert.IsNull(html);
        }

        [Test]
        public void CanRenderNoticeThatHasNoText()
        {
            var data = new ViewDataDictionary();
            data.Notify("test");

            var html = data.Notice("test").ToString();
            StringAssert.Contains("class=\"{0}\"".AsFormat(HtmlNotice.DefaultNotificationFormat.AsFormat("test")), html);
        }

        [Test]
        public void CanRenderNoticeThatHasOnlyTitle()
        {
            var data = new ViewDataDictionary();
            data.NotifySuccess("test");

            var html = data.NoticeSuccess().ToString();
            StringAssert.Contains("class=\"{0}\"".AsFormat(
                HtmlNotice.DefaultNotificationFormat.AsFormat("success")), html);
            StringAssert.Contains("<span>test</span>", html);
        }

        [Test]
        public void CanRenderNoticeThatHasOnlyText()
        {
            var data = new ViewDataDictionary();
            data.Notify("test").AddItems("a<sd");

            var html = data.Notice("test").ToString();
            StringAssert.Contains("class=\"{0}\"".AsFormat(HtmlNotice.DefaultNotificationFormat.AsFormat("test")), html);
            StringAssert.Contains("<ul><li>a&lt;sd</li></ul>",html);
        }

    }
}
