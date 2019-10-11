using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using Iql.Entities;
using Iql.Extensions;
using Iql.JavaScript.JavaScriptExpressionToExpressionTree;
using Iql.JavaScript.JavaScriptExpressionToExpressionTree.Nodes;

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
                var root = parent;
                while (root != null && root.Kind != IqlExpressionKind.RootReference)
                {
                    root = root.Parent;
                }

                var rootReference = root as IqlRootReferenceExpression;
                PropertyInfo property;
                var rootEntityType = rootReference == null
                    ? null
                    : context.RootEntities.Last(r => r.Name == rootReference.VariableName).Type;
                if (rootEntityType != null)
                {
                    switch (parent.Kind)
                    {
                        case IqlExpressionKind.RootReference:
                            //entityConfiguration = instance.EntityConfigurationContext.GetEntity<T>();
                            property = rootEntityType.GetProperty(expression.Name);
                            propertyType = property.PropertyType;
                            break;
                        case IqlExpressionKind.Variable:
                            //var variableParent = parent as IqlVariableExpression;
                            //debugger;
                            break;
                        case IqlExpressionKind.Property:
                            var propertyParent = parent as IqlPropertyExpression;
                            property = rootEntityType.GetProperty(propertyParent.PropertyName);
                            if (property == null)
                            {
                                throw new Exception(
                                    $"No property \"{expression.Name}\" found on type \"{rootEntityType.Name}\"");
                            }
                            propertyType = property.PropertyType;
                            break;
                    }
                }

                if (propertyType == null)
                {
                    var resolvedType = rootEntityType == null || context.TypeResolver == null ? null : context.TypeResolver.FindTypeByType(rootEntityType);
                    if (resolvedType != null)
                    {
                        ITypeProperty configuredProperty = null;
                        if (parent is IqlPropertyExpression)
                        {
                            var path = IqlPropertyPath.FromPropertyExpression(
                                context.TypeResolver,
                                resolvedType,
                                parent as IqlPropertyExpression,
                                false);
                            if (path != null && path.Property != null)
                            {
                                configuredProperty = path.Property;
                            }
                        }
                        else if (parent is IqlRootReferenceExpression)
                        {
                            configuredProperty = resolvedType.FindProperty(expression.Name);
                        }
                        if (configuredProperty != null)
                        {
                            propertyType = configuredProperty.Type;
                            if (configuredProperty.IsCollection)
                            {
                                propertyType = typeof(IEnumerable);
                            }
                        }
                    }
                }

                if (propertyType != null && propertyType.IsEnumerableType() && expression.Name == "length")
                {
                    exp = new IqlCountExpression("", parent as IqlReferenceExpression, null);
                }
                else
                {
                    exp = new IqlPropertyExpression(expression.Name, null, propertyType.ToIqlType());
                }
                //instance.entityConfigurationContext.entity()
            }
            else
            {
                var variableName = expression.Name;
                object value = null;
                var isRootEntity = false;
                var rootEntityMatch = context.RootEntities.LastOrDefault(re => re.Name == variableName);
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
                        evaluateValue,
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