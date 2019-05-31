using System;
using System.Collections.Generic;
using System.Linq;
using Iql.Extensions;

namespace Iql
{
    public class IqlEnumLiteralExpression : IqlLiteralExpressionBase<IqlEnumValueExpression[]>
    {
        private readonly object _enumType;

        public IqlEnumLiteralExpression(object enumType = null, string nameSpace = null)
            : base(null, IqlType.Enum, IqlExpressionKind.EnumLiteral)
        {
            _enumType = enumType;
            Namespace = nameSpace;
        }

        public IqlEnumLiteralExpression() : base(null, IqlType.Enum, IqlExpressionKind.EnumLiteral)
        {
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


        internal override void FlattenInternal(IqlFlattenContext context)
        {
            // #FlattenStart

				if(Value != null)
				{
					for(var i = 0; i < Value.Length; i++)
					{
						context.Flatten(Value[i]);
					}
				}
				context.Flatten(Parent);

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

		public static IqlEnumLiteralExpression Clone(IqlEnumLiteralExpression source)
		{
			// #CloneStart

			var expression = new IqlEnumLiteralExpression();
			expression.Namespace = source.Namespace;
			if(source.Value == null)
			{
				expression.Value = null;
			}
			else
			{
				var listCopy = new List<IqlEnumValueExpression>();
				for(var i = 0; i < source.Value.Length; i++)
				{
					listCopy.Add((IqlEnumValueExpression)source.Value[i]?.Clone());
				}
				expression.Value = listCopy.ToArray();
			}
			expression.InferredReturnType = source.InferredReturnType;
			expression.Key = source.Key;
			expression.Kind = source.Kind;
			expression.ReturnType = source.ReturnType;
			expression.Parent = source.Parent?.Clone();
			return expression;

			// #CloneEnd
		}
    }
}
