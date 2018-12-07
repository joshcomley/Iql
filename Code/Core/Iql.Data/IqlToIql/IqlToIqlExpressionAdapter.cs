using System;
using Iql.Data.IqlToIql.Parsers;
using Iql.Entities;
using Iql.Parsing;

namespace Iql.Data.IqlToIql
{
    public class IqlToIqlExpressionAdapter : IqlExpressionAdapter<IqlToIqlIqlData>
    {
        public IEntityConfigurationBuilder EntityConfigurationContext { get; }

        public IEntityConfiguration ResolveEntityConfiguration(Type entityType)
        {
            return EntityConfigurationContext.GetEntityByType(entityType);
        }

        public IqlToIqlExpressionAdapter(IEntityConfigurationBuilder entityConfigurationContext)
        {
            EntityConfigurationContext = entityConfigurationContext;
            //Registry.Register(typeof(IqlExpression), () => new JavaScriptActionParser());
            Registry.Register(typeof(IqlLiteralExpression), () => new IqlToIqlLiteralParser());
            Registry.Register(typeof(IqlSpecialValueExpression), () => new IqlToIqlSpecialValueParser());
            Registry.Register(typeof(IqlIntersectsExpression), () => new IqlToIqlIntersectsParser());
            Registry.Register(typeof(IqlLambdaExpression), () => new IqlToIqlLambdaParser());
            Registry.Register(typeof(IqlPropertyExpression), () => new IqlToIqlPropertyParser());
            Registry.Register(typeof(IqlAggregateExpression), () => new IqlToIqlAggregateParser());
            Registry.Register(typeof(IqlBinaryExpression), () => new IqlToIqlBinaryParser());
            Registry.Register(typeof(IqlNavigationExpression), () => new IqlToIqlNavigationParser());
            Registry.Register(typeof(IqlCollectitonQueryExpression), () => new IqlToIqlCollectitonQueryParser());
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

        public override IqlToIqlIqlData NewData()
        {
            return new IqlToIqlIqlData();
        }
    }
}