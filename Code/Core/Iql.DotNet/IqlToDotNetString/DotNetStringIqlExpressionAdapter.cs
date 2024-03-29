﻿using Iql.DotNet.IqlToDotNetExpression.Parsers;
using Iql.DotNet.IqlToDotNetString.Parsers;
using Iql.Parsing;

namespace Iql.DotNet.IqlToDotNetString
{
    public class DotNetStringIqlExpressionAdapter : IqlExpressionAdapter<DotNetStringIqlData>
    {
        public DotNetStringIqlExpressionAdapter(string rootVariableName)
        {
            RootVariableName = rootVariableName;
            Registry.Register(typeof(IqlNotExpression), () => new DotNetStringNotExpressionParser());
            Registry.Register(typeof(IqlVariableExpression), () => new DotNetStringVariableParser());
            Registry.Register(typeof(IqlConditionExpression), () => new DotNetStringConditionExpressionParser());
            Registry.Register(typeof(IqlCollectionQueryExpression), () => new DotNetStringDataSetQueryExpressionParser());
            Registry.Register(typeof(IqlStringTrimExpression), () => new DotNetStringStringTrimExpressionParser());
            Registry.Register(typeof(IqlLambdaExpression), () => new DotNetStringLambdaParser());
            Registry.Register(typeof(IqlPropertyExpression), () => new DotNetStringPropertyReferenceParser());
            Registry.Register(typeof(IqlRootReferenceExpression), () => new DotNetStringRootReferenceParser());
            Registry.Register(typeof(IqlEnumLiteralExpression), () => new DotNetStringEnumLiteralParser());
            Registry.Register(typeof(IqlLiteralExpression), () => new DotNetStringLiteralParser());
            Registry.Register(typeof(IqlStringSubStringExpression), () => new DotNetStringStringSubStringExpressionParser());
            Registry.Register(typeof(IqlStringLengthExpression), () => new DotNetStringStringLengthExpressionParser());
            Registry.Register(typeof(IqlBinaryExpression), () => new DotNetStringBinaryActionParser());
        }

        public string RootVariableName { get; set; }

        public override DotNetStringIqlData NewData()
        {
            return new DotNetStringIqlData();
        }
    }
}