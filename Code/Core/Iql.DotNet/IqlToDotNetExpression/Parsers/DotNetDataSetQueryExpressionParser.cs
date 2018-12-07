using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Iql.DotNet.IqlToDotNetExpression.Parsers
{
    public class DotNetDataSetQueryExpressionParser : DotNetActionParserBase<IqlCollectitonQueryExpression>
    {
        static DotNetDataSetQueryExpressionParser()
        {
            EnumerableWhereMethod = typeof(Enumerable)
                .GetMethods().FirstOrDefault(m => m.Name == nameof(Enumerable.Where));
        }

        internal static MethodInfo EnumerableWhereMethod { get; set; }

        public override IqlExpression ToQueryStringTyped<TEntity>(IqlCollectitonQueryExpression action, DotNetIqlParserContext parser)
        {
            var filter = parser.Parse(action.Filter);
            var orderBys = action.OrderBys?.Select(o => parser.Parse(o));
            var expands = action.Expands?.Select(o => parser.Parse(o));
            Expression body = parser.ContextParameter;
            var expandsArray = expands?.ToArray();
            if (expandsArray != null && expandsArray.Any())
            {
                foreach (var expand in expandsArray)
                {
#if !TypeScript
                    body = parser.Chain<TEntity>(
                        body,
                        e =>
                        e.Run((MethodCallExpression)expand.Expression));
#endif
                }
            }

            if (filter.Expression != null)
            {
                body = parser.Chain<TEntity>(
                    body,
                    e =>
                        e.Where((Expression<Func<TEntity, bool>>)filter.ToLambda(), action.Filter));
            }

            if (action.WithKey != null)
            {
                var withKeyExpression = parser.Parse(action.WithKey);
                body = parser.Chain<TEntity>(
                    body,
                    e =>
                        e.Where((Expression<Func<TEntity, bool>>)withKeyExpression.ToLambda(), action.WithKey));
            }

            var orderBysArray = orderBys?.ToArray();
            if (orderBysArray != null && orderBysArray.Any())
            {
                foreach (var orderBy in orderBysArray)
                {

                }
            }

            var lambda = Expression.Lambda(body, parser.ContextParameter);
            parser.ConvertToLambda = false;
            return new IqlFinalExpression<Expression>(lambda);
        }
    }
}