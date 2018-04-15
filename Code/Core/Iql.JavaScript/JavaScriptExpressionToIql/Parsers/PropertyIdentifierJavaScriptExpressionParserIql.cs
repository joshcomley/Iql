using System;
using System.Linq;
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
            JavaScriptExpressionNodeParseContext<T> context,
            PropertyIdentifierJavaScriptExpressionNode expression)
        {
            IqlExpression exp = null;
            if (context.ObjectStack().Count > 0)
            {
                Type propertyType = null;
                var parent = context.Parent() as IqlExpression;
                PropertyInfo property;
                switch (parent.Type)
                {
                    case IqlExpressionType.RootReference:
                        //entityConfiguration = instance.EntityConfigurationContext.GetEntity<T>();
                        property = typeof(T).GetProperty(expression.Name);
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
                                $"No property \"{expression.Name}\" found on type \"{typeof(T).Name}\"");
                        }
                        propertyType = property.PropertyType;
                        // var p = new propertyParent.entityType();
                        // var x = p[instance.expression.name].constructor;
                        break;
                }
                //instance.entityConfigurationContext.entity()
                exp = new IqlPropertyExpression(expression.Name, null, propertyType.ToIqlType());
            }
            else
            {
                var variableName = expression.Name;
                object value = null;
                var isRootEntity = false;
                var rootEntityMatch = context.RootEntities.SingleOrDefault(re => re.Name == variableName);
                if (rootEntityMatch != null)
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
                        rootEntityMatch.Type);
                    exp.Parent = rootEntityMatch.Path;
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