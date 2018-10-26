using System;
using System.Collections.Generic;
using Iql.Data.DataStores.InMemory;

namespace Iql.JavaScript.IqlToJavaScriptExpression.Parsers
{
    public class JavaScriptVariableActionParser : JavaScriptActionParserBase<IqlVariableExpression>
    {
        public override IqlExpression ToQueryString(IqlVariableExpression action, JavaScriptIqlParserInstance parser)
        {
            if (parser.IsParameterName(action.VariableName))
            {
                return new IqlFinalExpression<string>(parser.GetRootEntityParameterName(action.VariableName));
            }

            var key = $"_lookup_{action.VariableName}_{Guid.NewGuid()}";
            var methodCall = $"context.GetVariable(\"{key}\")";
            GlobalContext.GlobalVariables.Add(key, action.Value);
            return new IqlFinalExpression<string>(methodCall);
        }
    }
}