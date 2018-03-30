using System;
using System.Collections.Generic;

namespace Iql
{
    public abstract class IqlExpression
    {
        protected IqlExpression(IqlExpressionType type, IqlType? returnType = IqlType.Void, IqlExpression parent = null)
        {
            Type = type;
            ReturnType = returnType ?? IqlType.Boolean;
            Parent = parent;
        }

        public IqlExpressionType Type { get; set; }
        public IqlType ReturnType { get; set; }
        public IqlExpression Parent { get; set; }

        public static bool IsIqlExpression(object obj)
        {
            return obj is IqlExpression;
        }

        public virtual bool ContainsRootEntity()
        {
            return Parent != null && Parent.ContainsRootEntity();
        }

        public static IqlPropertyExpression GetPropertyExpression(string propertyName)
        {
            var rootReferenceExpression = new IqlRootReferenceExpression("entity", "");
            var propertyExpression = new IqlPropertyExpression(propertyName);
            propertyExpression.Parent = rootReferenceExpression;
            return propertyExpression;
        }

        public virtual Type ResolveType(Type rootEntityType)
        {
            var ancestors = new List<IqlExpression>();
            ancestors.Add(this);
            var parent = Parent;
            while (parent != null)
            {
                ancestors.Add(parent);
                parent = parent.Parent;
            }

            if (ancestors.Count == 1 && this is IqlRootReferenceExpression)
            {
                return rootEntityType;
            }

            Type type = null;
            for (var i = ancestors.Count - 1; i >= 0; i--)
            {
                var ancestor = ancestors[i];
                if (type == null)
                {
                    if (ancestor is IqlRootReferenceExpression)
                    {
                        type = rootEntityType;
                    }
                    else if(ancestor is IqlLiteralExpression)
                    {
                        type = (ancestor as IqlLiteralExpression).Value?.GetType();
                    }

                    if (type == null)
                    {
                        return null;
                    }
                }
                else
                {
                    if (ancestor is IqlPropertyExpression)
                    {
                        type = type.GetProperty((ancestor as IqlPropertyExpression).PropertyName).PropertyType;
                    }
                    else
                    {
                        return null;
                    }
                }
            }

            return type;
        }
    }
}