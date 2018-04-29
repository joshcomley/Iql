using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Iql.DotNet.IqlToDotNetExpression.Parsers
{
    public class DotNetDataSetQueryExpressionParser : DotNetActionParserBase<IqlDataSetQueryExpression>
    {
        static DotNetDataSetQueryExpressionParser()
        {
            EnumerableWhereMethod = typeof(Enumerable)
                .GetMethods().FirstOrDefault(m => m.Name == nameof(Enumerable.Where));
        }

        internal static MethodInfo EnumerableWhereMethod { get; set; }

        public override IqlExpression ToQueryStringTyped<TEntity>(IqlDataSetQueryExpression action, DotNetIqlParserInstance parser)
        {
            var filter = parser.Parse(action.Filter);
            var orderBys = action.OrderBys?.Select(parser.Parse);
            var expands = action.Expands?.Select(parser.Parse);
            Expression body = parser.ContextParameter;
            var expandsArray = expands?.ToArray();
            if (expandsArray != null && expandsArray.Any())
            {
                foreach (var expand in expandsArray)
                {
                    body = parser.Chain<TEntity>(
                        body,
                        e =>
                        e.Run((MethodCallExpression)expand.Expression));
                }
            }

            if (filter.Expression != null)
            {
                body = parser.Chain<TEntity>(
                    body,
                    e =>
                    e.Where((Expression<Func<TEntity, bool>>)filter.ToLambda()));
                //body = Expression.Call(EnumerableWhereMethod.MakeGenericMethod(parser.RootEntityType),
                //    parser.WithContext<TEntity, IList<TEntity>>(e => e.SourceList),
                //    filter.ToLambda());
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