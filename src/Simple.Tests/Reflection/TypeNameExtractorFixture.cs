using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.Reflection;

namespace Simple.Tests.Reflection
{
    public class TypeNameExtractorFixture
    {
        [Test]
        public void NormalClassDefinitionName()
        {
            var extractor = new TypeNameExtractor(typeof(TypeNameExtractorFixture));
            Assert.AreEqual("TypeNameExtractorFixture", extractor.GetName());
        }

        [Test]
        public void GenericClassDefinitionName()
        {
            var extractor = new TypeNameExtractor(typeof(IList<>));
            Assert.AreEqual("IList<T>", extractor.GetName());
        }

        [Test]
        public void GenericClassName()
        {
            var extractor = new TypeNameExtractor(typeof(IList<int>));
            Assert.AreEqual("IList<Int32>", extractor.GetName());
        }

        [Test]
        public void GenericClassDefinitionTwoName()
        {
            var extractor = new TypeNameExtractor(typeof(IDictionary<,>));
            Assert.AreEqual("IDictionary<TKey, TValue>", extractor.GetName());
        }

        [Test]
        public void GenericClassTwoName()
        {
            var extractor = new TypeNameExtractor(typeof(IDictionary<string, int>));
            Assert.AreEqual("IDictionary<String, Int32>", extractor.GetName());
        }

        [Test]
        public void GenericClassInnerName()
        {
            var extractor = new TypeNameExtractor(typeof(Dictionary<string, int>.Enumerator));
            Assert.AreEqual("Dictionary<String, Int32>.Enumerator", extractor.GetName());
        }

        [Test]
        public void GenericClassInsideGenericClassName()
        {
            var extractor = new TypeNameExtractor(typeof(Test<,>.Other<,>));
            Assert.AreEqual("TypeNameExtractorFixture.Test<A, B>.Other<C, D>", extractor.GetName());
        }

        [Test]
        public void GenericClassAsGenericParameter()
        {
            var extractor = new TypeNameExtractor(typeof(IDictionary<IList<int>, IList<string>>));
            Assert.AreEqual("IDictionary<IList<Int32>, IList<String>>", extractor.GetName());
        }


        [Test]
        public void GenericClassInsideNonGenericInsideGenericClassName()
        {
            var extractor = new TypeNameExtractor(typeof(Test<,>.NonGeneric.Generic<,>));
            Assert.AreEqual("TypeNameExtractorFixture.Test<A, B>.NonGeneric.Generic<C, D>", extractor.GetName());
        }

        [Test]
        public void CanReturnLowercaseNameForVoidType()
        {
            var extractor = new TypeNameExtractor(typeof(void));
            Assert.AreEqual("void", extractor.GetName());
        }

        public class Test<A, B>
        {
            public class NonGeneric
            {
                public class Generic<C, D>
                {

                }
            }

            public class Other<C, D>
            {
            }
        }
    }
   
}
