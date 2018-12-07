using System;
using System.Threading.Tasks;
using Iql.Conversion;
using Iql.Extensions;

namespace Iql.Parsing
{
    public abstract class AsyncActionParser<TAction, TIqlData, TQueryAdapter, TOutput, TParserOutput, TParserContext, TConverter>
        : IAsyncActionParser<TAction, TIqlData, TQueryAdapter, TOutput, TParserOutput, TParserContext, TConverter>
        //: ActionParserContextBase<IqlAsyncParserRegistry, TAction, TIqlData, TQueryAdapter, TOutput, TParserOutput, TParserInstance, TConverter, IAsyncActionParserBase>,
        //  :  IAsyncActionParserBase
        where TAction : IqlExpression
        where TQueryAdapter : IIqlExpressionAdapter<TIqlData, IqlAsyncParserRegistry, IAsyncActionParserBase>
        where TParserOutput : IParserOutput
        where TParserContext : AsyncActionParserContext<TIqlData, TQueryAdapter, TOutput, TParserOutput, TConverter>
        where TConverter : IExpressionConverter
    {
        public virtual Task<IqlExpression> ToQueryStringTypedAsync<TEntity>(TAction action, TParserContext parser) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public TConverter Converter { get; set; }

        public virtual Task<IqlExpression> ToQueryStringAsync(TAction action, TParserContext parser)
        {
            return (Task<IqlExpression>)GetType().GetMethod(nameof(ToQueryStringTypedAsync))
                .InvokeGeneric(this, new object[] { action, parser }, parser.CurrentEntityType);
        }

        Task<IqlExpression> IAsyncActionParserBase.ToQueryStringAsync(IqlExpression action, IAsyncActionParserContext parser)
        {
            return ToQueryStringAsync((TAction)action, (TParserContext)parser);
        }

        public IqlExpression ToQueryString(IqlExpression action, IActionParserContext parser)
        {
            throw new NotImplementedException();
        }
    }
}