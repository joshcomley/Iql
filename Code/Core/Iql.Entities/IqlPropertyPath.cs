using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Iql.Conversion;

namespace Iql.Entities
{
    public class IqlPropertyPath
    {
        public bool IsEmpty => string.IsNullOrWhiteSpace(PathToHere);

        public IqlPropertyPath(IProperty property, IqlPropertyExpression expression, IqlPropertyPath parent)
        {
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
        public string PathToHere => string.Join("/", PropertyPath.Select(p => p.Property.Name));

        public string PathFromHere
        {
            get
            {
                var parts = new List<string>();
                parts.Add(PropertyName);
                var child = Child;
                while (child != null)
                {
                    parts.Add(child.PropertyName);
                    child = child.Child;
                }

                return string.Join("/", parts);
            }
        }

        public IqlPropertyPath Parent { get; }
        public string PropertyName => Property?.Name;
        public IProperty Property { get; }
        public IqlPropertyExpression Expression { get; }

        public IqlPropertyPath[] PropertyPath { get; }

        public IEntityConfiguration EntityConfiguration { get; set; }

        public IEntityConfiguration PropertyEntityConfiguration =>
            Property?.Relationship.OtherEnd.Configuration;

        public object Getter(object entity)
        {
            if (entity == null)
            {
                return null;
            }

            var value = entity;
            for (var i = 0; i < PropertyPath.Length; i++)
            {
                var pathItem = PropertyPath[i];
                value = pathItem.Property.PropertyGetter(value);
            }

            return value;
        }

        public static IqlPropertyPath FromLambda<T>(Expression<Func<T, object>> field,
            EntityConfiguration<T> entityConfigurationContext) where T : class
        {
            var propertyExpression = IqlExpressionConversion.DefaultExpressionConverter().ConvertPropertyLambdaToIql(field).Expression;
            return FromPropertyExpression(entityConfigurationContext, propertyExpression);
        }

        public static IqlPropertyPath FromString(string path,
            IEntityConfiguration entityConfigurationContext,
            IqlPropertyExpression parent = null)
        {
            var propertyExpression = IqlExpression.GetPropertyExpression(path);
            if (parent != null)
            {
                propertyExpression.Parent = parent;
            }
            return FromPropertyExpression(entityConfigurationContext, propertyExpression);
        }

        public static IqlPropertyPath FromPropertyExpression(
            IEntityConfiguration entityConfigurationContext,
            IqlPropertyExpression propertyExpression)
        {
            IqlPropertyPath propertyPath = null;
            var list = new List<IqlPropertyExpression>();
            list.Add(propertyExpression);
            var parent = propertyExpression.Parent;
            while (parent is IqlPropertyExpression || parent is IqlRootReferenceExpression)
            {
                if (parent is IqlRootReferenceExpression)
                {
                    var rootReference = parent as IqlRootReferenceExpression;
                    if (rootReference.Parent != null)
                    {
                        parent = rootReference.Parent;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    list.Add(parent as IqlPropertyExpression);
                    parent = parent.Parent;
                }
            }

            var entityConfig = entityConfigurationContext;
            for (var i = list.Count - 1; i >= 0; i--)
            {
                var property = entityConfig.FindProperty(list[i].PropertyName);
                propertyPath = new IqlPropertyPath(
                    property,
                    list[i],
                    propertyPath);
                propertyPath.EntityConfiguration = entityConfig;

                if (i == 0)
                {
                    break;
                }

                entityConfig = entityConfigurationContext.Builder.GetEntityByType(property.Relationship.OtherEnd.Type);
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
            return FromString(ourTop.Child.PathFromHere, ourTop.Child.EntityConfiguration);
        }
    }
}