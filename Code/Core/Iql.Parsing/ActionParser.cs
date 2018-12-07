using System;
using Iql.Conversion;
using Iql.Extensions;

namespace Iql.Parsing
{
    public abstract class ActionParser<TAction, TIqlData, TQueryAdapter, TOutput, TParserOutput, TParserInstance, TConverter>
        : IActionParser<TAction, TIqlData, TQueryAdapter, TOutput, TParserOutput, TParserInstance, TConverter>
        where TAction : IqlExpression
        where TQueryAdapter : IIqlExpressionAdapter<TIqlData, IqlParserRegistry, IActionParserBase>
        where TParserOutput : IParserOutput
        where TParserInstance : ActionParserContext<TIqlData, TQueryAdapter, TOutput, TParserOutput, TConverter>
        where TConverter : IExpressionConverter
    {
        public TConverter Converter { get; set; }

        public virtual IqlExpression ToQueryStringTyped<TEntity>(TAction action, TParserInstance parser) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public virtual IqlExpression ToQueryString(TAction action, TParserInstance parser)
        {
            return (IqlExpression)GetType().GetMethod(nameof(ToQueryStringTyped))
                .InvokeGeneric(this, new object[] {action, parser}, parser.CurrentEntityType);
        }

        IqlExpression IActionParserBase.ToQueryString(IqlExpression action, IActionParserContext parser)
        {
            return ToQueryString((TAction)action, (TParserInstance)parser);
        }
    }
}