using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Collections.Specialized;
using Simple.Web.Mvc;
using Moq;
using Simple.Tests.Mvc.Mocks;
using System.Globalization;

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
            var values = new NameValueCollectionValueProvider(col, CultureInfo.InvariantCulture);
            var context = new Mock<ControllerContext>().Object;


            var provider = ModelMetadataProviders.Current.GetMetadataForType(() => new T(), typeof(T));
            binding = new ModelBindingContext() { ModelMetadata = provider, ValueProvider = values };

            var obj = binder.BindModel(context, binding);
            return obj;
        }
    }
}
