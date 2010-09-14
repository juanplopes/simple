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
            item.Should().Not.Be.Null();
            item.Title.Should().Be(null);
            item.Items.Should().Be.Empty();
        }

        [Test]
        public void GivenGenericDictionaryWithNoticeEntryCanVerifyExistence()
        {
            var dic = new Dictionary<string, object>();
            dic.Notify("test");

            dic.HasNotice("test").Should().Be.True();
        }


        [Test]
        public void GivenGenericDictionaryWithoutNoticeEntryCanVerifyAbsence()
        {
            var dic = new Dictionary<string, object>();
            dic.HasNotice("test").Should().Be.False();
        }

        [Test]
        public void GivenGenericDictionaryCreateCorrectEntryForSuccessMethod()
        {
            var dic = new Dictionary<string, object>();
            dic.NotifySuccess("some text");

            var item = dic[HtmlNotice.DefaultNotificationFormat.AsFormat(HtmlNotice.DefaultSucessClass)] as NoticeDefinition;
            item.Should().Not.Be.Null();
            item.Title.Should().Be("some text");
            item.Items.Should().Be.Empty();
        }


        [Test]
        public void GivenGenericDictionaryCreateCorrectEntryForErrorMethod()
        {
            var dic = new Dictionary<string, object>();
            dic.NotifyError("some text");

            var item = dic[HtmlNotice.DefaultNotificationFormat.AsFormat(HtmlNotice.DefaultErrorClass)] as NoticeDefinition;
            item.Should().Not.Be.Null();
            item.Title.Should().Be("some text");
            item.Items.Should().Be.Empty();
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
            item.Items.Should().Have.SameSequenceAs("item1", "item2");
        }

        [Test]
        public void GivenGenericDictionaryCreateCorrectEntryWithItemsOnSameMethod()
        {
            var dic = new Dictionary<string, object>();
            dic.Notify("test").AddItems("item1", "item2");

            var item = dic[HtmlNotice.DefaultNotificationFormat.AsFormat("test")] as NoticeDefinition;
            item.Items.Should().Have.SameSequenceAs("item1", "item2");
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
            item.Items.Should().Have.SameSequenceAs("test1", "test2");
        }

        [Test]
        public void CanRenderNoticeThatDoesnotExist()
        {
            var data = new ViewDataDictionary();
            var html = data.Notice("test");
            html.Should().Be.Null();
        }

        [Test]
        public void CanRenderNoticeThatHasNoText()
        {
            var data = new ViewDataDictionary();
            data.Notify("test");

            var html = data.Notice("test").ToString();
            html.Should().Contain("class=\"{0}\"".AsFormat(HtmlNotice.DefaultNotificationFormat.AsFormat("test")));
        }

        [Test]
        public void CanRenderNoticeThatHasOnlyTitle()
        {
            var data = new ViewDataDictionary();
            data.NotifySuccess("test");

            var html = data.NoticeSuccess().ToString();
            html.Should().Contain("class=\"{0}\"".AsFormat(
                HtmlNotice.DefaultNotificationFormat.AsFormat("success")));
            html.Should().Contain("<span>test</span>");
        }

        [Test]
        public void CanRenderNoticeThatHasOnlyText()
        {
            var data = new ViewDataDictionary();
            data.Notify("test").AddItems("a<sd");

            var html = data.Notice("test").ToString();
            html.Should().Contain("class=\"{0}\"".AsFormat(HtmlNotice.DefaultNotificationFormat.AsFormat("test")));
            html.Should().Contain("<ul><li>a&lt;sd</li></ul>");
        }

    }
}
