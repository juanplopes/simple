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
            var extractor = new TypeNameExtractor();
            Assert.AreEqual("TypeNameExtractorFixture", extractor.GetName(typeof(TypeNameExtractorFixture)));
        }

        [Test]
        public void GenericClassDefinitionName()
        {
            var extractor = new TypeNameExtractor();
            Assert.AreEqual("IList<T>", extractor.GetName(typeof(IList<>)));
        }

        [Test]
        public void GenericClassName()
        {
            var extractor = new TypeNameExtractor();
            Assert.AreEqual("IList<Int32>", extractor.GetName(typeof(IList<int>)));
        }

        [Test]
        public void GenericClassDefinitionTwoName()
        {
            var extractor = new TypeNameExtractor();
            Assert.AreEqual("IDictionary<TKey, TValue>", extractor.GetName(typeof(IDictionary<,>)));
        }

        [Test]
        public void GenericClassTwoName()
        {
            var extractor = new TypeNameExtractor();
            Assert.AreEqual("IDictionary<String, Int32>", extractor.GetName(typeof(IDictionary<string, int>)));
        }

        [Test]
        public void GenericClassInnerName()
        {
            var extractor = new TypeNameExtractor();
            Assert.AreEqual("Dictionary<String, Int32>.Enumerator", 
                extractor.GetName(typeof(Dictionary<string, int>.Enumerator)));
        }

        [Test]
        public void GenericClassInsideGenericClassName()
        {
            var extractor = new TypeNameExtractor();
            Assert.AreEqual("TypeNameExtractorFixture.Test<A, B>.Other<C, D>", 
                extractor.GetName(typeof(Test<,>.Other<,>)));
        }

        [Test]
        public void GenericClassAsGenericParameter()
        {
            var extractor = new TypeNameExtractor();
            Assert.AreEqual("IDictionary<IList<Int32>, IList<String>>", 
                extractor.GetName(typeof(IDictionary<IList<int>, IList<string>>)));
        }


        [Test]
        public void GenericClassInsideNonGenericInsideGenericClassName()
        {
            var extractor = new TypeNameExtractor();
            Assert.AreEqual("TypeNameExtractorFixture.Test<A, B>.NonGeneric.Generic<C, D>",
                extractor.GetName(typeof(Test<,>.NonGeneric.Generic<,>)));
        }

        [Test]
        public void CanReturnLowercaseNameForVoidType()
        {
            var extractor = new TypeNameExtractor();
            Assert.AreEqual("void", extractor.GetName(typeof(void)));
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
