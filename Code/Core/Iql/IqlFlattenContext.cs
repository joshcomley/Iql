using System;
using System.Collections.Generic;

namespace Iql
{
    public class IqlFlattenContext
    {
        private List<IqlExpression> _expressions = null;
        public List<IqlExpression> Expressions => _expressions = _expressions ?? new List<IqlExpression>();
        private List<IqlFlattenedExpression> _flattenedExpressions = null;
        public List<IqlFlattenedExpression> FlattenedExpressions => _flattenedExpressions = _flattenedExpressions ?? new List<IqlFlattenedExpression>();
        private List<IqlFlattenedExpression> _ancestors = null;
        public List<IqlFlattenedExpression> Ancestors => _ancestors = _ancestors ?? new List<IqlFlattenedExpression>();
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