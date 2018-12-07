namespace Iql.Parsing
{
    public abstract class IqlExpressionAdapter<TIqlData>
        : IqlExpressionAdapterBase<TIqlData, IqlParserRegistry, IActionParserBase>
    {
        protected IqlExpressionAdapter()
        {
        }
    }
}