using Iql.Entities;
using Iql.JavaScript.JavaScriptExpressionToExpressionTree;
using Iql.JavaScript.JavaScriptExpressionToExpressionTree.Nodes;

namespace Iql.JavaScript.JavaScriptExpressionToIql.Parsers
{
    public class MemberJavaScriptExpressionParserIql<T> : IqlQueryJavaScriptExpressionParser<T,
        MemberJavaScriptExpressionNode>
        where T : class
    {
        public override IqlParseResult Parse(
            JavaScriptExpressionNodeParseContext<T> context,
            MemberJavaScriptExpressionNode expression)
        {
            var owner = context.Parse(expression.Owner).Value;
            if (owner is IqlBinaryExpression)
            {
                var binary = owner as IqlBinaryExpression;
                if (binary.Left is IqlCountExpression || binary.Right is IqlCountExpression)
                {
                    var iqlParseResult = new IqlParseResult(owner);
                    iqlParseResult.ReplaceParent = true;
                    return iqlParseResult;
                }
            }
            var property = context.ParseWith(expression.Property, owner).Value;
            property.Parent = owner;
            // We need to determine the type of the property, if possible, using IqlPropertyPath
            var entityConfiguration = EntityConfigurationBuilder.FindConfigurationForEntityType(context.CurrentRootEntity.Type);
            if (entityConfiguration != null && property.Kind == IqlExpressionKind.Property)
            {
                var path = IqlPropertyPath.FromPropertyExpression(entityConfiguration,
                    property as IqlPropertyExpression);
                if (path != null && path.Property != null)
                {
                    property.ReturnType = path.Property.TypeDefinition.Kind;
                }
            }
            return new IqlParseResult(property);
        }
    }
}