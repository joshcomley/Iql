using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using Iql.Extensions;
using Iql.Parsing.Types;
#if TypeScript
using Iql.Parsing;
#endif

namespace Iql.Conversion
{
    public abstract class ExpressionConverterBase : IExpressionConverter
    {
        //        public abstract ExpressionResult<IqlExpression> ConvertQueryExpressionToIql<TEntity>(QueryExpression filter
        //#if TypeScript
        //            , EvaluateContext evaluateContext
        //#endif
        //        ) where TEntity : class;

        protected abstract ExpressionResult<IqlExpression> ConvertLambdaExpressionToIqlInternal<TEntity>(LambdaExpression lambda
            , ITypeResolver typeResolver
#if TypeScript
            , EvaluateContext evaluateContext
#endif
        ) where TEntity : class;

        static ExpressionConverterBase()
        {
            ConvertLambdaToIqlInternalMethod = typeof(ExpressionConverterBase)
                .GetMethod(nameof(ConvertLambdaToIqlInternal), BindingFlags.Instance | BindingFlags.NonPublic);
        }

        public static MethodInfo ConvertLambdaToIqlInternalMethod { get; set; }

        private static readonly string[] LambdaRegexes = new[]
        {
#if TypeScript
            @"^function\s*\(([A-Za-z_][A-Za-z0-9_]*)\)\s*{\s*return\s+\1\.([A-Za-z0-9_\.]+);\s*}$",
            //@"^function\s+\(([A-Za-z_][A-Za-z0-9_]{0,})\)\s+\{\s+return\s+\1\.([^;]+);\s+\}$",
#endif
            @"^([A-Za-z_][A-Za-z0-9_]{0,})\s+\=\>\s+\1\.([A-Za-z_][A-Za-z0-9_]{0,})+$"
        };
        private static bool PropertyLambdaConversionCacheDelayedInitialized;
        private static Dictionary<string, Match> PropertyLambdaConversionCacheDelayed;
        private static Dictionary<string, Match> PropertyLambdaConversionCache { get { if(!PropertyLambdaConversionCacheDelayedInitialized) { PropertyLambdaConversionCacheDelayedInitialized = true; PropertyLambdaConversionCacheDelayed = new Dictionary<string, Match>(); } return PropertyLambdaConversionCacheDelayed; } set { PropertyLambdaConversionCacheDelayedInitialized = true; PropertyLambdaConversionCacheDelayed = value; } }
        public virtual ExpressionResult<IqlExpression> ConvertLambdaExpressionToIql<TEntity>(LambdaExpression expression
            , ITypeResolver typeResolver
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        ) where TEntity : class
        {
            var input = expression.ToString().Trim();
            var key = input + typeof(TEntity).Name;
            Match match = null;
            if (PropertyLambdaConversionCache.ContainsKey(key))
            {
                match = PropertyLambdaConversionCache[key];
            }
            else
            {
                for (var i = 0; i < LambdaRegexes.Length; i++)
                {
                    var regex = new Regex(LambdaRegexes[i]);
                    var m = regex.Match(input);

#if TypeScript
                    if (m != null && m.Groups[2] != null)
#else
                    if (m.Groups[2].Success)
#endif
                    {
                        match = m;
                        break;
                    }
                }
                PropertyLambdaConversionCache.Add(key, match);
            }

            if (match != null)
            {
                IqlPropertyExpression property = null;
                var parts = match.Groups[2].Value.Split('.');
                var iqlRootReferenceExpression = new IqlRootReferenceExpression(match.Groups[1].Value);
                iqlRootReferenceExpression.EntityTypeName = typeof(TEntity).GetFullName();
                for (var j = 0; j < parts.Length; j++)
                {
                    var part = parts[j];
                    property = new IqlPropertyExpression(part, (IqlReferenceExpression)property ?? iqlRootReferenceExpression);
                }

                var l = new IqlLambdaExpression(IqlType.Unknown, property);
                l.Parameters.Add(iqlRootReferenceExpression);
                return new ExpressionResult<IqlExpression>(l);
            }

            var result = ConvertLambdaExpressionToIqlInternal<TEntity>(expression,
                typeResolver
#if TypeScript
, evaluateContext
#endif
            );
            return result;
        }

        public virtual ExpressionResult<IqlExpression> ConvertLambdaExpressionToIqlByType(
            LambdaExpression lambda
            , ITypeResolver typeResolver
            , Type entityType
#if TypeScript
            , EvaluateContext evaluateContext
#endif
            )
        {
            return (ExpressionResult<IqlExpression>)ConvertLambdaToIqlInternalMethod
                .InvokeGeneric(this, new object[]
                {
                    lambda, typeResolver
#if TypeScript
                    , evaluateContext
#endif
                }, entityType);
        }

        private ExpressionResult<IqlExpression> ConvertLambdaToIqlInternal<TEntity>(LambdaExpression lambdaExpression
            , ITypeResolver typeResolver
#if TypeScript
, EvaluateContext evaluateContext
#endif
        ) where TEntity : class
        {
            return ConvertLambdaExpressionToIql<TEntity>(lambdaExpression
                , typeResolver
#if TypeScript
                , evaluateContext
#endif
            );
        }

        public ExpressionResult<IqlExpression> ConvertLambdaToIql<TEntity>(Expression<Func<TEntity, object>> expression
            , ITypeResolver typeResolver
#if TypeScript
, EvaluateContext evaluateContext
#endif
        ) where TEntity : class
        {
            return ConvertLambdaExpressionToIql<TEntity>(expression
                , typeResolver
#if TypeScript
                , evaluateContext
#endif
            );
        }

        public virtual ExpressionResult<IqlPropertyExpression> ConvertPropertyLambdaToIql<TEntity, TProperty>(Expression<Func<TEntity, TProperty>> filter
            , ITypeResolver typeResolver
#if TypeScript
            , EvaluateContext evaluateContext
#endif
        ) where TEntity : class
        {
            var lambdaExpression = filter as LambdaExpression;
            return ConvertPropertyLambdaExpressionToIql<TEntity>(lambdaExpression
                , typeResolver
#if TypeScript
                , evaluateContext
#endif
            );
        }

        public virtual ExpressionResult<IqlPropertyExpression> ConvertPropertyLambdaExpressionToIql<TEntity>(LambdaExpression filter
            , ITypeResolver typeResolver
#if TypeScript
            , EvaluateContext evaluateContext
#endif
        )
            where TEntity : class
        {
            var exp = ConvertLambdaExpressionToIql<TEntity>(filter
                , typeResolver
#if TypeScript
                    , evaluateContext
#endif
            ).Expression;
            var lambda = exp as IqlLambdaExpression;
            return new ExpressionResult<IqlPropertyExpression>(
                lambda.Body as IqlPropertyExpression);
        }

        public abstract LambdaExpression ConvertIqlToExpression<TEntity>(IqlExpression expression
            , ITypeResolver typeResolver
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        ) where TEntity : class;

        public abstract LambdaExpression ConvertIqlToLambdaExpression(IqlExpression expression
            , ITypeResolver typeResolver
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );

        public string ConvertIqlToExpressionString(IqlExpression expression
            , ITypeResolver typeResolver
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            return ConvertIqlToExpressionStringByType(expression, typeResolver, null
#if TypeScript
            , evaluateContext
#endif
            );
        }

        public string ConvertIqlToExpressionStringAs<TEntity>(IqlExpression expression
            , ITypeResolver typeResolver
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            return ConvertIqlToExpressionStringByType(expression, typeResolver, typeof(TEntity)
#if TypeScript
            , evaluateContext
#endif
            );
        }

        public abstract string ConvertIqlToExpressionStringByType(IqlExpression expression
            , ITypeResolver typeResolver
            , Type rootEntityType
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );
    }
}