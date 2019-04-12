using System.Collections.Generic;
using System.Linq;
using System.Text;
using Iql.OData.Extensions;

namespace Iql.OData.IqlToODataExpression.Parsers
{
    public class ODataCollectionQueryActionParser : ODataActionParserBase<IqlCollectitonQueryExpression>
    {
        public override IqlExpression ToQueryString(IqlCollectitonQueryExpression action, ODataIqlParserContext parser)
        {
            var odataParts = new List<string>();
            var filter = action.Filter == null ? null : parser.Parse(action.Filter).ToCodeString();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                odataParts.Add($"$filter={parser.EncodeIfNecessary(filter)}");
            }

            var expands = new List<string>();
            if (action.Expands != null && action.Expands.Any())
            {
                for (var i = 0; i < action.Expands.Count; i++)
                {
                    var expand = action.Expands[i];
                    var codeString = parser.Parse(expand).ToCodeString();
                    if (!string.IsNullOrWhiteSpace(codeString))
                    {
                        expands.Add(codeString);
                    }
                }
            }

            if (expands.Any())
            {
                odataParts.Add($"$expand={parser.EncodeIfNecessary(string.Join(",", expands))}");
            }

            var orderBys = new List<string>();
            if (action.OrderBys != null && action.OrderBys.Any())
            {
                for (var i = 0; i < action.OrderBys.Count; i++)
                {
                    var orderBy = action.OrderBys[i];
                    var orderByExpression = parser.Parse(orderBy).ToCodeString();
                    if (!orderBys.Contains(orderByExpression))
                    {
                        orderBys.Add(orderByExpression);
                    }
                }
            }

            if (orderBys.Any())
            {
                odataParts.Add($"$orderby={parser.EncodeIfNecessary(string.Join(",", orderBys))}");
            }

            if (action.Skip.HasValue && action.Skip > 0)
            {
                odataParts.Add($"$skip={action.Skip}");
            }

            if (action.Take.HasValue && action.WithKey == null)
            {
                odataParts.Add($"$top={action.Take}");
            }

            if (action.IncludeCount.HasValue && action.WithKey == null)
            {
                odataParts.Add($"$count={(action.IncludeCount.Value ? "true" : "false")}");
            }

            var query = string.Join(parser.Nested ? ";" : "&", odataParts);
            if (!parser.Nested && !string.IsNullOrWhiteSpace(query))
            {
                query = $"?{query}";
            }
            if (action.WithKey != null)
            {
                var withKey = parser.Parse(action.WithKey).ToCodeString();
                query = $"({withKey}){query}";
            }

            if (query == "?")
            {
                query = "";
            }

            string baseUri = null;
            if (action is IqlDataSetQueryExpression)
            {
                var dataSetQuery = action as IqlDataSetQueryExpression;
                if (dataSetQuery.DataSet != null && !string.IsNullOrWhiteSpace(dataSetQuery.DataSet.Name))
                {
                    baseUri = parser.Converter.Configuration.ResolveEntitySetUriByType(parser.CurrentEntityType, dataSetQuery.DataSet.Name);
                }
            }
            if (string.IsNullOrWhiteSpace(baseUri))
            {
                baseUri = parser.Converter.Configuration.ResolveEntitySetUriByType(parser.CurrentEntityType);
            }
            if (!parser.Nested && !string.IsNullOrWhiteSpace(baseUri))
            {
                query = $"{baseUri}{query}";
            }
            return new IqlFinalExpression<string>(query);
        }
    }
}