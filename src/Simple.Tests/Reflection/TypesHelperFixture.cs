using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

namespace Simple.Tests.Reflection
{
    [TestFixture]
    public class TypesHelperFixture
    {
        [Test]
        public void CanCreateNewIntegerOrFloat()
        {
            Assert.AreEqual(0, TypesHelper.GetBoxedDefaultInstance(typeof(int)));
            Assert.AreEqual(0f, TypesHelper.GetBoxedDefaultInstance(typeof(float)));

            Assert.AreEqual(null, TypesHelper.GetBoxedDefaultInstance(typeof(int?)));
            Assert.AreEqual(null, TypesHelper.GetBoxedDefaultInstance(typeof(float?)));
        }

        [Test]
        public void CanCreateNewVoid()
        {
            Assert.AreEqual(null, TypesHelper.GetBoxedDefaultInstance(typeof(void)));
        }
        [Test]
        public void CanCreateNewRefType()
        {
            Assert.AreEqual(null, TypesHelper.GetBoxedDefaultInstance(typeof(Console)));
        }


        [Test]
        public void CanAssignToInterface()
        {
            Assert.IsTrue(TypesHelper.CanAssign(typeof(List<int>), typeof(IList<int>)));
            Assert.IsFalse(TypesHelper.CanAssign(typeof(List<string>), typeof(IList<int>)));
        }

        [Test]
        public void CanAssignToAbstractClass()
        {
            Assert.IsTrue(TypesHelper.CanAssign(typeof(MemoryStream), typeof(Stream)));
            Assert.IsFalse(TypesHelper.CanAssign(typeof(Stream), typeof(MemoryStream)));
        }

        [Test]
        public void NormalClassDefinitionName()
        {
            string className = TypesHelper.GetRealClassName(typeof(TypesHelperFixture));
            Assert.AreEqual("TypesHelperFixture", className);
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
            string className = TypesHelper.GetFlatClassName(typeof(TypesHelperFixture));
            Assert.AreEqual("TypesHelperFixture", className);
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

        [Test]
        public void CanGetValueTypeIfItsNotNullable()
        {
            Assert.AreEqual(typeof(int), typeof(int).GetValueTypeIfNullable());
        }

        [Test]
        public void CanGetValueTypeIfItsNullable()
        {
            Assert.AreEqual(typeof(int), typeof(int?).GetValueTypeIfNullable());
        }

        [Test]
        public void CanGetValueTypeIfItsReferenceType()
        {
            Assert.AreEqual(typeof(string), typeof(string).GetValueTypeIfNullable());
        }
    }
}
