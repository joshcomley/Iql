using System;
using System.Collections.Generic;
using System.Linq;

namespace Iql
{
    public class IqlWithKeyExpression : IqlExpression
    {
        public List<IqlIsEqualToExpression> KeyEqualToExpressions { get; set; }

        public IqlWithKeyExpression(IEnumerable<IqlIsEqualToExpression> keyEqualToExpressions = null, 
            IqlExpression parent = null)
            : base(IqlExpressionKind.WithKey, IqlType.Class, parent)
        {
            KeyEqualToExpressions = keyEqualToExpressions?.ToList();
        }

        public IqlWithKeyExpression() : this(null)
        {

        }

		public override IqlExpression Clone()
		{
			// #CloneStart

			var expression = new IqlWithKeyExpression();
			if(KeyEqualToExpressions == null)
			{
				expression.KeyEqualToExpressions = null;
			}
			else
			{
				var listCopy = new List<IqlIsEqualToExpression>();
				for(var i = 0; i < KeyEqualToExpressions.Count; i++)
				{
					listCopy.Add((IqlIsEqualToExpression)KeyEqualToExpressions[i]?.Clone());
				}
				expression.KeyEqualToExpressions = listCopy;
			}
			expression.Key = Key;
			expression.Kind = Kind;
			expression.ReturnType = ReturnType;
			expression.Parent = Parent?.Clone();
			return expression;

			// #CloneEnd
		}

		internal override void FlattenInternal(IList<IqlExpression> expressions, Func<IqlExpression, FlattenReactionKind> checker = null)
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
				if(KeyEqualToExpressions != null)
				{
					for(var i = 0; i < KeyEqualToExpressions.Count; i++)
					{
						KeyEqualToExpressions[i]?.FlattenInternal(expressions, checker);
					}
				}
				Parent?.FlattenInternal(expressions, checker);
			}

			// #FlattenEnd
        }

		internal override IqlExpression ReplaceExpressions(ReplaceContext context)
		{
			// #ReplaceStart

			if(KeyEqualToExpressions != null)
			{
				for(var i = 0; i < KeyEqualToExpressions.Count; i++)
				{
					KeyEqualToExpressions[i] = (IqlIsEqualToExpression)context.Replace(this, nameof(KeyEqualToExpressions), i, KeyEqualToExpressions[i]);
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
