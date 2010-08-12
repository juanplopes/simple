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
        class TestParent
        {
            public Test Other { get; set; }
        }

        class Test
        {
            public TestChild Child { get; set; }
        }

        class TestChild : IEntity
        {
            //public TestChild(string id) { ID = int.Parse(id) + 1; }
            public TestChild(int id) { ID = id; }
            public int ID { get; set; }
        }

        [Test]
        public void CanBindSimpleIntProperty()
        {
            var obj = TestBind<Test>(new NameValueCollection { { "Child", "2" } });
            
            obj.Should().Be.OfType<Test>().And
                .Value.Child.ID.Should().Be(2);
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

        private static object TestBind<T>(NameValueCollection col)
            where T : new()
        {
            var binder = new EntityModelBinder(new ModelBinderDictionary());

            var context = new Mock<ControllerContext>();
            context
                .SetupGet(x => x.HttpContext.Request.Form)
                .Returns(col);

            var provider = ModelMetadataProviders.Current.GetMetadataForType(() => new T(), typeof(T));
            var binding = new ModelBindingContext() { ModelMetadata = provider, ValueProvider = new FormValueProvider(context.Object) };

            var obj = binder.BindModel(context.Object, binding);
            return obj;
        }
    }
}
