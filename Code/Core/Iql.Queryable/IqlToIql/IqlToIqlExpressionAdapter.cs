using System;
using Iql.JavaScript.IqlToJavaScriptExpression;
using Iql.Parsing;
using Iql.Queryable.Data.EntityConfiguration;

namespace Iql.Queryable.IqlToIql
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