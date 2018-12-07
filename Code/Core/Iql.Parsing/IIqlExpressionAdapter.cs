namespace Iql.Parsing
{
    public interface IIqlExpressionAdapter<out TIqlData, TRegistry, TParserBase>
        where TRegistry : RegistryStore<IqlExpression, TParserBase>
        where TParserBase : class
    {
        TRegistry Registry { get; set; }
        TIqlData NewData();
    }
}