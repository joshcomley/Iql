﻿using System.Collections.Generic;
using System.Linq;
using Iql.Data.DataStores.InMemory;
using Iql.Data.Queryable;
using Iql.JavaScript.Extensions;
using Iql.Queryable;
using Newtonsoft.Json;

namespace Iql.JavaScript.IqlToJavaScriptExpression.Parsers
{
    public class JavaScriptExpandQueryParser : JavaScriptActionParserBase<IqlExpandExpression>
    {
        public override IqlExpression ToQueryStringTyped<TEntity>(IqlExpandExpression action, JavaScriptIqlParserContext parser)
        {
            return new IqlFinalExpression<string>(
                $".{nameof(InMemoryContext<TEntity>.Expand)}({action.SerializeDeserialize()})"
            );
        }
    }

    public class JavaScriptOrderByQueryParser : JavaScriptActionParserBase<IqlOrderByExpression>
    {
        public override IqlExpression ToQueryStringTyped<TEntity>(IqlOrderByExpression action, JavaScriptIqlParserContext parser)
        {
            var js = parser.Parse(action.OrderExpression).ToCodeString();
            return new IqlFinalExpression<string>(
                $".{nameof(InMemoryContext<TEntity>.OrderBy)}({js}, {(action.Descending ? "true" : "false")})"
            );
        }
    }

    public class JavaScriptWithKeyQueryParser : JavaScriptActionParserBase<IqlWithKeyExpression>
    {
        public override IqlExpression ToQueryStringTyped<TEntity>(IqlWithKeyExpression action, JavaScriptIqlParserContext parser)
        {
            var iqlAnd = action.KeyEqualToExpressions.And();
            var javaScriptOutput = parser.Parse(iqlAnd);
            var js = javaScriptOutput.ToCodeString();
            return new IqlFinalExpression<string>(
                $".{nameof(InMemoryContext<TEntity>.Where)}(function({javaScriptOutput.RootEntityParameterName}) {{ return {js}; }})"
            );
        }
    }

    public class JavaScriptCollectionQueryParser : JavaScriptActionParserBase<IqlCollectitonQueryExpression>
    {
        public override IqlExpression ToQueryStringTyped<TEntity>(IqlCollectitonQueryExpression action, JavaScriptIqlParserContext parser)
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
            return new IqlFinalExpression<string>($"function(context) {{ return context{query}; }}");
        }
    }
}