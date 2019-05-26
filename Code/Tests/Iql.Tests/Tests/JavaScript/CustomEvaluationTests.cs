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
        }

        class FilterClass
        {
            public List<FilterClassEntity> SomeList { get; set; }
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
    }
}