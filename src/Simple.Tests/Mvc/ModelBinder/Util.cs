using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Collections.Specialized;
using Simple.Web.Mvc;
using Moq;

namespace Simple.Tests.Mvc.ModelBinder
{
    public static class Util
    {
        public static object TestBind<T>(this NameValueCollection col)
            where T : new()
        {
            ModelBindingContext context;
            return TestBind<T>(col, out context);
        }

        public static object TestBind<T>(this NameValueCollection col, out ModelBindingContext binding)
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
