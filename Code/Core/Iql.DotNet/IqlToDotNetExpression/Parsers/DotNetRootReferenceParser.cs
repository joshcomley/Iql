﻿using System.Linq.Expressions;

namespace Iql.DotNet.IqlToDotNetExpression.Parsers
{
    public class DotNetRootReferenceParser : DotNetActionParserBase<IqlRootReferenceExpression>
    {
        public override IqlExpression ToQueryString(IqlRootReferenceExpression action,
            DotNetIqlParserContext parser)
        {
            var parameter = parser.GetParameterExpression(action.VariableName);
            IqlExpression expression =
                new IqlFinalExpression<Expression>(
                    parameter
                );
            return expression;
        }
    }
}