using System;

namespace Iql.Parsing
{
    public class EvaluateContext
    {
        public object Context;
        public Func<string, object> Evaluate { get; set; }

        public EvaluateContext(Func<string, object> evaluate = null)
        {
            Evaluate = evaluate;
        }
    }
}