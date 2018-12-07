namespace Iql.Parsing
{
    public abstract class AsyncIqlExpressionAdapter<TIqlData>
        : IqlExpressionAdapterBase<TIqlData, IqlAsyncParserRegistry, IAsyncActionParserBase>
    {
        protected AsyncIqlExpressionAdapter()
        {
        }
    }
}