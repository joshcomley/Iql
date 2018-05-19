using System;
using System.Linq;
using System.Reflection;
using Iql.DotNet.IqlToDotNetExpression;
using Iql.DotNet.IqlToDotNetExpression.Parsers;

namespace Iql.DotNet.IqlToDotNetString.Parsers
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