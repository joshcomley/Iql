using System;
using System.Threading.Tasks;
using Iql.Conversion;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IqlSampleApp.Data.Entities;

namespace Iql.Tests.Tests
{
    [TestClass]
    public class ContextReplacementTests : TestsBase
    {
        public class MyContext<T>
        {
            public T Entity { get; set; }

            public string Get(string id)
            {
                return "hello back";
            }
        }

        [TestMethod]
        public async Task ArbitraryContext()
        {
            var iql = new IqlLambdaExpression(
                IqlType.String,
                new IqlPropertyExpression(
                    nameof(Client.Name),
                    new IqlPropertyExpression(
                        nameof(MyContext<Client>.Entity),
                        new IqlRootReferenceExpression()
                    )
                )).AddParameter();
            var client = new Client();
            client.Name = "My client";
            var context = new MyContext<Client>
            {
                Entity = client
            };
            var lambda = IqlConverter.Instance.ConvertIqlToExpression<MyContext<Client>>(iql);
            var fn = (Func<MyContext<Client>, string>)lambda.Compile();
            var result = fn(context);
            Assert.AreEqual(result, "My client");
        }


        [TestMethod]
        public async Task CallingContextMethod()
        {
            var iql = new IqlLambdaExpression(
                IqlType.String,
                new IqlInvocationExpression(
                    nameof(MyContext<Client>.Get),
                    null,
                    new IqlRootReferenceExpression()
                ).AddLiteralParameter("hello"))
                .AddParameter();
            var client = new Client();
            client.Name = "My client";
            var context = new MyContext<Client>
            {
                Entity = client
            };
            var lambda = IqlConverter.Instance.ConvertIqlToExpression<MyContext<Client>>(iql);
            var fn = (Func<MyContext<Client>, string>)lambda.Compile();
            var result = fn(context);
            Assert.AreEqual(result, "hello back");
        }

        //[TestMethod]
        //public async Task PropertyTouching()
        //{
        //    var iql = new IqlLambdaExpression(
        //        IqlType.String,
        //        new IqlPropertyExpression(
        //            nameof(Client.Name),
        //            new IqlRootReferenceExpression()
        //        )).AddParameter();

        //    var client = new Client();
        //    client.Name = "My client";
        //    var context = new MyContext<Client>
        //    {
        //        Entity = client
        //    };
        //    var lambda = IqlConverter.Instance.ConvertIqlToExpression<MyContext<Client>>(iql);
        //    var fn = (Func<MyContext<Client>, string>)lambda.Compile();
        //    var result = fn(context);
        //    Assert.AreEqual(result, "My client");
        //}
    }
}