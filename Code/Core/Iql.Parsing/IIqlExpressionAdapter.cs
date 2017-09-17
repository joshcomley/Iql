namespace Iql.Parsing
{
    public interface IIqlExpressionAdapter<out TIqlData>
    {
        IqlParserRegistry Registry { get; set; }
        TIqlData NewData();
    }
}