using System.Collections.Generic;
using System.Threading.Tasks;
using Iql.Conversion;
using Iql.Data;
using Iql.Data.Evaluation;
using Iql.Data.Types;

namespace Iql.JavaScript.JavaScriptExpressionToIql
{
    public class JavaScriptEvaluator
    {
        private static bool IqlLookupDelayedInitialized;
        private static Dictionary<string, IqlExpression> IqlLookupDelayed;
        private static Dictionary<string, IqlExpression> IqlLookup { get { if(!IqlLookupDelayedInitialized) { IqlLookupDelayedInitialized = true; IqlLookupDelayed = new Dictionary<string, IqlExpression>(); } return IqlLookupDelayed; } set { IqlLookupDelayedInitialized = true; IqlLookupDelayed = value; } }
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
            //var typeResolver = new TypeResolver();
            //typeResolver.ContextEvaluator = evaluator;
            var evaluationSession = new EvaluationSession(true, EvaluationCacheMode.None, converter);
            var result = await evaluationSession.EvaluateIqlCustomAsync(iql, evaluator, evaluator);
            return result;
        }

    }
}