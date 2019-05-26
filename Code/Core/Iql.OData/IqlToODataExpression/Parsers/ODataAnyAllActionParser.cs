using System;
using System.Collections.Generic;

namespace Iql.OData.IqlToODataExpression.Parsers
{
    public class ODataAnyAllActionParser : ODataActionParserBase<IqlAnyAllExpression>
    {
        public override IqlExpression ToQueryString(IqlAnyAllExpression action, ODataIqlParserContext parser)
        {
            string methodName = null;
            switch (action.Kind)
            {
                case IqlExpressionKind.Any:
                    methodName= "any";
                    break;
                case IqlExpressionKind.All:
                    methodName = "all";
                    break;
            }

            if (methodName == null)
            {
                ODataErrors.OperationNotSupported(action.Kind);
            }
            // https://localhost:44306/odata/Users?$filter=ScaffoldsCreated/all(d:((d/Description ne null) and (d/Description ne '')))
            var arr = new List<IqlExpression>();
            var actionRootVariableName = action.RootVariableName;
            var parsed = parser.ParseWithValidRootReferenceVariable(action.Value, actionRootVariableName).ToCodeString();
            var lambdaIsEmpty = string.IsNullOrWhiteSpace(parsed);
            if (!lambdaIsEmpty || action.Kind == IqlExpressionKind.Any)
            {
                arr.Add(action.Parent);
                if (string.IsNullOrWhiteSpace(actionRootVariableName))
                {
                    throw new ArgumentException($"A {nameof(IqlAnyAllExpression.RootVariableName)} is required for any/all operations in OData");
                }
                arr.Add(new IqlFinalExpression<string>($"/{methodName}("));
                if (!string.IsNullOrWhiteSpace(parsed))
                {
                    arr.Add(new IqlFinalExpression<string>($"{actionRootVariableName}:"));
                }
                arr.Add(new IqlFinalExpression<string>(parsed));
                arr.Add(new IqlFinalExpression<string>(")"));
            }
            return new IqlAggregateExpression(arr.ToArray());
        }
    }
}