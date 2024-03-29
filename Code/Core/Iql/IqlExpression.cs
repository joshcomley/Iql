﻿using System;
using System.Collections.Generic;
using System.Linq;
using Iql.Entities.InferredValues;
using Iql.Extensions;

namespace Iql
{
    public abstract class IqlExpression
    {
        public bool IsIqlExpression => true;

        protected IqlExpression(IqlExpressionKind kind, IqlType? returnType = IqlType.Unknown, IqlExpression parent = null)
        {
            Kind = kind;
            if (returnType == null || returnType == IqlType.Unknown)
            {
                returnType = kind.ResolveDefaultReturnType();
            }
            ReturnType = returnType ?? IqlType.Unknown;
            Parent = parent;
        }

        public string Key { get; set; }
        public IqlExpressionKind Kind { get; set; }
        public virtual IqlType ReturnType { get; set; }
        public IqlExpression Parent { get; set; }

        public bool IsOrHasRootEntity()
        {
            return IsOrHas(e => e is IqlRootReferenceExpression);
        }

        public virtual bool IsOrHas(Func<IqlExpression, bool> matches)
        {
            if (matches(this))
            {
                return true;
            }
            return Parent != null && Parent.IsOrHas(matches);
        }

        public virtual IqlRootReferenceExpression GetRootEntity()
        {
            if (this is IqlRootReferenceExpression)
            {
                return this as IqlRootReferenceExpression;
            }
            return Parent?.GetRootEntity();
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

        public T As<T>()
            where T : IqlExpression
        {
            return this as T;
        }

        public static IqlPropertyExpression GetPropertyExpression(string propertyName, string rootReferenceName = null)
        {
            var rootReferenceExpression = new IqlRootReferenceExpression(rootReferenceName ?? "entity", "");
            var parts = propertyName.Contains("/") ? propertyName.Split('/') : propertyName.Split('.');
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

        public virtual IqlType ResolveIqlType(Type rootEntityType)
        {
            return ResolveType(rootEntityType).ToIqlType();
        }
        
        public virtual Type ResolveType(Type rootEntityType)
        {
            var ancestors = new List<IqlExpression>();
            ancestors.Add(this);
            var parent = Parent;
            while (parent != null)
            {
                ancestors.Add(parent);
                if (parent.Parent is IqlRootReferenceExpression rootReferenceExpression)
                {
                    if (rootReferenceExpression.EntityTypeName ==
                        $"{nameof(InferredValueContext<object>)}<{rootEntityType.Name}>")
                    {
                        break;
                    }
                }
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
                        type = type.GetProperty((ancestor as IqlPropertyExpression).PropertyName)?.PropertyType;
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
                case IqlExpressionKind.DataSetReference:
                    return typeof(IqlDataSetReferenceExpression);
                case IqlExpressionKind.WithKey:
                    return typeof(IqlWithKeyExpression);
                case IqlExpressionKind.OrderBy:
                    return typeof(IqlOrderByExpression);
                case IqlExpressionKind.EnumLiteral:
                    return typeof(IqlEnumLiteralExpression);
                case IqlExpressionKind.EnumValue:
                    return typeof(IqlEnumValueExpression);
                case IqlExpressionKind.Lambda:
                    return typeof(IqlLambdaExpression);
                case IqlExpressionKind.Condition:
                    return typeof(IqlConditionExpression);
                case IqlExpressionKind.Invocation:
                    return typeof(IqlInvocationExpression);
                case IqlExpressionKind.GeoPoint:
                    return typeof(IqlPointExpression);
                case IqlExpressionKind.GeoMultiPoint:
                    return typeof(IqlMultiPointExpression);
                case IqlExpressionKind.GeoLine:
                    return typeof(IqlLineExpression);
                case IqlExpressionKind.GeoMultiLine:
                    return typeof(IqlMultiLineExpression);
                case IqlExpressionKind.GeoPolygon:
                    return typeof(IqlPolygonExpression);
                case IqlExpressionKind.GeoMultiPolygon:
                    return typeof(IqlMultiPolygonExpression);
                case IqlExpressionKind.GeoRing:
                    return typeof(IqlRingExpression);
                case IqlExpressionKind.Intersects:
                    return typeof(IqlIntersectsExpression);
                case IqlExpressionKind.Length:
                    return typeof(IqlLengthExpression);
                case IqlExpressionKind.Distance:
                    return typeof(IqlDistanceExpression);
                case IqlExpressionKind.CurrentUser:
                    return typeof(IqlCurrentUserExpression);
                case IqlExpressionKind.CurrentUserId:
                    return typeof(IqlCurrentUserIdExpression);
                case IqlExpressionKind.CurrentLocation:
                    return typeof(IqlCurrentLocationExpression);
                case IqlExpressionKind.NewGuid:
                    return typeof(IqlNewGuidExpression);
                case IqlExpressionKind.NowTicks:
                    return typeof(IqlNowTicksExpression);
                case IqlExpressionKind.NowTicksString:
                    return typeof(IqlNowTicksStringExpression);
            }
            throw new NotSupportedException($"Unable to resolve type for expression kind {kind.ToString()}");
        }

        public IqlExpression ReplaceWith(Func<ReplaceContext, IqlExpression, IqlExpression> replacer)
        {
            return Replace(new ReplaceContext(replacer));
        }
        public IqlExpression Replace(ReplaceContext context)
        {
            return ReplaceExpressions(context);
        }
        public IqlExpression ReplaceExpression(IqlExpression toReplace, IqlExpression toReplaceWith)
        {
            return ReplaceWith((context, expression) =>
            {
                if (expression == toReplace)
                {
                    return toReplaceWith;
                }

                return expression;
            });
        }
        internal abstract IqlExpression ReplaceExpressions(ReplaceContext context);

        public IqlFlattenedExpression[] TopLevelPropertyExpressions()
        {
            var expressions = Flatten()
                .Where(_ =>
                    {
                        return _.Expression.Kind == IqlExpressionKind.Property;
                    })
                .ToArray();
            return expressions.Where(_ =>
                {
                    for (var i = 0; i < expressions.Length; i++)
                    {
                        var expression = expressions[i].Expression;
                        if (expression == _.Expression)
                        {
                            continue;
                        }

                        var parent = expression.Parent;
                        while (parent != null)
                        {
                            if (parent == _.Expression)
                            {
                                return false;
                            }

                            parent = parent.Parent;
                        }
                    }

                    return true;
                })
                .Where(_ =>
                {
                    var expression = _.Expression;
                    while (expression != null)
                    {
                        if (expression.Kind == IqlExpressionKind.RootReference && expression.Parent != null)
                        {
                            return false;
                        }

                        expression = expression.Parent;
                    }

                    return true;

                })
                .ToArray();
        }

        public virtual IqlFlattenedExpression[] Flatten(Func<IqlExpression, FlattenReactionKind> checker = null)
        {
            var context = new IqlFlattenContext(checker);
            context.Flatten(this);
            return context.FlattenedExpressions.ToArray();
        }

        protected FlattenReactionKind CheckFlatten(Func<IqlExpression, FlattenReactionKind> checker)
        {
            if (checker == null)
            {
                return FlattenReactionKind.Continue;
            }
            return checker(this);
        }

        internal abstract void FlattenInternal(IqlFlattenContext context);
    }
}