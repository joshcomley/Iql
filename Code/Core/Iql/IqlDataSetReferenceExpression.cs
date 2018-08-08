using System;
using System.Collections.Generic;

namespace Iql
{
    public class IqlDataSetReferenceExpression : IqlExpression
    {
        public string Name { get; set; }
        public IqlDataSetReferenceExpression(IqlExpression parent = null)
        : base(IqlExpressionKind.DataSetReference, IqlType.Collection, parent)
        {

        }

        public IqlDataSetReferenceExpression() : this(null)
        {

        }

		public override IqlExpression Clone()
		{
			// #CloneStart

			var expression = new IqlDataSetReferenceExpression();
			expression.Name = Name;
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
				Parent?.FlattenInternal(expressions, checker);
			}

			// #FlattenEnd
        }
    }
}
