using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Tests.SampleServer;
using NUnit.Framework;
using NHibernate;
using Simple.DataAccess;
using Simple.Expressions;

namespace Simple.Tests.DataAccess
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

            Assert.AreEqual(5, list.Count);
            foreach (var obj in list)
            {
                Assert.AreEqual(obj.SampleInt, obj.SampleProduct.Supplier.Id);
                Assert.AreEqual(obj.SampleString, obj.SampleProduct.Name);
            }
        }

        [Test]
        public void TestHQLQueryByConstructorSample1()
        {
            IQuery q = Session.CreateQuery("select p, p.Supplier.Id, p.Name from Product p");
            q.SetMaxResults(5);
            q.SetResultTransformer(SimpleTransformers.ByConstructor<SampleDTO>());

            var list = q.List<SampleDTO>();

            Assert.AreEqual(5, list.Count);
            foreach (var obj in list)
            {
                Assert.AreEqual(obj.SampleInt, obj.SampleProduct.Supplier.Id);
                Assert.AreEqual(obj.SampleString, obj.SampleProduct.Name);
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

            Assert.AreEqual(5, list.Count);
            foreach (var obj in list)
                Assert.IsNull(obj.SampleProduct); //here is a NHibernate bug
        }

        [Test]
        public void TestHQLQueryByPropertiesSample1()
        {
            IQuery q = Session.CreateQuery("select p as SampleProduct, p.Supplier.Id as SampleInt, p.Name as SampleString from Product p");
            q.SetMaxResults(5);
            q.SetResultTransformer(SimpleTransformers.ByProperties<SampleDTO>());

            var list = q.List<SampleDTO>();

            Assert.AreEqual(5, list.Count);
            foreach (var obj in list)
            {
                Assert.AreEqual(obj.SampleInt, obj.SampleProduct.Supplier.Id);
                Assert.AreEqual(obj.SampleString, obj.SampleProduct.Name);
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

            Assert.AreEqual(5, list.Count);
            foreach (var obj in list)
            {
                Assert.AreEqual(obj.SampleInt, obj.Supplier.Id);
                Assert.AreEqual(obj.SampleString, obj.Name);
            }
        }

        [Test]
        public void TestHQLQueryByPropertiesSample2()
        {
            IQuery q = Session.CreateQuery("select p as SampleProduct, p.Supplier.Id as SampleInt, p.Name as SampleString from Product p");
            q.SetMaxResults(5);
            q.SetResultTransformer(SimpleTransformers.ByProperties<SampleDTO2>());

            var list = q.List<SampleDTO2>();

            Assert.AreEqual(5, list.Count);
            foreach (var obj in list)
            {
                Assert.AreEqual(obj.SampleInt, obj.Supplier.Id);
                Assert.AreEqual(obj.SampleString, obj.Name);
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
            Assert.AreEqual(5, dics.Count);

            foreach (var dic in dics)
            {
                Assert.AreEqual(3, dic.Count);
                Assert.AreEqual(dic["SampleInt"], ((Product)dic["SampleProduct"]).Supplier.Id);
                Assert.AreEqual(dic["SampleString"], ((Product)dic["SampleProduct"]).Name);
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
            Assert.AreEqual(5, dics.Count);

            foreach (var dic in dics)
            {
                Assert.AreEqual(2, dic.Count);
                Assert.That(dic.ContainsKey("SampleString"));
                Assert.That(dic.ContainsKey("SampleInt"));
            }
        }



    }
}


