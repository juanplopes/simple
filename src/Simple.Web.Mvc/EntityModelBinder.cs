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

        protected override void BindProperty(ControllerContext controllerContext, ModelBindingContext bindingContext, System.ComponentModel.PropertyDescriptor propertyDescriptor)
        {
            var type = propertyDescriptor.PropertyType;
            if (typeof(IEntity).IsAssignableFrom(type))
            {
                var fullName = CreateSubPropertyName(bindingContext.ModelName, propertyDescriptor.Name);
                var ctor = ctors.GetBest(type);
                var value = bindingContext.ValueProvider.GetValue(fullName);
                bindingContext.ModelState.SetModelValue(fullName, value);

                try
                {
                    if (value == null) return;
                    var paramType = ctor.GetParameters().Single().ParameterType;
                    var obj = MethodCache.Do.CreateInstance(type, value.ConvertTo(paramType));
                    propertyDescriptor.SetValue(bindingContext.Model, obj);
                }
                catch (Exception e)
                {
                    bindingContext.ModelState.AddModelError(propertyDescriptor.Name, e);
                }
                return;
            }
           

            base.BindProperty(controllerContext, bindingContext, propertyDescriptor);

        }
     
    }
}
