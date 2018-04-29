﻿using System.Linq.Expressions;
using Iql.Parsing;

namespace Iql.DotNet.IqlToDotNetExpression.Parsers
{
    public class DotNetActionParserBase<TIqlExpression> :
        ActionParser<
            TIqlExpression,
            DotNetIqlData,
            DotNetIqlExpressionAdapter,
            Expression,
            DotNetOutput,
            DotNetIqlParserInstance,
            DotNetExpressionConverter>
        where TIqlExpression : IqlExpression { }
}