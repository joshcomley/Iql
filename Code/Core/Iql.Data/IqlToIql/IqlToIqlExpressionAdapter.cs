using System;
using Iql.Data.IqlToIql.Parsers;
using Iql.Entities;
using Iql.Parsing;
using Iql.Parsing.Types;

namespace Iql.Data.IqlToIql
{
    public class IqlToIqlExpressionAdapter : AsyncIqlExpressionAdapter<IqlToIqlIqlData>
    {
        public IqlToIqlExpressionAdapter(ITypeResolver typeResolver)
        {
            TypeResolver = typeResolver;
            //Registry.Register(typeof(IqlExpression), () => new JavaScriptActionParser());
            Registry.Register(typeof(IqlLiteralExpression), () => new IqlToIqlLiteralParser());
            //Registry.Register(typeof(IqlDistanceExpression), () => new IqlToIqlDistanceParser());
            //Registry.Register(typeof(IqlVariableExpression), () => new IqlToIqlVariableParser());
            Registry.Register(typeof(IqlSpecialValueExpression), () => new IqlToIqlSpecialValueParser());
            Registry.Register(typeof(IqlIntersectsExpression), () => new IqlToIqlIntersectsParser());
            Registry.Register(typeof(IqlLambdaExpression), () => new IqlToIqlLambdaParser());
            Registry.Register(typeof(IqlPropertyExpression), () => new IqlToIqlPropertyParser());
            Registry.Register(typeof(IqlAggregateExpression), () => new IqlToIqlAggregateParser());
            Registry.Register(typeof(IqlBinaryExpression), () => new IqlToIqlBinaryParser());
            Registry.Register(typeof(IqlNavigationExpression), () => new IqlToIqlNavigationParser());
            Registry.Register(typeof(IqlCollectionQueryExpression), () => new IqlToIqlCollectitonQueryParser());
            Registry.Register(typeof(IqlDataSetQueryExpression), () => new IqlToIqlDataSetQueryParser());
            Registry.Register(typeof(IqlExpandExpression), () => new IqlToIqlExpandParser());
            Registry.Register(typeof(IqlExpression), () => new IqlToIqlParser());
            Registry.Register(typeof(IqlNotExpression), () => new IqlToIqlNotParser());
            Registry.Register(typeof(IqlOrderByExpression), () => new IqlToIqlOrderByParser());
            Registry.Register(typeof(IqlParenthesisExpression), () => new IqlToIqlParenthesisParser());
            Registry.Register(typeof(IqlParentValueExpression), () => new IqlToIqlParentValueParser());
            Registry.Register(typeof(IqlStringSubStringExpression), () => new IqlToIqlStringSubStringParser());
            Registry.Register(typeof(IqlWithKeyExpression), () => new IqlToIqlWithKeyParser());
        }

        public ITypeResolver TypeResolver { get; set; }

        public override IqlToIqlIqlData NewData()
        {
            return new IqlToIqlIqlData();
        }
    }
}