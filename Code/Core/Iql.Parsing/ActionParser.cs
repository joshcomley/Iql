using System;

namespace Iql.Parsing
{
    public abstract class ActionParser<TAction, TIqlData, TQueryAdapter, TOutput, TParserOutput, TParserInstance>
        : IActionParser<TAction, TIqlData, TQueryAdapter, TOutput, TParserOutput, TParserInstance>
        where TAction : IqlExpression
        where TQueryAdapter : IIqlExpressionAdapter<TIqlData>
        where TParserOutput : IParserOutput
        where TParserInstance : ActionParserInstance<TIqlData, TQueryAdapter, TOutput, TParserOutput>
    {
        public virtual IqlExpression ToQueryString(TAction action, TParserInstance parser)
        {
            throw new NotImplementedException();
        }

        IqlExpression IActionParserBase.ToQueryString(IqlExpression action, IActionParserInstance parser)
        {
            return ToQueryString((TAction)action, (TParserInstance)parser);
        }
    }
}