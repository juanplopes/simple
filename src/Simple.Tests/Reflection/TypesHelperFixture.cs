using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using SharpTestsEx;

namespace Simple.Tests.Reflection
{
    [TestFixture]
    public class TypesHelperFixture
    {
        [Test]
        public void CanCreateNewIntegerOrFloat()
        {
            TypesHelper.GetBoxedDefaultInstance(typeof(int)).Should().Be(0);
            TypesHelper.GetBoxedDefaultInstance(typeof(float)).Should().Be(0f);

            TypesHelper.GetBoxedDefaultInstance(typeof(int?)).Should().Be(null);
            TypesHelper.GetBoxedDefaultInstance(typeof(float?)).Should().Be(null);
        }

        [Test]
        public void CanCreateNewVoid()
        {
            TypesHelper.GetBoxedDefaultInstance(typeof(void)).Should().Be(null);
        }
        [Test]
        public void CanCreateNewRefType()
        {
            TypesHelper.GetBoxedDefaultInstance(typeof(Console)).Should().Be(null);
        }


        [Test]
        public void CanAssignToInterface()
        {
            TypesHelper.CanAssign(typeof(List<int>), typeof(IList<int>)).Should().Be.True();
            TypesHelper.CanAssign(typeof(List<string>), typeof(IList<int>)).Should().Be.False();
        }

        [Test]
        public void CanAssignToAbstractClass()
        {
            TypesHelper.CanAssign(typeof(MemoryStream), typeof(Stream)).Should().Be.True();
            TypesHelper.CanAssign(typeof(Stream), typeof(MemoryStream)).Should().Be.False();
        }

        [Test]
        public void FlatNormalClassDefinitionName()
        {
            string className = TypesHelper.GetFlatClassName(typeof(TypesHelperFixture));
            className.Should().Be("TypesHelperFixture");
        }

        [Test]
        public void FlatGenericClassDefinitionName()
        {
            string className = TypesHelper.GetFlatClassName(typeof(IList<>));
            className.Should().Be("IList_T_");
        }

        [Test]
        public void FlatGenericClassName()
        {
            string className = TypesHelper.GetFlatClassName(typeof(IList<int>));
            className.Should().Be("IList_Int32_");
        }

        [Test]
        public void FlatGenericClassTwoName()
        {
            string className = TypesHelper.GetFlatClassName(typeof(IDictionary<string, int>));
            className.Should().Be("IDictionary_String_Int32_");
        }

        [Test]
        public void FlatGenericClassInnerName()
        {
            string className = TypesHelper.GetFlatClassName(typeof(Dictionary<string, int>.Enumerator));
            className.Should().Be("Dictionary_String_Int32__Enumerator");
        }

        [Test]
        public void CanGetValueTypeIfItsNotNullable()
        {
            typeof(int).GetValueTypeIfNullable().Should().Be(typeof(int));
        }

        [Test]
        public void CanGetValueTypeIfItsNullable()
        {
            typeof(int?).GetValueTypeIfNullable().Should().Be(typeof(int));
        }

        [Test]
        public void CanGetValueTypeIfItsReferenceType()
        {
            typeof(string).GetValueTypeIfNullable().Should().Be(typeof(string));
        }
    }
}
