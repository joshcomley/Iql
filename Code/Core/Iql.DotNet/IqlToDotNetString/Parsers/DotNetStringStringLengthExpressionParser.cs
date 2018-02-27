﻿using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Iql.DotNet.IqlToDotNetString.Parsers
{
    public class DotNetStringStringLengthExpressionParser : DotNetStringActionParserBase<IqlStringLengthExpression>
    {
        public override IqlExpression ToQueryString(IqlStringLengthExpression action,
            DotNetStringIqlParserInstance parser)
        {
            var parentExpression = parser.Parse(action.Parent
#if TypeScript
                        , null
#endif
            ).Expression;
            IqlExpression expression =
                new IqlFinalExpression<string>(
                    $"{parentExpression}.Length"
                );
            return expression;
        }
    }
}