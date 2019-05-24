using System.Collections.Generic;
using System.Threading.Tasks;
using Iql.Conversion;
using Iql.Data;
using Iql.Data.Evaluation;

namespace Iql.JavaScript.JavaScriptExpressionToIql
{
    public class JavaScriptEvaluator
    {
        private static readonly Dictionary<string, IqlExpression> IqlLookup = new Dictionary<string, IqlExpression>();
        public static async Task<IqlExpressonEvaluationResult> EvaluateJavaScriptAsync(
            string expression,
            IContextEvaluator evaluator,
            IExpressionConverter converter = null)
        {
            converter = converter ?? IqlConverter.Instance;
            IqlExpression iql;
            if (!IqlLookup.ContainsKey(expression))
            {
                var fn = $"function () {{ return {expression}; }}";
                var conversionResult = new JavaScriptExpressionConverter().ConvertJavaScriptStringToIql<object>(fn, null);
                iql = conversionResult.Expression;
                IqlLookup.Add(expression, iql);
            }
            else
            {
                iql = IqlLookup[expression];
            }
            var evaluationSession = new EvaluationSession(true, EvaluationCacheMode.None, converter);
            var result = await evaluationSession.EvaluateIqlCustomAsync(iql, evaluator);
            return result;
        }

    }
}