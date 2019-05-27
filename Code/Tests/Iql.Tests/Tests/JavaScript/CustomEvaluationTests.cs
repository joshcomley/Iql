using System.Collections.Generic;
using System.Threading.Tasks;
using Iql.Conversion;
using Iql.Data.Evaluation;
using Iql.Extensions;
#if !TypeScript
using Iql.DotNet;
#endif
using Iql.JavaScript.JavaScriptExpressionToIql;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace Iql.Tests.Tests.JavaScript
{
    [TestClass]
    public class CustomEvaluationTests
    {
        class NameDescriptionTestEvaluator : IContextEvaluator
        {
            public object Name { get; set; }
            public object Description { get; set; }

            public NameDescriptionTestEvaluator(object name, object description)
            {
                Name = name;
                Description = description;
            }

            public ResolvedValue ResolveVariable(string path)
            {
                if (path == "something" || path == "somethingElse")
                {
                    return new ResolvedValue(this, true, false);
                }
                return new ResolvedValue(null, false);
            }

            public ResolvedValue ResolveProperty(object entity, string propertyName)
            {
                object value = null;
                var success = false;
                switch (propertyName)
                {
                    case "Owner":
                        return new ResolvedValue(this, true, false);
                    case nameof(Name):
                        value = Name;
                        success = true;
                        break;
                    case nameof(Description):
                        value = Description;
                        success = true;
                        break;
                }
                return new ResolvedValue(value, success);
            }
        }

        [TestMethod]
        public async Task MethodExpression()
        {
            InitConverter();
            var expression = "something.Owner.Name.indexOf(somethingElse.Description) == -1";
            var result = await JavaScriptEvaluator.EvaluateJavaScriptAsync(expression,
                new NameDescriptionTestEvaluator("abc", "def"));
            Assert.IsTrue(result.Success);
            Assert.AreEqual(true, result.Result);

            result = await JavaScriptEvaluator.EvaluateJavaScriptAsync(expression,
                new NameDescriptionTestEvaluator("abc123def", "123"));
            Assert.IsTrue(result.Success);
            Assert.AreEqual(false, result.Result);

            result = await JavaScriptEvaluator.EvaluateJavaScriptAsync(expression,
                new NameDescriptionTestEvaluator(null, "123"));
            Assert.IsTrue(result.Success);
            Assert.AreEqual(true, result.Result);

            expression = "something.Owner.Name.includes(somethingElse.Description)";
            result = await JavaScriptEvaluator.EvaluateJavaScriptAsync(expression,
               new NameDescriptionTestEvaluator("abc", "def"));
            Assert.IsTrue(result.Success);
            Assert.AreEqual(false, result.Result);

            result = await JavaScriptEvaluator.EvaluateJavaScriptAsync(expression,
                new NameDescriptionTestEvaluator("abc123def", "123"));
            Assert.IsTrue(result.Success);
            Assert.AreEqual(true, result.Result);

            result = await JavaScriptEvaluator.EvaluateJavaScriptAsync(expression,
                new NameDescriptionTestEvaluator(null, "123"));
            Assert.IsTrue(result.Success);
            Assert.AreEqual(false, result.Result);
        }

        class FilterClass
        {
            public List<FilterClassEntity> SomeList { get; set; }
            public string SomeName { get; set; }
        }

        class FilterClassEntity
        {
            public string Name { get; set; }
            public List<FilterClassNestedEntity> NestedEntities { get; set; }
        }

        class FilterClassNestedEntity
        {
            public int Age { get; set; }
        }
        class TestEvaluator : IContextEvaluator
        {
            public TestEvaluator()
            {

            }

            public ResolvedValue ResolveVariable(string path)
            {
                if (_variables.ContainsKey(path))
                {
                    return new ResolvedValue(_variables[path], true);
                }

                return new ResolvedValue(null, false);
            }

            public ResolvedValue ResolveProperty(object entity, string propertyName)
            {
                if (entity.GetType().GetProperty(propertyName) == null)
                {
                    return new ResolvedValue(null, false);
                }
                return new ResolvedValue(entity.GetPropertyValueByName(propertyName), true);
            }

            private Dictionary<string, object> _variables = new Dictionary<string, object>();
            public IContextEvaluator RegisterVariable(string name, FilterClass value)
            {
                _variables.Add(name, value);
                return this;
            }
        }

        [TestMethod]
        public async Task FilterExpressionWithRuntimeTypeResolution()
        {
            InitConverter();
            var context = new FilterClass();
            context.SomeList = new List<FilterClassEntity>(
                new FilterClassEntity[]
                {
                    new FilterClassEntity{Name = "abc"},
                    new FilterClassEntity{Name = "def"},
                });
            var expression = "something.SomeList.filter(_ => _.Name == 'abc').length > 0";
            var result = await JavaScriptEvaluator.EvaluateJavaScriptAsync(expression,
                new TestEvaluator().RegisterVariable("something", context));
            Assert.IsTrue(result.Success);
            Assert.AreEqual(true, result.Result);

            expression = "something.SomeList.filter(_ => _.Name == 'def').length > 0";
            result = await JavaScriptEvaluator.EvaluateJavaScriptAsync(expression,
                new TestEvaluator().RegisterVariable("something", context));
            Assert.IsTrue(result.Success);
            Assert.AreEqual(true, result.Result);

            expression = "something.SomeList.filter(_ => _.Name == 'hij').length > 0";
            result = await JavaScriptEvaluator.EvaluateJavaScriptAsync(expression,
                new TestEvaluator().RegisterVariable("something", context));
            Assert.IsTrue(result.Success);
            Assert.AreEqual(false, result.Result);
        }

        [TestMethod]
        public async Task FilterExpressionWithoutPossibilityOfRuntimeTypeResolution()
        {
            InitConverter();
            var context = new FilterClass();
            context.SomeList = new List<FilterClassEntity>(
                new FilterClassEntity[]
                {
                });
            var expression = "something.SomeList.filter(_ => _.Name == 'abc').length > 0";
            var result = await JavaScriptEvaluator.EvaluateJavaScriptAsync(expression,
                new TestEvaluator().RegisterVariable("something", context));
            Assert.IsTrue(result.Success);
            Assert.AreEqual(false, result.Result);
        }

        [TestMethod]
        public async Task StringMethodCall()
        {
            InitConverter();
            var context = new FilterClass();
            context.SomeList = new List<FilterClassEntity>(
                new FilterClassEntity[]
                {
                });
            var expression = "something.SomeName.includes('a')";
            var result = await JavaScriptEvaluator.EvaluateJavaScriptAsync(expression,
                new TestEvaluator().RegisterVariable("something", context));
            Assert.IsTrue(result.Success);
            Assert.AreEqual(false, result.Result);

            context.SomeName = "my name";

            result = await JavaScriptEvaluator.EvaluateJavaScriptAsync(expression,
                new TestEvaluator().RegisterVariable("something", context));
            Assert.IsTrue(result.Success);
            Assert.AreEqual(true, result.Result);
        }

        [TestMethod]
        public async Task StringNestedMethodCall()
        {
            InitConverter();
            var context = new FilterClass();
            context.SomeList = new List<FilterClassEntity>(
                new FilterClassEntity[]
                {
                });

            var expression = "something.SomeList.filter(_ => _.Name.includes('a')).length > 0";
            var result = await JavaScriptEvaluator.EvaluateJavaScriptAsync(expression,
                new TestEvaluator().RegisterVariable("something", context));
            Assert.IsTrue(result.Success);
            Assert.AreEqual(false, result.Result);

            context.SomeList.Add(new FilterClassEntity
            {
                Name = "b"
            });
            result = await JavaScriptEvaluator.EvaluateJavaScriptAsync(expression,
                new TestEvaluator().RegisterVariable("something", context));
            Assert.IsTrue(result.Success);
            Assert.AreEqual(false, result.Result);

            context.SomeList.Add(new FilterClassEntity
            {
                Name = "a"
            });
            result = await JavaScriptEvaluator.EvaluateJavaScriptAsync(expression,
                new TestEvaluator().RegisterVariable("something", context));
            Assert.IsTrue(result.Success);
            Assert.AreEqual(true, result.Result);

            context.SomeList = null;
            result = await JavaScriptEvaluator.EvaluateJavaScriptAsync(expression,
                new TestEvaluator().RegisterVariable("something", context));
            Assert.IsTrue(result.Success);
            Assert.AreEqual(false, result.Result);
        }

        [TestMethod]
        public async Task FilterExpressionWithNestedLambdas()
        {
            InitConverter();
            var context = new FilterClass();
            context.SomeList = new List<FilterClassEntity>(
                new FilterClassEntity[]
                {
                    new FilterClassEntity
                    {
                        Name = "abc",
                        NestedEntities = new List<FilterClassNestedEntity>(
                            new FilterClassNestedEntity[]
                            {
                                new FilterClassNestedEntity {Age = 21},
                                new FilterClassNestedEntity {Age = 31},
                            })
                    },
                    new FilterClassEntity
                    {
                        Name = "def",
                        NestedEntities = new List<FilterClassNestedEntity>(
                            new FilterClassNestedEntity[]
                            {
                                new FilterClassNestedEntity {Age = 41},
                                new FilterClassNestedEntity {Age = 51},
                            })
                    },
                    new FilterClassEntity
                    {
                        Name = "def",
                        NestedEntities = null
                    },
                });
            var expression = "something.SomeList.filter(_ => _.NestedEntities != null && _.NestedEntities.filter(n => n.Age > 40).length > 0).length == 0";
            var result = await JavaScriptEvaluator.EvaluateJavaScriptAsync(expression,
                new TestEvaluator().RegisterVariable("something", context));
            Assert.IsTrue(result.Success);
            Assert.AreEqual(false, result.Result);
            expression = "something.SomeList.filter(_ => _.NestedEntities != null && _.NestedEntities.filter(n => n.Age > 40).length > 0).length > 0";
            result = await JavaScriptEvaluator.EvaluateJavaScriptAsync(expression,
               new TestEvaluator().RegisterVariable("something", context));
            Assert.IsTrue(result.Success);
            Assert.AreEqual(true, result.Result);
        }

        private static void InitConverter()
        {
#if !TypeScript
            IqlExpressionConversion.DefaultExpressionConverter = () => new DotNetExpressionConverter();
#else
            IqlExpressionConversion.DefaultExpressionConverter = () => new JavaScriptExpressionConverter();
#endif
        }

        [TestMethod]
        public async Task ConditionalNotEqualsExpression()
        {
            InitConverter();
            var expression = "something.Owner.Name != somethingElse.Description";
            var result = await JavaScriptEvaluator.EvaluateJavaScriptAsync(expression,
                new NameDescriptionTestEvaluator("abc", "def"));
            Assert.IsTrue(result.Success);
            Assert.AreEqual(true, result.Result);

            result = await JavaScriptEvaluator.EvaluateJavaScriptAsync(expression,
                new NameDescriptionTestEvaluator("123", "123"));
            Assert.IsTrue(result.Success);
            Assert.AreEqual(false, result.Result);

            result = await JavaScriptEvaluator.EvaluateJavaScriptAsync(expression,
                new NameDescriptionTestEvaluator("123", 123));
            Assert.IsTrue(result.Success);
            Assert.AreEqual(true, result.Result);

            result = await JavaScriptEvaluator.EvaluateJavaScriptAsync(expression,
                new NameDescriptionTestEvaluator(123, 123));
            Assert.IsTrue(result.Success);
            Assert.AreEqual(false, result.Result);
        }

        [TestMethod]
        public async Task EvaluateFromJson()
        {
            InitConverter();
            var data = JObject.Parse(@"{ ""post"": { ""name"": ""Alon Bar"", ""text"": ""Very important\\ntext here!"", ""age"": 22, ""fans"": [{""name"": ""Leonard"", ""age"": 10 }, {""name"": ""John"", ""age"": 11 }, {""name"": ""Rufus"", ""age"": 12}] } }");

            var result = await JavaScriptEvaluator.EvaluateJavaScriptAsync(@"post.fans.filter(_ => _.name.includes('o')).length > 0",
                new JsonEvaluator(data));
            Assert.IsTrue(result.Success);
            Assert.AreEqual(true, result.Result);

            result = await JavaScriptEvaluator.EvaluateJavaScriptAsync(@"post.fans.filter(_ => _.name.includes('x')).length > 0",
                new JsonEvaluator(data));
            Assert.IsTrue(result.Success);
            Assert.AreEqual(false, result.Result);

            result = await JavaScriptEvaluator.EvaluateJavaScriptAsync(@"post.fans.filter(_ => _.age > 20).length > 0",
                new JsonEvaluator(data));
            Assert.IsTrue(result.Success);
            Assert.AreEqual(false, result.Result);

            result = await JavaScriptEvaluator.EvaluateJavaScriptAsync(@"post.fans.filter(_ => _.age > 20).length == 0",
                new JsonEvaluator(data));
            Assert.IsTrue(result.Success);
            Assert.AreEqual(true, result.Result);

            result = await JavaScriptEvaluator.EvaluateJavaScriptAsync(@"post.fans.filter(_ => _.age > 20).length == 1",
                new JsonEvaluator(data));
            Assert.IsTrue(result.Success);
            Assert.AreEqual(false, result.Result);

            result = await JavaScriptEvaluator.EvaluateJavaScriptAsync(@"post.fans.filter(_ => _.age < 20).length > 0",
                new JsonEvaluator(data));
            Assert.IsTrue(result.Success);
            Assert.AreEqual(true, result.Result);

            result = await JavaScriptEvaluator.EvaluateJavaScriptAsync(@"post.fans.filter(_ => _.age > 20 || _.name.includes('o')).length > 0",
                new JsonEvaluator(data));
            Assert.IsTrue(result.Success);
            Assert.AreEqual(true, result.Result);

            result = await JavaScriptEvaluator.EvaluateJavaScriptAsync(@"post.name.includes('x')",
                new JsonEvaluator(data));
            Assert.IsTrue(result.Success);
            Assert.AreEqual(false, result.Result);

            result = await JavaScriptEvaluator.EvaluateJavaScriptAsync(@"post.fans.filter(_ => _.age == null).length > 0",
                new JsonEvaluator(data));
            Assert.IsTrue(result.Success);
            Assert.AreEqual(false, result.Result);

            data = JObject.Parse(@"{ ""post"": { ""name"": ""Alon Bar"", ""text"": ""Very important\\ntext here!"", ""age"": 22, ""fans"": [{""name"": ""Leonard"", ""age"": 10 }, {""name"": ""John"", ""age"": 11 }, {""name"": ""Rufus""}] } }");
            result = await JavaScriptEvaluator.EvaluateJavaScriptAsync(@"post.name.includes('b')",
                new JsonEvaluator(data));
            Assert.IsTrue(result.Success);
            Assert.AreEqual(true, result.Result);

            result = await JavaScriptEvaluator.EvaluateJavaScriptAsync(@"post.fans.filter(_ => _.age < 5).length > 0",
                new JsonEvaluator(data));
            Assert.IsTrue(result.Success);
            Assert.AreEqual(false, result.Result);

            result = await JavaScriptEvaluator.EvaluateJavaScriptAsync(@"post.fans.filter(_ => _.age == null).length > 0",
                new JsonEvaluator(data));
            Assert.IsTrue(result.Success);
            Assert.AreEqual(true, result.Result);
        }
    }

    public class JsonEvaluator : IContextEvaluator
    {
        public JObject Data { get; }

        public JsonEvaluator(JObject data)
        {
            Data = data;
        }

        public ResolvedValue ResolveVariable(string path)
        {
            if (Data[path] == null)
            {
                return new ResolvedValue(null, false);
            }

            return new ResolvedValue(Data[path], true);
        }

        public ResolvedValue ResolveProperty(object entity, string propertyName)
        {
            var data = entity as JObject;
            if (data == null || data[propertyName] == null)
            {
                return new ResolvedValue(null, false);
            }

            return new ResolvedValue(data[propertyName], true);
        }
    }
}