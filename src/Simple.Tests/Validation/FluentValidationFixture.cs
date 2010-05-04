using System.Linq;
using NUnit.Framework;
using Simple.Expressions;
using Simple.Reflection;
using Simple.Tests.Resources;
using System.Linq.Expressions;
using System.Collections.Generic;
using System;
using Simple.Common;

namespace Simple.Tests.Validation
{
    public class FluentValidationFixture
    {
        [Test]
        public void GenerateRightExpressionFromOneIdentifier()
        {
            Customer customer = new Customer();
            customer.Id = "Alan";

            var param = Expression.Parameter(typeof(Customer), "x");
            var idList = new EntityHelper<Customer>(x => x.Id).IdentifierList;

            var idExpression = idList.Select(prop =>
            {
                var idExpr = prop.GetPropertyExpression(param);
                var idLambda = Expression.Lambda(idExpr, param);
                return Expression.NotEqual(idExpr, Expression.Constant(idLambda.Compile().DynamicInvoke(customer)));
            }).AggregateJoin((expr1, expr2) => Expression.AndAlso(expr1, expr2));

            Assert.AreEqual("(x.Id != \"Alan\")", idExpression.ToString());
        }

        [Test]
        public void GenerateRightExpressionFromMultipleIdentifiers()
        {
            Customer customer = new Customer();
            customer.Id = "Alan";

            var param = Expression.Parameter(typeof(Customer), "x");
            var idList = new EntityHelper<Customer>(x => x.Id, x => x.CompanyName, x => x.ContactName).IdentifierList;

            var idExpression = idList.Select(prop =>
            {
                var idExpr = prop.GetPropertyExpression(param);
                var idLambda = Expression.Lambda(idExpr, param);
                return Expression.NotEqual(idExpr, Expression.Constant(idLambda.Compile().DynamicInvoke(customer)));
            }).AggregateJoin((expr1, expr2) => Expression.AndAlso(expr1, expr2));
            
            Assert.AreEqual("(((x.Id != \"Alan\") && (x.CompanyName != null)) && (x.ContactName != null))", idExpression.ToString());
        }

        //[Test]
        //public void Whatever()
        //{
        //    Customer customer = new Customer();
        //    customer.Id = "Alan";
        //    customer.Address = "dhje";
        //    customer.City = "dwedwe";

        //    Assert.AreEqual("x => (((x.Id != \"Alan\") && (x.Address = \"dhje\")) && (x.City = \"dwedwe\"))");
        //    customer.UniqueProperties(x => x.Address, x => x.City);
        //}
    }


}
