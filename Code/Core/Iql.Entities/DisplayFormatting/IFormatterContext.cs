namespace Iql.Entities.DisplayFormatting
{
    public interface IFormatterContext
    {
        object Entity { get; set; }
        string Format(IqlExpression expression, object value);
    }
}