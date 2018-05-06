using System.Collections.Generic;
using System.Linq;
using System.Text;
using Iql.OData.Extensions;
using Iql.Queryable.Data;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Expressions;

namespace Iql.OData.IqlToODataExpression.Parsers
{
    public class ODataCollectionQueryActionParser : ODataActionParserBase<IqlCollectitonQueryExpression>
    {
        public override IqlExpression ToQueryString(IqlCollectitonQueryExpression action, ODataIqlParserInstance parser)
        {
            var odataParts = new List<string>();
            var filter = action.Filter == null ? null : parser.Parse(action.Filter).ToCodeString();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                odataParts.Add($"$filter={filter}");
            }

            var expands = new List<string>();
            if (action.Expands != null && action.Expands.Any())
            {
                for (var i = 0; i < action.Expands.Count; i++)
                {
                    var expand = action.Expands[i];
                    var codeString = parser.ParseNested(expand).ToCodeString();
                    if (!string.IsNullOrWhiteSpace(codeString))
                    {
                        expands.Add(codeString);
                    }
                }
            }

            if (expands.Any())
            {
                odataParts.Add($"$expand={string.Join(",", expands)}");
            }

            var orderBys = new List<string>();
            if (action.OrderBys != null && action.OrderBys.Any())
            {
                for (var i = 0; i < action.OrderBys.Count; i++)
                {
                    var orderBy = action.OrderBys[i];
                    orderBys.Add(parser.Parse(orderBy).ToCodeString());
                }
            }

            if (orderBys.Any())
            {
                odataParts.Add($"$orderby={string.Join(",", orderBys)}");
            }

            if (action.Skip.HasValue && action.Skip > 0)
            {
                odataParts.Add($"$skip={action.Skip}");
            }

            if (action.Take.HasValue)
            {
                odataParts.Add($"$top={action.Take}");
            }

            if (action.IncludeCount.HasValue)
            {
                odataParts.Add($"$count={(action.IncludeCount.Value ? "true" : "false")}");
            }

            var query = string.Join(parser.Nested ? ";" : "&", odataParts);
            if (!parser.Nested)
            {
                query = $"?{query}";
            }
            if (action.WithKey != null)
            {
                var withKey = parser.ParseNested(action.WithKey).ToCodeString();
                query = $"({withKey}){query}";
            }

            if (query == "?")
            {
                query = "";
            }
            var baseUri = parser.Converter.Configuration.ResolveEntitySetUriByType(parser.RootEntityType);
            if (!parser.Nested && !string.IsNullOrWhiteSpace(query) && !string.IsNullOrWhiteSpace(baseUri))
            {
                query = $"{baseUri}{query}";
            }
            return new IqlFinalExpression<string>(query);
        }
    }
    public class ODataWithKeyActionParser : ODataActionParserBase<IqlWithKeyExpression>
    {
        public override IqlExpression ToQueryString(IqlWithKeyExpression action, ODataIqlParserInstance parser)
        {
            var compositeKey = new CompositeKey(action.KeyEqualToExpressions.Count);
            for (var i = 0; i < action.KeyEqualToExpressions.Count; i++)
            {
                
                var keyPart = action.KeyEqualToExpressions[i];
                var propertyExpressionIsLeft = keyPart.Left is IqlPropertyExpression;
                var left = propertyExpressionIsLeft
                    ? keyPart.Left as IqlPropertyExpression
                    : keyPart.Right as IqlPropertyExpression;
                var right = propertyExpressionIsLeft
                    ? keyPart.Right
                    : keyPart.Left;
                compositeKey.Keys[i] = new KeyValue(parser.ParseNested(left).ToCodeString(),
                    parser.ParseNested(right).ToCodeString(),
                    null);
            }

            if (compositeKey.Keys.Length == 1)
            {
                return new IqlFinalExpression<string>(compositeKey.Keys[0].Value.ToString());
            }

            var parts = new List<string>();
            for (var i = 0; i < compositeKey.Keys.Length; i++)
            {
                var part = compositeKey.Keys[i];
                parts.Add($"{part.Name}={part.Value}");
            }

            return new IqlFinalExpression<string>(string.Join(",", parts));
        }
    }
    public class ODataExpandActionParser : ODataActionParserBase<IqlExpandExpression>
    {
        public override IqlExpression ToQueryString(IqlExpandExpression action, ODataIqlParserInstance parser)
        {
            var expandProperty = parser.ParseNested(action.NavigationProperty).ToCodeString();
            if (action.Query != null)
            {
                var expandDetails = parser.Parse(action.Query).ToCodeString();
                if (!string.IsNullOrWhiteSpace(expandDetails))
                {
                    expandProperty = $"{expandProperty}({expandDetails})";
                }
            }

            if (action.Count)
            {
                expandProperty = $"{expandProperty}/$count";
            }

            if (parser.Data.Expands.ContainsKey(expandProperty))
            {
                return null;
            }
            parser.Data.Expands.Add(expandProperty, expandProperty);
            return new IqlFinalExpression<string>(
                expandProperty);
        }
    }
    public class ODataOrderByActionParser : ODataActionParserBase<IqlOrderByExpression>
    {
        public override IqlExpression ToQueryString(IqlOrderByExpression action, ODataIqlParserInstance parser)
        {
            var orderBy = parser.Parse(action.OrderExpression).ToCodeString();
            if (action.Descending)
            {
                orderBy = $"{orderBy} desc";
            }
            return new IqlFinalExpression<string>(
                orderBy);
        }
    }
}