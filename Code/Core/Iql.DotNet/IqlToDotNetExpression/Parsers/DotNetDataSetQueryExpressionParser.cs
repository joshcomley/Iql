using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Iql.DotNet.IqlToDotNetExpression.Parsers
{
    public class DotNetDataSetQueryExpressionParser : DotNetActionParserBase<IqlDataSetQueryExpression>
    {
        static DotNetDataSetQueryExpressionParser()
        {
            EnumerableWhereMethod = typeof(System.Linq.Enumerable)
                .GetMethods().FirstOrDefault(m => m.Name == nameof(System.Linq.Enumerable.Where));
        }

        internal static MethodInfo EnumerableWhereMethod { get; set; }

        public override IqlExpression ToQueryString(IqlDataSetQueryExpression action, DotNetIqlParserInstance parser)
        {
            var filter = parser.Parse(action.Filter);
            var orderBys = action.OrderBys?.Select(parser.Parse);
            var expands = action.Expands?.Select(parser.Parse);
            var queryParam = Expression.Parameter(typeof(IList<>).MakeGenericType(parser.RootEntityType), parser.RootEntity.Name + "_query");
            Expression body = queryParam;
            if (filter != null)
            {
                body = Expression.Call(EnumerableWhereMethod.MakeGenericMethod(parser.RootEntityType), queryParam, 
                    filter.ToLambda());
            }
            var lambda = Expression.Lambda(body, queryParam);
            parser.ConvertToLambda = false;
            return new IqlFinalExpression<Expression>(lambda);
        }
    }
}