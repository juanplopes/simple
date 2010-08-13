using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Simple.Reflection;
using Simple.Entities;

namespace Simple.Web.Mvc
{
    public class EntityModelBinder : DefaultModelBinder
    {
        ConversionConstructors ctors = new ConversionConstructors();

        public EntityModelBinder() { }

        public EntityModelBinder(ModelBinderDictionary binders)
        {
            Binders = binders;
            binders.DefaultBinder = this;
        }

        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var type = bindingContext.ModelType;
            if (typeof(IEntity).IsAssignableFrom(type))
            {
                return BindEntity(bindingContext, type);
            }

            return base.BindModel(controllerContext, bindingContext);
        }

        private object BindEntity(ModelBindingContext bindingContext, Type type)
        {
            var fullName = bindingContext.ModelName;
            var ctor = ctors.GetBest(type);
            var value = bindingContext.ValueProvider.GetValue(fullName);
            bindingContext.ModelState.SetModelValue(fullName, value);

            try
            {
                if (value == null) return null;
                var paramType = ctor.GetParameters().Single().ParameterType;
                var convertedValue = value.ConvertTo(paramType);

                var obj = convertedValue != null ? MethodCache.Do.CreateInstance(type, convertedValue) : null;

                return obj;
            }
            catch (Exception e)
            {
                bindingContext.ModelState.AddModelError(fullName, e);
                return null;
            }
        }

        protected override void BindProperty(ControllerContext controllerContext, ModelBindingContext bindingContext, System.ComponentModel.PropertyDescriptor propertyDescriptor)
        {

           

            base.BindProperty(controllerContext, bindingContext, propertyDescriptor);

        }
    

    }
}
