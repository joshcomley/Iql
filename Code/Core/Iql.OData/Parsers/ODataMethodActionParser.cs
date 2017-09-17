using System;
using System.Collections.Generic;
using Iql.Parsing;

namespace Iql.OData.Parsers
{
    public abstract class
        ODataMethodActionParser<TAction> : ActionParser<TAction, ODataIqlData, ODataIqlExpressionAdapter>
        where TAction : IqlExpression
    {
        public override IqlExpression ToQueryString(TAction action,
            ActionParserInstance<ODataIqlData, ODataIqlExpressionAdapter> parser)
        {
            return ODataMethod(ResolveMethodName(action), ResolveMethodArguments(action));
        }

        public IqlExpression ODataMethod(string name, IqlExpression[] args)
        {
            var arr = new List<IqlExpression>();
            arr.Add(new IqlFinalExpression(name));
            arr.Add(new IqlFinalExpression("("));
            for (var i = 0; i < args.Length; i++)
            {
                arr.Add(args[i]);
                if (i < args.Length - 1)
                {
                    arr.Add(new IqlFinalExpression(","));
                }
            }
            arr.Add(new IqlFinalExpression(")"));
            return new IqlAggregateExpression(arr.ToArray());
        }

        public virtual string ResolveMethodName(TAction action)
        {
            throw new NotImplementedException();
        }

        public virtual IqlExpression[] ResolveMethodArguments(TAction action)
        {
            throw new NotImplementedException();
        }
    }
}