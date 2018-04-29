using System;
using Iql.Extensions;
using Iql.Queryable.Expressions.Conversion;

namespace Iql.Parsing
{
    public abstract class ActionParser<TAction, TIqlData, TQueryAdapter, TOutput, TParserOutput, TParserInstance, TConverter>
        : IActionParser<TAction, TIqlData, TQueryAdapter, TOutput, TParserOutput, TParserInstance, TConverter>
        where TAction : IqlExpression
        where TQueryAdapter : IIqlExpressionAdapter<TIqlData>
        where TParserOutput : IParserOutput
        where TParserInstance : ActionParserInstance<TIqlData, TQueryAdapter, TOutput, TParserOutput, TConverter>
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
                .InvokeGeneric(this, new object[] {action, parser}, parser.RootEntityType);
        }

        IqlExpression IActionParserBase.ToQueryString(IqlExpression action, IActionParserInstance parser)
        {
            return ToQueryString((TAction)action, (TParserInstance)parser);
        }
    }
}