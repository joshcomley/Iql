using System.Threading.Tasks;
using Iql.Conversion;
using Iql.Data.Evaluation;
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
        class Evaluator : IContextEvaluator
        {
            public object Name { get; set; }
            public object Description { get; set; }

            public Evaluator(object name, object description)
            {
                Name = name;
                Description = description;
            }

            public object ResolveVariable(string path)
            {
                return this;
            }

            public object ResolveProperty(object entity, string propertyName)
            {
                switch (propertyName)
                {
                    case nameof(Name):
                        return Name;
                    case nameof(Description):
                        return Description;
                }
                return this;
            }
        }

        [TestMethod]
        public async Task MethodExpression()
        {
            InitConverter();
            var expression = "something.Owner.Name.indexOf(somethingElse.Description) == -1";
            var result = await JavaScriptEvaluator.EvaluateJavaScriptAsync(expression,
                new Evaluator("abc", "def"));
            Assert.IsTrue(result.Success);
            Assert.AreEqual(true, result.Result);

            result = await JavaScriptEvaluator.EvaluateJavaScriptAsync(expression,
                new Evaluator("abc123def", "123"));
            Assert.IsTrue(result.Success);
            Assert.AreEqual(false, result.Result);
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
                new Evaluator("abc", "def"));
            Assert.IsTrue(result.Success);
            Assert.AreEqual(true, result.Result);

            result = await JavaScriptEvaluator.EvaluateJavaScriptAsync(expression,
                new Evaluator("123", "123"));
            Assert.IsTrue(result.Success);
            Assert.AreEqual(false, result.Result);

            result = await JavaScriptEvaluator.EvaluateJavaScriptAsync(expression,
                new Evaluator("123", 123));
            Assert.IsTrue(result.Success);
            Assert.AreEqual(true, result.Result);

            result = await JavaScriptEvaluator.EvaluateJavaScriptAsync(expression,
                new Evaluator(123, 123));
            Assert.IsTrue(result.Success);
            Assert.AreEqual(false, result.Result);
        }
    }
}