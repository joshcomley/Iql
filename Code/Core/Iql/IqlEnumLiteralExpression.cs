using System;
using System.Collections.Generic;
using System.Linq;
using Iql.Extensions;

namespace Iql
{
    public class IqlEnumLiteralExpression : IqlLiteralExpressionBase<IqlEnumValueExpression[]>
    {
        private readonly object _enumType;

        public IqlEnumLiteralExpression(object enumType, string nameSpace = null)
            : base(null, IqlType.Enum, IqlExpressionKind.EnumLiteral)
        {
            _enumType = enumType;
            Namespace = nameSpace;
        }

        public object EnumType()
        {
            return _enumType;
        }

        public IqlEnumLiteralExpression AddValue(long value, string name)
        {
            var values = Value == null ? new List<IqlEnumValueExpression>() : Value.ToList();
            values.Add(new IqlEnumValueExpression(value, name));
            Value = values.ToArray();
            return this;
        }

        public IqlEnumLiteralExpression()
        {
        }

        public static IqlEnumLiteralExpression Combine(params IqlEnumLiteralExpression[] expressions)
        {
            var expression = new IqlEnumLiteralExpression(expressions[0]._enumType, expressions[0].Namespace);
            for (var i = 0; i < expressions.Length; i++)
            {
                var e = expressions[i];
                for (var j = 0; j < e.Value.Length; j++)
                {
                    var value = e.Value[j];
                    expression.AddValue(value.Value, value.Name);
                }
            }

            return expression;
        }

        public string Namespace { get; set; }

		public override IqlExpression Clone()
		{
			// #CloneStart

			var expression = new IqlEnumLiteralExpression(null);
			expression.Namespace = Namespace;
			if(Value == null)
			{
				expression.Value = null;
			}
			else
			{
				var listCopy = new List<IqlEnumValueExpression>();
				for(var i = 0; i < Value.Length; i++)
				{
					listCopy.Add((IqlEnumValueExpression)Value[i]?.Clone());
				}
				expression.Value = listCopy.ToArray();
			}
			expression.InferredReturnType = InferredReturnType;
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
				if(Value != null)
				{
					for(var i = 0; i < Value.Length; i++)
					{
						Value[i]?.FlattenInternal(expressions, checker);
					}
				}
				Parent?.FlattenInternal(expressions, checker);
			}

			// #FlattenEnd
        }

		internal override IqlExpression ReplaceExpressions(ReplaceContext context)
		{
			// #ReplaceStart

			if(Value != null)
			{
				for(var i = 0; i < Value.Length; i++)
				{
					Value[i] = (IqlEnumValueExpression)context.Replace(this, nameof(Value), i, Value[i]);
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
