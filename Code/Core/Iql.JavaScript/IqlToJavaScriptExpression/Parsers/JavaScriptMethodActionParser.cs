using System.Collections.Generic;
using System.Linq;
using Iql.JavaScript.Extensions;

namespace Iql.JavaScript.IqlToJavaScriptExpression.Parsers
{
    public abstract class
        JavaScriptMethodActionParser<TAction> : JavaScriptActionParserBase<TAction>
        where TAction : IqlExpression
    {
        public override IqlExpression ToQueryString(TAction action,
            JavaScriptIqlParserContext parser)
        {
            return JavaScriptMethod(
                parser,
                ResolveMethodName(action),
                ResolveMethodCaller(action),
                ResolveMethodArguments(action).ToArray());
        }

        public IqlExpression JavaScriptMethod(JavaScriptIqlParserContext parser, string name, IqlExpression caller,
            IqlExpression[] args)
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
            var invocation = new IqlAggregateExpression(arr.ToArray());
            if (caller == null)
            {
                return invocation;
            }

            if (parser.AllowTranspilation())
            {
                return caller.Coalesce(invocation, new[] { "trim", "toUpperCase", "toLowerCase", "includes" }.Contains(name) ? @"''" : null);
            }

            return caller.DotAccess(invocation);
        }

        public virtual IqlExpression ResolveMethodCaller(TAction action)
        {
            return action.Parent;
        }

        public abstract string ResolveMethodName(TAction action);

        public abstract IEnumerable<IqlExpression> ResolveMethodArguments(TAction action);
    }
}