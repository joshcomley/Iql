using System;
using System.Collections.Generic;

namespace Iql
{
    public class IqlIntersectsExpression : IqlExpression
    {
        public IqlPointExpression Point { get; set; }
        public IqlPolygonExpression Polygon { get; set; }

        public IqlIntersectsExpression(IqlPointExpression point, IqlPolygonExpression polygon, IqlExpression parent = null) : base(IqlExpressionKind.Intersects, IqlType.Decimal, parent)
        {
            Point = point;
            Polygon = polygon;
        }

        public IqlIntersectsExpression() : base(IqlExpressionKind.Intersects)
        {

        }

        public override IqlExpression Clone()
        {
            // #CloneStart

            var expression = new IqlIntersectsExpression();
            expression.Key = Key;
            expression.Kind = Kind;
            expression.ReturnType = ReturnType;
            expression.Parent = Parent?.Clone();
            return expression;

            // #CloneEnd
        }

        internal override void FlattenInternal(IList<IqlExpression> expressions,
            Func<IqlExpression, FlattenReactionKind> checker = null)
        {
            // #FlattenStart

            if (expressions.Contains(this))
            {
                return;
            }
            var reaction = checker == null ? FlattenReactionKind.Continue : checker(this);
            if (reaction == FlattenReactionKind.Ignore)
            {
                return;
            }
            if (reaction != FlattenReactionKind.OnlyChildren)
            {
                expressions.Add(this);
            }
            if (reaction != FlattenReactionKind.IgnoreChildren)
            {
                Parent?.FlattenInternal(expressions, checker);
            }

            // #FlattenEnd
        }

        internal override IqlExpression ReplaceExpressions(ReplaceContext context)
        {
            // #ReplaceStart

            Parent = context.Replace(this, nameof(Parent), null, Parent);
            var replaced = context.Replacer(context, this);
            if (replaced != this)
            {
                return replaced;
            }
            return this;

            // #ReplaceEnd
        }
    }
}
