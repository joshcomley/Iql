﻿using System;
using System.Collections.Generic;

namespace Iql
{
    public abstract class IqlExpression
    {
        protected IqlExpression(IqlExpressionKind kind, IqlType? returnType = IqlType.Unknown, IqlExpression parent = null)
        {
            Kind = kind;
            ReturnType = returnType ?? IqlType.Unknown;
            Parent = parent;
        }

        public IqlExpressionKind Kind { get; set; }
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

        public static Type ResolveExpressionType(IqlExpression expression)
        {
            if (expression is IqlExpression)
            {
                return expression.GetType();
            }

            return ResolveExpressionTypeFromKind(expression.Kind) ?? expression.GetType();
        }

        public static Type ResolveExpressionTypeFromKind(IqlExpressionKind kind)
        {
            switch (kind)
            {
                case IqlExpressionKind.Aggregate:
                    return typeof(IqlAggregateExpression);
                case IqlExpressionKind.Parenthesis:
                    return typeof(IqlParenthesisExpression);
                case IqlExpressionKind.And:
                    return typeof(IqlAndExpression);
                case IqlExpressionKind.Or:
                    return typeof(IqlOrExpression);
                case IqlExpressionKind.IsGreaterThan:
                    return typeof(IqlIsGreaterThanExpression);
                case IqlExpressionKind.IsGreaterThanOrEqualTo:
                    return typeof(IqlIsGreaterThanOrEqualToExpression);
                case IqlExpressionKind.IsLessThan:
                    return typeof(IqlIsLessThanExpression);
                case IqlExpressionKind.IsLessThanOrEqualTo:
                    return typeof(IqlIsLessThanOrEqualToExpression);
                case IqlExpressionKind.Assign:
                    return typeof(IqlAssignExpression);
                case IqlExpressionKind.IsEqualTo:
                    return typeof(IqlIsEqualToExpression);
                case IqlExpressionKind.IsNotEqualTo:
                    return typeof(IqlIsNotEqualToExpression);
                case IqlExpressionKind.Not:
                    return typeof(IqlNotExpression);
                case IqlExpressionKind.Modulo:
                    return typeof(IqlModuloExpression);
                case IqlExpressionKind.ModuloEquals:
                    return typeof(IqlModuloEqualsExpression);
                case IqlExpressionKind.Add:
                    return typeof(IqlAddExpression);
                case IqlExpressionKind.Subtract:
                    return typeof(IqlSubtractExpression);
                case IqlExpressionKind.Multiply:
                    return typeof(IqlMultiplyExpression);
                case IqlExpressionKind.Divide:
                    return typeof(IqlDivideExpression);
                case IqlExpressionKind.AddEquals:
                    return typeof(IqlAddEqualsExpression);
                case IqlExpressionKind.SubtractEquals:
                    return typeof(IqlSubtractEqualsExpression);
                case IqlExpressionKind.MultiplyEquals:
                    return typeof(IqlMultiplyEqualsExpression);
                case IqlExpressionKind.DivideEquals:
                    return typeof(IqlDivideEqualsExpression);
                case IqlExpressionKind.BitwiseOr:
                    return typeof(IqlBitwiseOrExpression);
                case IqlExpressionKind.Has:
                    return typeof(IqlHasExpression);
                case IqlExpressionKind.BitwiseNot:
                    return typeof(IqlBitwiseNotExpression);
                case IqlExpressionKind.Literal:
                    return typeof(IqlLiteralExpression);
                case IqlExpressionKind.UnarySubtract:
                    return typeof(IqlUnarySubtractExpression);
                case IqlExpressionKind.RootReference:
                    return typeof(IqlRootReferenceExpression);
                case IqlExpressionKind.Variable:
                    return typeof(IqlVariableExpression);
                case IqlExpressionKind.Property:
                    return typeof(IqlPropertyExpression);
                case IqlExpressionKind.StringIncludes:
                    return typeof(IqlStringIncludesExpression);
                case IqlExpressionKind.StringIndexOf:
                    return typeof(IqlStringIndexOfExpression);
                case IqlExpressionKind.StringSubString:
                    return typeof(IqlStringSubStringExpression);
                case IqlExpressionKind.StringToUpperCase:
                    return typeof(IqlStringToUpperCaseExpression);
                case IqlExpressionKind.StringToLowerCase:
                    return typeof(IqlStringToLowerCaseExpression);
                case IqlExpressionKind.StringTrim:
                    return typeof(IqlStringTrimExpression);
                case IqlExpressionKind.StringEndsWith:
                    return typeof(IqlStringEndsWithExpression);
                case IqlExpressionKind.StringStartsWith:
                    return typeof(IqlStringStartsWithExpression);
                case IqlExpressionKind.StringConcat:
                    return typeof(IqlStringConcatExpression);
                case IqlExpressionKind.StringLength:
                    return typeof(IqlStringLengthExpression);
                case IqlExpressionKind.ToString:
                    return typeof(IqlToStringExpression);
                case IqlExpressionKind.Final:
                    return typeof(IqlFinalExpression<>);
                case IqlExpressionKind.Now:
                    return typeof(IqlNowExpression);
                case IqlExpressionKind.Any:
                    return typeof(IqlAnyExpression);
                case IqlExpressionKind.All:
                    return typeof(IqlAllExpression);
                case IqlExpressionKind.Count:
                    return typeof(IqlCountExpression);
                case IqlExpressionKind.TimeSpan:
                    return typeof(IqlTimeSpanExpression);
                case IqlExpressionKind.Expand:
                    return typeof(IqlExpandExpression);
                case IqlExpressionKind.DataSetQuery:
                    return typeof(IqlDataSetQueryExpression);
                case IqlExpressionKind.WithKey:
                    return typeof(IqlWithKeyExpression);
                case IqlExpressionKind.OrderBy:
                    return typeof(IqlOrderByExpression);
            }
            return null;
        }
    }
}