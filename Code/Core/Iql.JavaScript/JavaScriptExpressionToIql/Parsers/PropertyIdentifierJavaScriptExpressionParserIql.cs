using System;
using System.Reflection;
using Iql.Extensions;
using Iql.JavaScript.JavaScriptExpressionToExpressionTree;
using Iql.JavaScript.JavaScriptExpressionToExpressionTree.Nodes;
using Iql.Queryable.Data.EntityConfiguration;

namespace Iql.JavaScript.JavaScriptExpressionToIql.Parsers
{
    public class PropertyIdentifierJavaScriptExpressionParserIql<T> :
        IqlQueryJavaScriptExpressionParser<T, PropertyIdentifierJavaScriptExpressionNode>
        where T : class
    {
        public override IqlParseResult Parse(
            JavaScriptExpressionNodeParseContext<T, PropertyIdentifierJavaScriptExpressionNode> context)
        {
            IqlExpression exp = null;
            if (context.ObjectStack().Count > 0)
            {
                Type propertyType = null;
                var parent = context.Parent() as IqlExpression;
                IEntityConfiguration entityConfiguration = null;
                PropertyInfo property;
                switch (parent.Type)
                {
                    case IqlExpressionType.RootReference:
                        //entityConfiguration = instance.EntityConfigurationContext.GetEntity<T>();
                        property = typeof(T).GetProperty(context.Expression.Name);
                        propertyType = property.PropertyType;
                        break;
                    case IqlExpressionType.Variable:
                        //debugger;
                        break;
                    case IqlExpressionType.Property:
                        var propertyParent = parent as IqlPropertyExpression;
                        property = typeof(T).GetProperty(propertyParent.PropertyName);
                        if (property == null)
                        {
                            throw new Exception(
                                $"No property \"{context.Expression.Name}\" found on type \"{typeof(T).Name}\"");
                        }
                        propertyType = property.PropertyType;
                        // var p = new propertyParent.entityType();
                        // var x = p[instance.expression.name].constructor;
                        break;
                }
                //instance.entityConfigurationContext.entity()
                exp = new IqlPropertyExpression(context.Expression.Name, null, propertyType.ToIqlType());
            }
            else
            {
                var variableName = context.Expression.Name;
                object value = null;
                var isRootEntity = false;
                if (variableName == context.RootEntityVariableName)
                {
                    isRootEntity = true;
                    value = context.RootEntity;
                }
                if (variableName == "_this" || variableName == "this")
                {
                    value = context.EvaluateContext.Context;
                }
                if (isRootEntity)
                {
                    exp = new IqlRootReferenceExpression(
                        variableName,
                        value?.ToString(),
                        typeof(T));
                }
                else
                {
                    var evaluateValue = context.Evaluate(variableName);
                    exp = new IqlVariableExpression(
                        variableName,
                        evaluateValue.ToString(),
                        evaluateValue != null ? evaluateValue.GetType() : null
                    );
                }
            }
            return new IqlParseResult(
                exp
            );
        }
    }
}