using Iql.Queryable.Expressions.Conversion;

namespace Iql.Queryable.Expressions
{
    /// <summary>
    /// This is merely short-hand for IqlExpressionConversion.DefaultExpressionConverterInstance
    /// </summary>
    public class IqlConverter
    {
        public static IExpressionConverter Instance
        {
            get => IqlExpressionConversion.DefaultExpressionConverterInstance;
        }
    }
}