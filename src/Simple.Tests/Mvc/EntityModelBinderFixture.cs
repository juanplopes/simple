using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SharpTestsEx;
using Simple.Web.Mvc;
using System.Web.Mvc;
using Simple.Tests.Mvc.Mocks;
using System.Web.Routing;
using Moq;
using System.Collections.Specialized;
using Simple.Entities;

namespace Simple.Tests.Mvc
{
    [TestFixture]
    public class EntityModelBinderFixture
    {
       
        [Test]
        public void CanBindSimpleIntProperty()
        {
            var obj = TestBind<Test>(new NameValueCollection { { "Child", "2" } });
            
            obj.Should().Be.OfType<Test>().And
                .Value.Child.ID.Should().Be(2);
        }

        [Test]
        public void CannotBindSimpleIntPropertyWithNonIntValue()
        {
            ModelBindingContext context;
            var obj = TestBind<Test>(new NameValueCollection { { "Child", "2a" } }, out context);

            obj.Should().Be.OfType<Test>().And
                .Value.Child.Should().Be.Null();
            context.ModelState["Child"].Errors.Count.Should().Be(1);
        }

        [Test]
        public void CanBindMultipleIntProperty()
        {
            var obj = TestBind<Test>(new NameValueCollection { { "Child", "4" }, { "Child", "3" } });

            obj.Should().Be.OfType<Test>().And
                .Value.Child.ID.Should().Be(4);
        }

        [Test, Ignore("I hate DefaultModelBinder")]
        public void CanBindMultipleIntListProperty()
        {
            var obj = TestBind<TestList>(new NameValueCollection { { "Children", "4" }, { "Children", "3" } });

            var asserter = obj.Should().Be.OfType<TestList>().And.Value;
            asserter.Children.Count.Should().Be(2);
            asserter.Children.Select(x => x.ID).Should().Have.SameSequenceAs(4, 3);
        }

        [Test]
        public void CanBindNullToProperty()
        {
            var obj = TestBind<Test>(new NameValueCollection { { "Child", "" } });

            obj.Should().Be.OfType<Test>().And
                .Value.Child.Should().Be.Null();
        }


        [Test]
        public void CanBindInnerIntProperty()
        {
            var obj = TestBind<TestParent>(new NameValueCollection { { "Other.Child", "2" } });

            obj.Should().Be.OfType<TestParent>().And
                .Value.Other.Child.ID.Should().Be(2);
        }

        class TestParent
        {
            public Test Other { get; set; }
        }

        class TestList
        {
            public IList<TestChild> Children { get; set; }
        }

        class Test
        {
            public TestChild Child { get; set; }
        }

        class TestChild : IEntity
        {
            public TestChild(int id) { ID = id; }
            public int ID { get; set; }
        }

        private static object TestBind<T>(NameValueCollection col)
            where T : new()
        {
            ModelBindingContext context;
            return TestBind<T>(col, out context);
        }

        private static object TestBind<T>(NameValueCollection col, out ModelBindingContext binding)
            where T : new()
        {
            var binder = new EntityModelBinder(new ModelBinderDictionary());

            var context = new Mock<ControllerContext>();
            context
                .SetupGet(x => x.HttpContext.Request.Form)
                .Returns(col);

            var provider = ModelMetadataProviders.Current.GetMetadataForType(() => new T(), typeof(T));
            binding = new ModelBindingContext() { ModelMetadata = provider, ValueProvider = new FormValueProvider(context.Object) };

            var obj = binder.BindModel(context.Object, binding);
            return obj;
        }
    }
}
