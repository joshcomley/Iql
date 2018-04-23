using System;
using System.Collections.Generic;

namespace Iql.OData.IqlToODataExpression.Parsers
{
    public abstract class
        ODataMethodActionParser<TAction> : ODataActionParserBase<TAction>
        where TAction : IqlExpression
    {
        public override IqlExpression ToQueryString(TAction action,
            ODataIqlParserInstance parser)
        {
            var methodArguments = ResolveMethodArguments(action);
            var methodName = ResolveMethodName(action);
            if (methodName == "indexof")
            {
                methodArguments[0] = new IqlStringToLowerCaseExpression(methodArguments[0] as IqlReferenceExpression);
                methodArguments[1] = new IqlStringToLowerCaseExpression(methodArguments[1] as IqlReferenceExpression);
            }
            return ODataMethod(methodName, methodArguments);
        }

        public IqlExpression ODataMethod(string name, IqlExpression[] args)
        {
            var arr = new List<IqlExpression>();
            arr.Add(new IqlFinalExpression<string>(name));
            arr.Add(new IqlFinalExpression<string>("("));
            for (var i = 0; i < args.Length; i++)
            {
                arr.Add(args[i]);
                if (i < args.Length - 1)
                {
                    arr.Add(new IqlFinalExpression<string>(","));
                }
            }
            arr.Add(new IqlFinalExpression<string>(")"));
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