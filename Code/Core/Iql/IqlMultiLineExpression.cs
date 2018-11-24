using System;
using System.Collections.Generic;
using System.Linq;

namespace Iql
{
    public class IqlMultiLineExpression : IqlSridExpression
    {
        public List<IqlLineExpression> Lines { get; set; }
        public IqlMultiLineExpression(IEnumerable<IqlLineExpression> points, IqlType type = IqlType.GeographyMultiLine, int? srid = null) : base(srid, type)
        {
            Lines = points?.ToList();
        }

        public IqlMultiLineExpression() : base(null, IqlType.GeographyMultiLine)
        {

        }

        public double Length(IqlDistanceKind unit = IqlDistanceKind.Meters)
        {
            if (Lines == null)
            {
                return 0;
            }

            return Lines.Sum(_ => _.Length());
        }

        public override IqlExpression Clone()
        {
            // #CloneStart

			var expression = new IqlMultiLineExpression(null);
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
			expression.Srid = Srid;
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
