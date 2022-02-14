using System;
using System.Linq;
using System.Reflection;
using Iql.DotNet.IqlToDotNetExpression;
using Iql.DotNet.IqlToDotNetExpression.Parsers;

namespace Iql.DotNet.IqlToDotNetString.Parsers
{
    public class DotNetStringDataSetQueryExpressionParser : DotNetActionParserBase<IqlCollectionQueryExpression>
    {
        static DotNetStringDataSetQueryExpressionParser()
        {
            EnumerableWhereMethod = typeof(Enumerable)
                .GetMethods().FirstOrDefault(m => m.Name == nameof(Enumerable.Where));
        }

        internal static MethodInfo EnumerableWhereMethod { get; set; }

        public override IqlExpression ToQueryStringTyped<TEntity>(IqlCollectionQueryExpression action, DotNetIqlParserContext parser)
        {
            throw new NotImplementedException();
        }
    }
}