using System;
using System.Collections.Generic;

namespace Iql
{
    public class IqlFlattenContext
    {
        public List<IqlExpression> Expressions { get; } = new List<IqlExpression>();
        public List<IqlFlattenedExpression> FlattenedExpressions { get; } = new List<IqlFlattenedExpression>();
        public List<IqlFlattenedExpression> Ancestors { get; } = new List<IqlFlattenedExpression>();
        public Func<IqlExpression, FlattenReactionKind> Checker { get; set; }

        public IqlFlattenContext(Func<IqlExpression, FlattenReactionKind> checker = null)
        {
            Checker = checker;
        }

        public void Flatten(IqlExpression expression)
        {
            if (expression != null && !Expressions.Contains(expression))
            {
                var reactionKind = Checker?.Invoke(expression) ?? FlattenReactionKind.Continue;
                if (reactionKind == FlattenReactionKind.Ignore)
                {
                    return;
                }

                var flattened = new IqlFlattenedExpression(
                    expression, Ancestors.ToArray());
                FlattenedExpressions.Add(flattened);
                Expressions.Add(expression);
                if (reactionKind == FlattenReactionKind.IgnoreChildren)
                {
                    return;
                }
                Ancestors.Add(flattened);
                expression.FlattenInternal(this);
                Ancestors.RemoveAt(Ancestors.Count - 1);
            }
        }
    }
}