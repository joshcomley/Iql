namespace Iql.Parsing
{
    public abstract class IqlExpressionAdapter<TIqlData>
        : IIqlExpressionAdapter<TIqlData>
    {
        protected IqlExpressionAdapter()
        {
            Registry = new IqlParserRegistry();
        }


        public abstract TIqlData NewData();

        public IqlParserRegistry Registry { get; set; }
    }
}