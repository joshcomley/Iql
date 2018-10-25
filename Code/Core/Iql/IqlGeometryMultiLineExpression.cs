using System;
using System.Collections.Generic;

namespace Iql
{
    public class IqlGeometryMultiLineExpression : IqlMultiLineExpression
    {
        public IqlGeometryMultiLineExpression(IEnumerable<IqlLineExpression> points, int? srid = null) : base(
            points, IqlExpressionKind.GeometryMultiLine, IqlType.GeometryMultiLine)
        {
        }

        public IqlGeometryMultiLineExpression() : base(new IqlLineExpression[] { },
            IqlExpressionKind.GeometryMultiLine, IqlType.GeometryMultiLine)
        {
        }

        public override IqlExpression Clone()
        {
            // #CloneStart

			var expression = new IqlGeometryMultiLineExpression(null);
			if(Lines == null)
			{
				expression.Lines = null;
			}
			else
			{
				var listCopy = new List<IqlLineExpression>();
				for(var i = 0; i < Lines.Count; i++)
				{
					listCopy.Add((IqlLineExpression)Lines[i]?.Clone());
				}
				expression.Lines = listCopy;
			}
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

			if(expressions.Contains(this))
			{
				return;
			}
			var reaction = checker == null ? FlattenReactionKind.Continue : checker(this);
			if(reaction == FlattenReactionKind.Ignore)
			{
				return;
			}
			if(reaction != FlattenReactionKind.OnlyChildren)
			{
				expressions.Add(this);
			}
			if(reaction != FlattenReactionKind.IgnoreChildren)
			{
				if(Lines != null)
				{
					for(var i = 0; i < Lines.Count; i++)
					{
						Lines[i]?.FlattenInternal(expressions, checker);
					}
				}
				Parent?.FlattenInternal(expressions, checker);
			}

            // #FlattenEnd
        }

        internal override IqlExpression ReplaceExpressions(ReplaceContext context)
        {
            // #ReplaceStart

			if(Lines != null)
			{
				for(var i = 0; i < Lines.Count; i++)
				{
					Lines[i] = (IqlLineExpression)context.Replace(this, nameof(Lines), i, Lines[i]);
				}
			}
			Parent = context.Replace(this, nameof(Parent), null, Parent);
			var replaced = context.Replacer(context, this);
			if(replaced != this)
			{
				return replaced;	
			}
			return this;

            // #ReplaceEnd
        }
    }
}
