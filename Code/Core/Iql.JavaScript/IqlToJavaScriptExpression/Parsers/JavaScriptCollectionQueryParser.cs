using System.Collections.Generic;
using System.Linq;
using Iql.Data.DataStores.InMemory;
using Iql.JavaScript.Extensions;

namespace Iql.JavaScript.IqlToJavaScriptExpression.Parsers
{
    public class JavaScriptCollectionQueryParser : JavaScriptActionParserBase<IqlCollectionQueryExpression>
    {
        public override IqlExpression ToQueryStringTyped<TEntity>(IqlCollectionQueryExpression action, JavaScriptIqlParserContext parser)
        {
            var queryParts = new List<string>();
            if (action.Filter != null)
            {
                var filter = parser.Parse(action.Filter).ToCodeString();
                if (!string.IsNullOrWhiteSpace(filter))
                {
                    var filterCall = $".{nameof(InMemoryContext<TEntity>.Where)}({filter}, {action.Filter.SerializeDeserialize()})";
                    queryParts.Add(filterCall);
                }
            }
            var expands = parser.ParseAll(action.Expands)
                .Select(s => s.ToCodeString());
            var orderBys = parser.ParseAll(action.OrderBys)
                .Select(s => s.ToCodeString());
            queryParts.AddRange(expands);
            queryParts.AddRange(orderBys);
            if (action.WithKey != null)
            {
                var withKey = parser.Parse(action.WithKey).ToCodeString();
                if (!string.IsNullOrWhiteSpace(withKey))
                {
                    queryParts.Add(withKey);
                }
            }
            var query = string.Join("\n\t\t", queryParts);
            return new IqlFinalExpression<string>($"function(contextWrapper) {{ return contextWrapper{query}; }}");
        }
    }
}