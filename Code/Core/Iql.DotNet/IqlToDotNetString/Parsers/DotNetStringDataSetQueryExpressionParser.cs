using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Iql.DotNet.IqlToDotNetExpression.Parsers
{
    public class DotNetStringDataSetQueryExpressionParser : DotNetActionParserBase<IqlCollectitonQueryExpression>
    {
        static DotNetStringDataSetQueryExpressionParser()
        {
            EnumerableWhereMethod = typeof(Enumerable)
                .GetMethods().FirstOrDefault(m => m.Name == nameof(Enumerable.Where));
        }

        internal static MethodInfo EnumerableWhereMethod { get; set; }

        public override IqlExpression ToQueryStringTyped<TEntity>(IqlCollectitonQueryExpression action, DotNetIqlParserInstance parser)
        {
            throw new NotImplementedException();
        }
    }
}