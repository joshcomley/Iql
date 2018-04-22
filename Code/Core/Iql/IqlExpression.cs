using System;
using System.Collections.Generic;

namespace Iql
{
    public abstract class IqlExpression
    {
        protected IqlExpression(IqlExpressionType type, IqlType? returnType = IqlType.Unknown, IqlExpression parent = null)
        {
            Type = type;
            ReturnType = returnType ?? IqlType.Unknown;
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

        public virtual IqlRootReferenceExpression GetRootEntity()
        {
            if (this is IqlRootReferenceExpression)
            {
                return this as IqlRootReferenceExpression;
            }
            return Parent?.GetRootEntity();
        }

        public static IEnumerable<T> FindAll<T>()
            where T : IqlExpression
        {
            throw new NotImplementedException();
        }

        public void AddRootReference(string parameter = null)
        {
            AddAncestor(new IqlRootReferenceExpression(parameter));
        }

        public void AddAncestor(IqlExpression expression)
        {
            var ancestor = this;
            while (ancestor.Parent != null)
            {
                ancestor = ancestor.Parent;
            }
            ancestor.Parent = expression;
        }

        public static IqlPropertyExpression GetPropertyExpression(string propertyName)
        {
            var rootReferenceExpression = new IqlRootReferenceExpression("entity", "");
            var parts = propertyName.Split('/');
            IqlReferenceExpression parent = rootReferenceExpression;
            IqlPropertyExpression propertyExpression = null;
            for (var i = 0; i < parts.Length; i++)
            {
                var part = parts[i];
                propertyExpression = new IqlPropertyExpression(part, parent);
                parent = propertyExpression;
            }
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
                    else if (ancestor is IqlLiteralExpression)
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