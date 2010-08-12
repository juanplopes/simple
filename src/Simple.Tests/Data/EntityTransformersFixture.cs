using System;
using System.Collections.Generic;
using NHibernate;
using NUnit.Framework;
using SharpTestsEx;
using Simple.Data;
using Simple.Tests.Resources;

namespace Simple.Tests.Data
{
    [TestFixture]
    public class EntityTransformersFixture : BaseDataFixture
    {
        public class SampleDTO
        {
            public Product SampleProduct { get; set; }
            public int SampleInt { get; set; }
            public string SampleString { get; set; }


            public SampleDTO() { }
            public SampleDTO(Product p, int i, string s)
            {
                SampleProduct = p;
                SampleInt = i;
                SampleString = s;
            }
        }

        public class SampleDTO2 : Product
        {
            public int SampleInt { get; set; }
            public string SampleString { get; set; }
        }

        [Test]
        public void TestSQLQueryByConstructorSample1()
        {
            ISQLQuery q = Session.CreateSQLQuery("select *, SupplierID, ProductName from Products");
            q.SetMaxResults(5);
            q.AddEntity(typeof(Product));
            q.AddScalar("SupplierID", NHibernateUtil.Int32);
            q.AddScalar("ProductName", NHibernateUtil.String);
            
            q.SetResultTransformer(SimpleTransformers.ByConstructor<SampleDTO>());
            
            var list = q.List<SampleDTO>();

            list.Count.Should().Be(5);
            foreach (var obj in list)
            {
                obj.SampleProduct.Supplier.Id.Should().Be(obj.SampleInt);
                obj.SampleProduct.Name.Should().Be(obj.SampleString);
            }
        }

        [Test]
        public void TestHQLQueryByConstructorSample1()
        {
            IQuery q = Session.CreateQuery("select p, p.Supplier.Id, p.Name from Product p");
            q.SetMaxResults(5);
            q.SetResultTransformer(SimpleTransformers.ByConstructor<SampleDTO>());

            var list = q.List<SampleDTO>();

            list.Count.Should().Be(5);
            foreach (var obj in list)
            {
                obj.SampleProduct.Supplier.Id.Should().Be(obj.SampleInt);
                obj.SampleProduct.Name.Should().Be(obj.SampleString);
            }
        }

        [Test]
        public void TestSQLQueryByPropertiesSample1()
        {
            ISQLQuery q = Session.CreateSQLQuery("select *, SupplierID as SampleInt, ProductName as SampleString from Products");
            q.SetMaxResults(5);
            q.AddEntity("SampleProduct", typeof(Product));
            q.AddScalar("SampleInt", NHibernateUtil.Int32);
            q.AddScalar("SampleString", NHibernateUtil.String);
            q.SetResultTransformer(SimpleTransformers.ByProperties<SampleDTO>());

            var list = q.List<SampleDTO>();

            list.Count.Should().Be(5);
            foreach (var obj in list)
                obj.SampleProduct.Should().Be.Null(); //here is a NHibernate bug
        }

        [Test]
        public void TestHQLQueryByPropertiesSample1()
        {
            IQuery q = Session.CreateQuery("select p as SampleProduct, p.Supplier.Id as SampleInt, p.Name as SampleString from Product p");
            q.SetMaxResults(5);
            q.SetResultTransformer(SimpleTransformers.ByProperties<SampleDTO>());

            var list = q.List<SampleDTO>();

            list.Count.Should().Be(5);
            foreach (var obj in list)
            {
                obj.SampleProduct.Supplier.Id.Should().Be(obj.SampleInt);
                obj.SampleProduct.Name.Should().Be(obj.SampleString);
            }
        }

        [Test]
        public void TestSQLQueryByPropertiesSample2()
        {
            ISQLQuery q = Session.CreateSQLQuery("select *, SupplierID as SampleInt, ProductName as SampleString from Products");
            q.SetMaxResults(5);
            q.AddEntity(typeof(Product));
            q.AddScalar("SampleInt", NHibernateUtil.Int32);
            q.AddScalar("SampleString", NHibernateUtil.String);

            q.SetResultTransformer(SimpleTransformers.ByProperties<SampleDTO2>());

            var list = q.List<SampleDTO2>();

            list.Count.Should().Be(5);
            foreach (var obj in list)
            {
                obj.Supplier.Id.Should().Be(obj.SampleInt);
                obj.Name.Should().Be(obj.SampleString);
            }
        }

        [Test]
        public void TestHQLQueryByPropertiesSample2()
        {
            IQuery q = Session.CreateQuery("select p as SampleProduct, p.Supplier.Id as SampleInt, p.Name as SampleString from Product p");
            q.SetMaxResults(5);
            q.SetResultTransformer(SimpleTransformers.ByProperties<SampleDTO2>());

            var list = q.List<SampleDTO2>();

            list.Count.Should().Be(5);
            foreach (var obj in list)
            {
                obj.Supplier.Id.Should().Be(obj.SampleInt);
                obj.Name.Should().Be(obj.SampleString);
            }
        }


        [Test]
        public void TestSQLQueryToDictionaryFail()
        {
            ISQLQuery q = Session.CreateSQLQuery("select *, SupplierID as SampleInt, ProductName as SampleString from Products");
            q.SetMaxResults(5);
            q.AddEntity(typeof(Product));
            q.AddScalar("SampleInt", NHibernateUtil.Int32);
            q.AddScalar("SampleString", NHibernateUtil.String);

            q.SetResultTransformer(SimpleTransformers.ToDictionary);

            Assert.Throws<InvalidOperationException>(()=>q.List());
        }

        [Test]
        public void TestHQLQueryToDictionaryWontFail()
        {
            IQuery q = Session.CreateQuery("select p as SampleProduct, p.Supplier.Id as SampleInt, p.Name as SampleString from Product p");
            q.SetMaxResults(5);
            q.SetResultTransformer(SimpleTransformers.ToDictionary);

            var dics = q.List<Dictionary<string, object>>();
            dics.Count.Should().Be(5);

            foreach (var dic in dics)
            {
                dic.Count.Should().Be(3);
                var asserter = dic["SampleProduct"].Should().Be.OfType<Product>().And;
 
                asserter.Value.Supplier.Id.Should().Be((int)dic["SampleInt"]);
                asserter.Value.Name.Should().Be((string)dic["SampleString"]);
            }
        }

        [Test]
        public void TestSQLQueryToDictionary()
        {
            ISQLQuery q = Session.CreateSQLQuery("select SupplierID as SampleInt, ProductName as SampleString from Products");
            q.SetMaxResults(5);
            q.AddScalar("SampleInt", NHibernateUtil.Int32);
            q.AddScalar("SampleString", NHibernateUtil.String);

            q.SetResultTransformer(SimpleTransformers.ToDictionary);

            var dics = q.List<Dictionary<string, object>>();
            dics.Count.Should().Be(5);

            foreach (var dic in dics)
            {
                dic.Count.Should().Be(2);
                Assert.That(dic.ContainsKey("SampleString"));
                Assert.That(dic.ContainsKey("SampleInt"));
            }
        }



    }
}


