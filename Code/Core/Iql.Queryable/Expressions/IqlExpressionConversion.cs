using System;
using Iql.Queryable.Expressions.Conversion;

namespace Iql.Queryable.Expressions
{
    public class IqlExpressionConversion
    {
        private static IExpressionConverter _defaultExpressionConverterInstance;
        private static Func<IExpressionConverter> _defaultExpressionConverter;

        public static Func<IExpressionConverter> DefaultExpressionConverter
        {
            get => _defaultExpressionConverter;
            set
            {
                _defaultExpressionConverter = value;
                _defaultExpressionConverterInstance = null;
            }
        }

        public static IExpressionConverter DefaultExpressionConverterInstance
        {
            get => _defaultExpressionConverterInstance = _defaultExpressionConverterInstance ?? DefaultExpressionConverter();
        }
    }
}