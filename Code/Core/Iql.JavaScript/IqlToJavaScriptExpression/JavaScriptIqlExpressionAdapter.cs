using System;
using Iql.Entities;
using Iql.JavaScript.IqlToJavaScriptExpression.Parsers;
using Iql.Parsing;

namespace Iql.JavaScript.IqlToJavaScriptExpression
{
    public class JavaScriptIqlExpressionAdapter : IqlExpressionAdapter<JavaScriptIqlData>
    {
        protected IEntityConfigurationBuilder EntityConfigurationContext { get; }

        public IEntityConfigurationBuilder ResolveEntityConfigurationBuilder(Type entityType)
        {
            return EntityConfigurationContext ??
                   EntityConfigurationBuilder.FindConfigurationBuilderForEntityType(entityType);
        }

        public JavaScriptIqlExpressionAdapter(IEntityConfigurationBuilder entityConfigurationContext = null)
        {
            EntityConfigurationContext = entityConfigurationContext;
            //Registry.Register(typeof(IqlExpression), () => new JavaScriptActionParser());
            Registry.Register(typeof(IqlExpression), () => new JavaScriptStringSourceActionParser());
            Registry.Register(typeof(IqlInvocationExpression), () => new JavaScriptInvocationParser());
            Registry.Register(typeof(IqlVariableExpression), () => new JavaScriptVariableActionParser());
            Registry.Register(typeof(IqlConditionExpression), () => new JavaScriptConditionActionParser());
            Registry.Register(typeof(IqlLambdaExpression), () => new JavaScriptLambdaActionParser());
            Registry.Register(typeof(IqlWithKeyExpression), () => new JavaScriptWithKeyQueryParser());
            Registry.Register(typeof(IqlExpandExpression), () => new JavaScriptExpandQueryParser());
            Registry.Register(typeof(IqlOrderByExpression), () => new JavaScriptOrderByQueryParser());
            Registry.Register(typeof(IqlCollectitonQueryExpression), () => new JavaScriptCollectionQueryParser());
            Registry.Register(typeof(IqlNotExpression), () => new JavaScriptNotActionParser());
            Registry.Register(typeof(IqlParenthesisExpression), () => new JavaScriptParenthesisParser());
            Registry.Register(typeof(IqlPropertyExpression), () => new JavaScriptPropertyReferenceParser());
            Registry.Register(typeof(IqlRootReferenceExpression), () => new JavaScriptRootReferenceParser());
            Registry.Register(typeof(IqlLiteralExpression), () => new JavaScriptLiteralParser());
            Registry.Register(typeof(IqlStringSubStringExpression),
                () => new JavaScriptStringSubStringActionParser());
            Registry.Register(typeof(IqlParentValueExpression),
                () => new JavaScriptStringSourceValueActionParser());
            Registry.Register(typeof(IqlStringLengthExpression),
                () => new JavaScriptStringLengthParser());
            Registry.Register(typeof(IqlUnaryExpression), () => new JavaScriptUnaryActionParser());
            Registry.Register(typeof(IqlBinaryExpression), () => new JavaScriptBinaryActionParser());
            Registry.Register(typeof(IqlCountExpression), () => new JavaScriptCountActionParser());
            Registry.Register(typeof(IqlAnyAllExpression), () => new JavaScriptAnyAllActionParser());
        }

        public override JavaScriptIqlData NewData()
        {
            return new JavaScriptIqlData();
        }
    }
}