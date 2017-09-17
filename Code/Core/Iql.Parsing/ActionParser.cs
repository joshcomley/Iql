using System;

namespace Iql.Parsing
{
    public abstract class ActionParser<TAction, TIqlData, TQueryAdapter>
        : IActionParser<TAction, TIqlData, TQueryAdapter>
        where TAction : IqlExpression
        where TQueryAdapter : IIqlExpressionAdapter<TIqlData>
    {
        public virtual IqlExpression ToQueryString(TAction action, ActionParserInstance<TIqlData, TQueryAdapter> parser)
        {
            throw new NotImplementedException();
        }

        IqlExpression IActionParserBase.ToQueryString(IqlExpression action, IActionParserInstance parser)
        {
            return ToQueryString((TAction) action, (ActionParserInstance<TIqlData, TQueryAdapter>) parser);
        }
    }
}