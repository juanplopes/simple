using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Validation;
using NUnit.Framework;

namespace Example.Project.Tests
{
    public class CustomAsserts
    {
        public static void Throws<T>(Action action, Action<T> assert)
            where T : Exception
        {
            try
            {
                action();
            }
            catch (T e)
            {
                assert(e);
                return;
            }
            Assert.Fail();
        }

        public static void ThrowsValidation(string property, bool strict, Action action)
        {
            Throws<SimpleValidationException>(action, e =>
            {
                if (strict) Assert.AreEqual(1, e.Errors.Count);
                Assert.AreEqual(1, e.Errors.Count(x => x.PropertyName == property));
            });

        }

        public static void ThrowsArgument(string property, Action action)
        {
            Throws<ArgumentException>(action, e =>
            {
                Assert.AreEqual(property, e.ParamName);
            });
        }

    }
}
