using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Simple.Reflection;
using Simple.Entities;
using System.Collections.ObjectModel;
using System.ComponentModel;

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
            Type elType = null;

            if (typeof(IEntity).IsAssignableFrom(type))
                return BindEntity(controllerContext, bindingContext, type);

            if (IsEntityCollection(bindingContext, type, ref elType))
                return BindEntityCollection(controllerContext, bindingContext, elType);

            if (IsEntityEnumerable(type, ref elType))
                return BindEntityArray(controllerContext, bindingContext, elType);

            return base.BindModel(controllerContext, bindingContext);
        }

        private static bool IsEntityEnumerable(Type type, ref Type elType)
        {
            return IsOf(type, typeof(IEnumerable<>), out elType) && elType.CanAssign(typeof(IEntity));
        }

        private static bool IsEntityCollection(ModelBindingContext bindingContext, Type type, ref Type elType)
        {
            return IsOf(type, typeof(ICollection<>), out elType)
                && !type.IsArray
                && (!type.IsInterface || bindingContext.Model != null)
                && elType.CanAssign(typeof(IEntity));
        }

        private object BindEntityCollection(ControllerContext controllerContext, ModelBindingContext bindingContext, Type elType)
        {
            var array = BindEntityArray(controllerContext, bindingContext, elType);
            var model = bindingContext.Model;
            var invoker = new DynamicInvoker(typeof(ICollection<>).MakeGenericType(elType));

            if (model != null)
                invoker.Invoke(model, "Clear");
            else
                model = methodCache.CreateInstance(bindingContext.ModelType);

            foreach (var item in array)
                try
                {
                    invoker.Invoke(model, "Add", item);
                }
                catch (Exception e)
                {
                    bindingContext.ModelState.AddModelError(bindingContext.ModelName, e);
                }

            return model;
        }

        private Array BindEntityArray(ControllerContext controllerContext, ModelBindingContext bindingContext, Type elType)
        {
            var fullName = bindingContext.ModelName;
            var value = bindingContext.ValueProvider.GetValue(fullName);
            bindingContext.ModelState.SetModelValue(fullName, value);

            if (value == null) return null;

            var values = (value.RawValue as object[]);

            var obj = Array.CreateInstance(elType, values.Length);

            for (int i = 0; i < values.Length; i++)
            {
                var newObj = BindModel(controllerContext, new ModelBindingContext(bindingContext)
                {
                    ModelName = fullName,
                    ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(() => null, elType),
                    ValueProvider = new ElementalValueProvider(fullName, values[i], value.Culture)
                });
                obj.SetValue(newObj, i);
            }

            return obj;
        }

        private object BindEntity(ControllerContext context, ModelBindingContext bindingContext, Type type)
        {
            var fullName = bindingContext.ModelName;
            var value = bindingContext.ValueProvider.GetValue(fullName);
            var ctor = ctors.MakeConversionPlan(type);

            bindingContext.ModelState.SetModelValue(fullName, value);

            try
            {
                if (value == null)
                    return base.BindModel(context, bindingContext);

                var convertedValue = value.ConvertTo(ctor.ExpectedType);

                var obj = convertedValue != null ? ctor.Converter(convertedValue) : null;

                return obj;
            }
            catch (Exception e)
            {
                bindingContext.ModelState.AddModelError(fullName, e);
                return null;
            }
        }

        private static bool IsOf(Type type, Type typeToTest, out Type elementType)
        {
            elementType = type.GetInterfaces()
                .Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeToTest)
                .Select(x => x.GetGenericArguments().Single()).FirstOrDefault();
            return elementType != null;
        }



        protected override void BindProperty(ControllerContext controllerContext, ModelBindingContext bindingContext, System.ComponentModel.PropertyDescriptor propertyDescriptor)
        {
            base.BindProperty(controllerContext, bindingContext, propertyDescriptor);
        }


    }
}
