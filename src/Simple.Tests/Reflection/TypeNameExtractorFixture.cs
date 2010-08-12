using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SharpTestsEx;
using Simple.Reflection;

namespace Simple.Tests.Reflection
{
    public class TypeNameExtractorFixture
    {
        [Test]
        public void NormalClassDefinitionName()
        {
            var extractor = new TypeNameExtractor();
            extractor.GetName(typeof(TypeNameExtractorFixture)).Should().Be("TypeNameExtractorFixture");

            CollectionAssert.AreEquivalent(new[] {
                typeof(TypeNameExtractorFixture).Namespace
            }, extractor.Namespaces.ToArray());
        }

        [Test]
        public void NormalClassDefinitionFullName()
        {
            var extractor = new TypeNameExtractor();
            Assert.AreEqual(typeof(TypeNameExtractorFixture).FullName, extractor.GetName(typeof(TypeNameExtractorFixture), true));

            CollectionAssert.AreEquivalent(new[] {
                typeof(TypeNameExtractorFixture).Namespace
            }, extractor.Namespaces.ToArray());
        }


        [Test]
        public void GenericClassDefinitionName()
        {
            var extractor = new TypeNameExtractor();
            extractor.GetName(typeof(IList<>)).Should().Be("IList<T>");

            CollectionAssert.AreEquivalent(new[] {
                typeof(IList<>).Namespace
            }, extractor.Namespaces.ToArray());
        }

        [Test]
        public void GenericClassDefinitionFullName()
        {
            var extractor = new TypeNameExtractor();
            Assert.AreEqual("System.Collections.Generic.IList<T>", extractor.GetName(typeof(IList<>), true));

            CollectionAssert.AreEquivalent(new[] {
                typeof(IList<>).Namespace
            }, extractor.Namespaces.ToArray());
        }

        [Test]
        public void GenericClassName()
        {
            var extractor = new TypeNameExtractor();
            extractor.GetName(typeof(IList<int>)).Should().Be("IList<Int32>");

            CollectionAssert.AreEquivalent(new[] {
                typeof(IList<>).Namespace,
                typeof(Int32).Namespace
            }, extractor.Namespaces.ToArray());
        }

        [Test]
        public void GenericClassFullName()
        {
            var extractor = new TypeNameExtractor();
            Assert.AreEqual("System.Collections.Generic.IList<System.Int32>",
                extractor.GetName(typeof(IList<int>), true));

            CollectionAssert.AreEquivalent(new[] {
                typeof(IList<>).Namespace,
                typeof(Int32).Namespace
            }, extractor.Namespaces.ToArray());
        }

        [Test]
        public void GenericClassDefinitionTwoName()
        {
            var extractor = new TypeNameExtractor();
            Assert.AreEqual("IDictionary<TKey, TValue>", extractor.GetName(typeof(IDictionary<,>)));

            CollectionAssert.AreEquivalent(new[] {
                typeof(IDictionary<,>).Namespace,
            }, extractor.Namespaces.ToArray());

        }

        [Test]
        public void GenericClassDefinitionTwoFullName()
        {
            var extractor = new TypeNameExtractor();
            Assert.AreEqual("System.Collections.Generic.IDictionary<TKey, TValue>", extractor.GetName(typeof(IDictionary<,>), true));

            CollectionAssert.AreEquivalent(new[] {
                typeof(IDictionary<,>).Namespace,
            }, extractor.Namespaces.ToArray());

        }

        [Test]
        public void GenericClassTwoName()
        {
            var extractor = new TypeNameExtractor();
            Assert.AreEqual("IDictionary<String, Int32>", extractor.GetName(typeof(IDictionary<string, int>)));

            CollectionAssert.AreEquivalent(new[] {
                typeof(IDictionary<,>).Namespace,
                typeof(string).Namespace
            }, extractor.Namespaces.ToArray());
        }

        [Test]
        public void GenericClassInnerName()
        {
            var extractor = new TypeNameExtractor();
            Assert.AreEqual("Dictionary<String, Int32>.Enumerator",
                extractor.GetName(typeof(Dictionary<string, int>.Enumerator)));

            CollectionAssert.AreEquivalent(new[] {
                typeof(IDictionary<,>).Namespace,
                typeof(string).Namespace
            }, extractor.Namespaces.ToArray());
        }

        [Test]
        public void GenericClassInnerFullName()
        {
            var extractor = new TypeNameExtractor();
            Assert.AreEqual("System.Collections.Generic.Dictionary<System.String, System.Int32>.Enumerator",
                extractor.GetName(typeof(Dictionary<string, int>.Enumerator), true));

            CollectionAssert.AreEquivalent(new[] {
                typeof(IDictionary<,>).Namespace,
                typeof(string).Namespace
            }, extractor.Namespaces.ToArray());
        }

        [Test]
        public void GenericClassInsideGenericClassName()
        {
            var extractor = new TypeNameExtractor();
            Assert.AreEqual("TypeNameExtractorFixture.Test<A, B>.Other<C, D>",
                extractor.GetName(typeof(Test<,>.Other<,>)));

            CollectionAssert.AreEquivalent(new[] {
                typeof(TypeNameExtractorFixture).Namespace,
            }, extractor.Namespaces.ToArray());

        }



        [Test]
        public void GenericClassAsGenericParameter()
        {
            var extractor = new TypeNameExtractor();
            Assert.AreEqual("IDictionary<IList<Int32>, IList<String>>",
                extractor.GetName(typeof(IDictionary<IList<int>, IList<string>>)));

            CollectionAssert.AreEquivalent(new[] {
                typeof(IDictionary<,>).Namespace,
                typeof(string).Namespace
            }, extractor.Namespaces.ToArray());

        }


        [Test]
        public void GenericClassInsideNonGenericInsideGenericClassName()
        {
            var extractor = new TypeNameExtractor();
            Assert.AreEqual("TypeNameExtractorFixture.Test<A, B>.NonGeneric.Generic<C, D>",
                extractor.GetName(typeof(Test<,>.NonGeneric.Generic<,>)));

            CollectionAssert.AreEquivalent(new[] {
                typeof(TypeNameExtractorFixture).Namespace,
            }, extractor.Namespaces.ToArray());

        }

        [Test]
        public void GenericClassInsideNonGenericInsideGenericClassFullName()
        {
            var extractor = new TypeNameExtractor();
            Assert.AreEqual("Simple.Tests.Reflection.TypeNameExtractorFixture.Test<A, B>.NonGeneric.Generic<C, D>",
                extractor.GetName(typeof(Test<,>.NonGeneric.Generic<,>), true));

            CollectionAssert.AreEquivalent(new[] {
                typeof(TypeNameExtractorFixture).Namespace,
            }, extractor.Namespaces.ToArray());

        }

        [Test]
        public void CanReturnLowercaseNameForVoidType()
        {
            var extractor = new TypeNameExtractor();
            extractor.GetName(typeof(void)).Should().Be("void");

            CollectionAssert.AreEquivalent(new[] {
                typeof(void).Namespace,
            }, extractor.Namespaces.ToArray());

        }

        [Test]
        public void CanReturnLowercaseFullNameForVoidType()
        {
            var extractor = new TypeNameExtractor();
            Assert.AreEqual("void", extractor.GetName(typeof(void), true));

            CollectionAssert.AreEquivalent(new[] {
                typeof(void).Namespace,
            }, extractor.Namespaces.ToArray());

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
