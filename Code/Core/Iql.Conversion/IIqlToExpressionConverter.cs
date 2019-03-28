#if TypeScript
using Iql.Parsing;    
#endif
using System;
using System.Linq.Expressions;
using Iql.Parsing.Types;

namespace Iql.Conversion
{
    public interface IIqlToExpressionConverter
    {
        LambdaExpression ConvertIqlToExpression<TEntity>(IqlExpression expression, ITypeResolver typeResolver
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
            where TEntity : class;

        LambdaExpression ConvertIqlToLambdaExpression(IqlExpression expression, ITypeResolver typeResolver
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );

        string ConvertIqlToExpressionStringByType(IqlExpression expression, ITypeResolver typeResolver,
            Type rootEntityType
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );

        string ConvertIqlToExpressionString(IqlExpression expression, ITypeResolver typeResolver
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );

        string ConvertIqlToExpressionStringAs<TEntity>(IqlExpression expression, ITypeResolver typeResolver
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        );
    }
}