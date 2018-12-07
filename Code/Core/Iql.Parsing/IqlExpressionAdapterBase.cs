namespace Iql.Parsing
{
    public abstract class IqlExpressionAdapterBase<TIqlData, TRegistry, TParserBase>
        : IIqlExpressionAdapter<TIqlData, TRegistry, TParserBase>
        where TRegistry : RegistryStore<IqlExpression, TParserBase>, new()
        where TParserBase : class
    {
        protected IqlExpressionAdapterBase()
        {
            Registry = new TRegistry();
        }


        public abstract TIqlData NewData();

        public TRegistry Registry { get; set; }
    }
}