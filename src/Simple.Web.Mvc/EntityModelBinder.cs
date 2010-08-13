using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Simple.Reflection;
using Simple.Entities;
using System.Collections.ObjectModel;

namespace Simple.Web.Mvc
{
    public class EntityModelBinder : DefaultModelBinder
    {
        ConversionConstructors ctors = new ConversionConstructors();
        MethodCache methodCache = new MethodCache();

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
                return BindEntity(controllerContext, bindingContext, type);
            }
            //else if (typeof(Array).IsAssignableFrom(type))
            //{
                
            //}
            //else if (IsOf(type, typeof(ICollection<>)) && bindingContext.Model != null && (bindingContext.Model as ICollection))
            //{

            //}
            //else if (IsOf(type, typeof(IEnumerable<>)))
            //{

            //}

            return base.BindModel(controllerContext, bindingContext);
        }

        private static bool IsOf(Type type, Type typeToTest)
        {
            return type.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeToTest);
        }

        private object BindEntity(ControllerContext context, ModelBindingContext bindingContext, Type type)
        {
            var fullName = bindingContext.ModelName;
            var ctor = ctors.GetBest(type);
            var value = bindingContext.ValueProvider.GetValue(fullName);
            bindingContext.ModelState.SetModelValue(fullName, value);

            try
            {
                if (value == null)
                    return base.BindModel(context, bindingContext);

                var paramType = ctor.GetParameters().Single().ParameterType;
                var convertedValue = value.ConvertTo(paramType);

                var obj = convertedValue != null ? methodCache.CreateInstance(type, convertedValue) : null;

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
