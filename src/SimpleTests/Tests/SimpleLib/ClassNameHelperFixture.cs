using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.Reflection;

namespace Simple.Tests.SimpleLib
{
    [TestFixture]
    public class ClassNameHelperFixture
    {
        [Test]
        public void NormalClassDefinitionName()
        {
            string className = TypesHelper.GetRealClassName(typeof(ClassNameHelperFixture));
            Assert.AreEqual("ClassNameHelperFixture", className);
        }

        [Test]
        public void GenericClassDefinitionName()
        {
            string className = TypesHelper.GetRealClassName(typeof(IList<>));
            Assert.AreEqual("IList<>", className);
        }

        [Test]
        public void GenericClassName()
        {
            string className = TypesHelper.GetRealClassName(typeof(IList<int>));
            Assert.AreEqual("IList<Int32>", className);
        }

        [Test]
        public void GenericClassTwoName()
        {
            string className = TypesHelper.GetRealClassName(typeof(IDictionary<string, int>));
            Assert.AreEqual("IDictionary<String,Int32>", className);
        }

        [Test]
        public void GenericClassInnerName()
        {
            string className = TypesHelper.GetRealClassName(typeof(Dictionary<string, int>.Enumerator));
            Assert.AreEqual("Enumerator<String,Int32>", className);
        }

        [Test]
        public void FlatNormalClassDefinitionName()
        {
            string className = TypesHelper.GetFlatClassName(typeof(ClassNameHelperFixture));
            Assert.AreEqual("ClassNameHelperFixture", className);
        }

        [Test]
        public void FlatGenericClassDefinitionName()
        {
            string className = TypesHelper.GetFlatClassName(typeof(IList<>));
            Assert.AreEqual("IList__", className);
        }

        [Test]
        public void FlatGenericClassName()
        {
            string className = TypesHelper.GetFlatClassName(typeof(IList<int>));
            Assert.AreEqual("IList_Int32_", className);
        }

        [Test]
        public void FlatGenericClassTwoName()
        {
            string className = TypesHelper.GetFlatClassName(typeof(IDictionary<string, int>));
            Assert.AreEqual("IDictionary_String_Int32_", className);
        }

        [Test]
        public void FlatGenericClassInnerName()
        {
            string className = TypesHelper.GetFlatClassName(typeof(Dictionary<string, int>.Enumerator));
            Assert.AreEqual("Enumerator_String_Int32_", className);
        }
    }
}
