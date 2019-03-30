﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Iql.Conversion;
using Iql.Entities.Extensions;
using Iql.Extensions;
using Iql.Parsing.Types;
using Iql.Serialization;

namespace Iql.Entities
{
    public class IqlPropertyPath
    {
        public bool IsEmpty => string.IsNullOrWhiteSpace(PathToHere);

        private IqlPropertyPath(ITypeResolver typeResolver, ITypeProperty property, IqlPropertyExpression expression, IqlPropertyPath parent)
        {
            TypeResolver = typeResolver;
            Property = property;
            Expression = expression;
            Parent = parent;

            if (parent != null)
            {
                parent.Child = this;
            }

            var list = new List<IqlPropertyPath>();
            while (parent != null)
            {
                list.Add(parent);
                parent = parent.Parent;
            }

            var reversed = new List<IqlPropertyPath>();
            for (var i = list.Count - 1; i >= 0; i--)
            {
                reversed.Add(list[i]);
            }

            reversed.Add(this);
            PropertyPath = reversed.ToArray();
        }

        public IqlPropertyPath Top => PropertyPath == null || PropertyPath.Length == 0 ? null : PropertyPath[0];
        public IqlPropertyPath Child { get; protected set; }
        public static string DefaultSeparator => "/";
        public string Separator { get; set; } = DefaultSeparator;
        public string PathToHere => GetPathToHere(Separator);

        public IqlPropertyPath RelationshipPath
        {
            get
            {
                if (string.IsNullOrWhiteSpace(RelationshipPathToHere))
                {
                    return null;
                }

                return RelationshipPathToHere == PathToHere ? this : Parent;
            }
        }

        public string GetPathToHere(string separator)
        {
            return string.Join(separator, PropertyPath.Select(p => p.Property.PropertyName));
        }

        public string RelationshipPathToHere => GetRelationshipPathToHere(Separator);
        public string GetRelationshipPathToHere(string separator)
        {
            return string.Join(separator,
                PropertyPath.Where(p => p.Property.Kind.HasFlag(PropertyKind.Relationship))
                    .Select(p => p.Property.PropertyName));
        }

        public string PathFromHere => GetPathFromHere(Separator);

        public string GetPathFromHere(string separator)
        {
            var parts = new List<string>();
            parts.Add(PropertyName);
            var child = Child;
            while (child != null)
            {
                parts.Add(child.PropertyName);
                child = child.Child;
            }

            return string.Join(separator, parts);
        }

        public string RelationshipPathFromHere => GetRelationshipPathFromHere(Separator);

        public string GetRelationshipPathFromHere(string separator)
        {
            var parts = new List<string>();
            if (Property.Kind.HasFlag(PropertyKind.Relationship))
            {
                parts.Add(PropertyName);
                var child = Child;
                while (child != null && child.Property.Kind.HasFlag(PropertyKind.Relationship))
                {
                    parts.Add(child.PropertyName);
                    child = child.Child;
                }
            }

            return string.Join(separator, parts);
        }

        public IqlPropertyPath Parent { get; }
        public string PropertyName => Property?.PropertyName;
        public ITypeResolver TypeResolver { get; }
        public ITypeProperty Property { get; }
        public IqlPropertyExpression Expression { get; }

        public IqlPropertyPath[] PropertyPath { get; }

        public IIqlTypeMetadata EntityConfiguration { get; set; }

        public IIqlTypeMetadata PropertyEntityConfiguration =>
            Property == null || Property.EntityProperty() == null ? null : Property.EntityProperty().Relationship.OtherEnd.EntityConfiguration.TypeMetadata;

        public object GetValue(object entity)
        {
            if (entity == null)
            {
                return null;
            }

            var value = entity;
            for (var i = 0; i < PropertyPath.Length; i++)
            {
                var pathItem = PropertyPath[i];
                value = pathItem.Property.GetValue(value);
            }

            return value;
        }

        public void SetValue(object entity, object valueToSet)
        {
            if (entity == null)
            {
                return;
            }

            var value = entity;
            for (var i = 0; i < PropertyPath.Length - 1; i++)
            {
                var pathItem = PropertyPath[i];
                value = pathItem.Property.GetValue(value);
            }

            Property.SetValue(value, valueToSet);
        }

        public static IqlPropertyPath FromLambda<T>(Expression<Func<T, object>> field,
            ITypeResolver typeResolver) where T : class
        {
            var propertyExpression = IqlExpressionConversion.DefaultExpressionConverter().ConvertPropertyLambdaToIql(field, typeResolver).Expression;
            return FromPropertyExpression(typeResolver, typeResolver.FindType<T>(), propertyExpression);
        }

        public static IqlPropertyPath FromLambdaExpression(LambdaExpression field,
            ITypeResolver typeResolver,
            IIqlTypeMetadata entityConfigurationContext)
        {
            var propertyExpression = IqlExpressionConversion.DefaultExpressionConverter()
                .ConvertLambdaExpressionToIqlByType(field, typeResolver, entityConfigurationContext.Type).Expression as IqlLambdaExpression;
            return FromPropertyExpression(typeResolver, entityConfigurationContext, propertyExpression.Body as IqlPropertyExpression);
        }

        public static IqlPropertyPath FromExpression<T>(Expression<Func<T, object>> expression,
            ITypeResolver typeResolver)
        {
            return FromLambdaExpression(expression, typeResolver, typeResolver.FindType<T>());
        }

        public static IqlPropertyPath FromString(
            ITypeResolver typeResolver,
            string path,
            IIqlTypeMetadata entityConfigurationContext,
            IqlPropertyExpression parent = null,
            string rootReferenceName = null)
        {
            var propertyExpression = IqlExpression.GetPropertyExpression(path, rootReferenceName);
            if (parent != null)
            {
                propertyExpression.Parent = parent;
            }
            return FromPropertyExpression(typeResolver, entityConfigurationContext, propertyExpression);
        }

        public static IqlPropertyPath FromProperty(IProperty property)
        {
            return FromString(property.EntityConfiguration.Builder, property.Name, property.EntityConfiguration.TypeMetadata);
        }

        public static IqlPropertyPath FromPropertyExpression(
            ITypeResolver typeResolver,
            IIqlTypeMetadata entityConfigurationContext,
            IqlPropertyExpression propertyExpression,
            bool traverseNestedRootReferences = true)
        {
            IqlPropertyPath propertyPath = null;
            var list = new List<IqlPropertyExpression>();
            list.Add(propertyExpression);
            var parent = propertyExpression.Parent;
            IqlVariableExpression lastRootReference = null;
            while (parent != null && (parent.Kind == IqlExpressionKind.Property || parent.Kind == IqlExpressionKind.RootReference || parent.Kind == IqlExpressionKind.Variable))
            {
                if (parent.Kind == IqlExpressionKind.RootReference)
                {
                    var rootReference = (IqlRootReferenceExpression)parent;
                    lastRootReference = rootReference;
                    if (rootReference.Parent != null && traverseNestedRootReferences)
                    {
                        parent = rootReference.Parent;
                    }
                    else
                    {
                        break;
                    }
                }
                else if (parent.Kind == IqlExpressionKind.Variable)
                {
                    var variableExpression = (IqlVariableExpression)parent;
                    var resolvedType = typeResolver.ResolveTypeFromTypeName(variableExpression.EntityTypeName);
                    if (resolvedType != null)
                    {
                        lastRootReference = variableExpression;
                    }
                    break;
                }
                else
                {
                    list.Add((IqlPropertyExpression)parent);
                    parent = parent.Parent;
                }
            }

            var entityConfig = entityConfigurationContext;

            if (lastRootReference != null)
            {
                for (var i = list.Count - 1; i >= 0; i--)
                {
                    var property = entityConfig.FindProperty(list[i].PropertyName);

                    // We might be trying to get a path from a method on a property, like:
                    // t.Description.ToString()
                    if (property == null)
                    {
                        continue;
                    }
                    propertyPath = new IqlPropertyPath(
                        typeResolver,
                        property,
                        list[i],
                        propertyPath);
                    propertyPath.EntityConfiguration = entityConfig;

                    if (i == 0)
                    {
                        break;
                    }

                    var entityProperty = property.EntityProperty();
                    if (entityProperty != null)
                    {
                        if (entityProperty.Relationship == null)
                        {
                            continue;
                        }
                        else
                        {
                            entityConfig = entityProperty.Relationship.OtherEnd.Property.EntityConfiguration.TypeMetadata;
                        }
                    }
                    else
                    {
                        var type = property.Type;
                        for (var j = 0; j < property.TypeMetadata.GenericTypeParameters.Length; j++)
                        {
                            if (type.Name == property.TypeMetadata.GenericTypeParameters[j].Name)
                            {
                                type = entityConfig.GenericTypeParameters[j].Type.Type;
                                break;
                            }
                        }
                        entityConfig = typeResolver.FindTypeByType(type);
                    }
                }
            }

            if (propertyPath == null && lastRootReference != null)
            {
                return new IqlPropertyPath(typeResolver, null, propertyExpression, null);
            }
            return propertyPath;
        }

        public IqlPropertyPath RebaseFrom(IqlPropertyPath pathBase)
        {
            if (!PathToHere.StartsWith(pathBase.PathToHere))
            {
                throw new ArgumentException();
            }
            var top = pathBase.Top;
            var ourTop = Top;
            while (top.Child != null)
            {
                top = top.Child;
                ourTop = ourTop.Child;
            }
            return FromString(TypeResolver, ourTop.Child.PathFromHere, ourTop.Child.EntityConfiguration);
        }

        public object Evaluate(object entity)
        {
            if (entity == null)
            {
                return null;
            }
            var result = entity;
            foreach (var part in PropertyPath)
            {
                result = result.GetPropertyValueByName(part.PropertyName);
                if (result == null)
                {
                    return null;
                }
            }

            return result;
        }

        public T EvaluateAs<T>(object entity)
        {
            return (T)Evaluate(entity);
        }
    }
}