using System.Collections.Generic;
using System.Linq;
using Iql.Parsing;

namespace Iql.JavaScript.IqlToJavaScript.Parsers
{
    public abstract class
        JavaScriptMethodActionParser<TAction> : ActionParser<TAction, JavaScriptIqlData, JavaScriptIqlExpressionAdapter>
        where TAction : IqlExpression
    {
        public override IqlExpression ToQueryString(TAction action,
            ActionParserInstance<JavaScriptIqlData, JavaScriptIqlExpressionAdapter> parser)
        {
            return JavaScriptMethod(
                ResolveMethodName(action),
                ResolveMethodCaller(action),
                ResolveMethodArguments(action).ToArray());
        }

        public IqlExpression JavaScriptMethod(string name, IqlExpression caller, IqlExpression[] args)
        {
            var arr = new List<IqlExpression>();
            if (caller != null)
            {
                arr.Add(caller);
                arr.Add(new IqlFinalExpression("."));
            }
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

        public virtual IqlExpression ResolveMethodCaller(TAction action)
        {
            return action.Parent;
        }

        public abstract string ResolveMethodName(TAction action);

        public abstract IEnumerable<IqlExpression> ResolveMethodArguments(TAction action);
    }
}