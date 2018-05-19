namespace Iql.Conversion
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