using System.Collections.Generic;
using System;
using Iql.Extensions;

namespace Iql
{
    public class IqlVariableExpression : IqlLiteralExpression
    {
        public IqlVariableExpression(
            string variableName = null,
            object value = null,
            Type entityType = null) : base(null, entityType.ToIqlType())
        {
            VariableName = variableName;
            Value = value;
            EntityTypeName = entityType?.GetFullName();
            Kind = IqlExpressionKind.Variable;
        }
        
        public IqlVariableExpression() : this(null, null)
        {
        }

        public string EntityTypeName { get; set; }

        public string VariableName { get; set; }

		public override IqlExpression Clone()
		{
			// #CloneStart

			var expression = new IqlVariableExpression();
			expression.EntityTypeName = EntityTypeName;
			expression.VariableName = VariableName;
			expression.Value = Value;
			expression.InferredReturnType = InferredReturnType;
			expression.Key = Key;
			expression.Kind = Kind;
			expression.ReturnType = ReturnType;
			expression.Parent = Parent?.Clone();
			return expression;

			// #CloneEnd
		}

		internal override void FlattenInternal(IqlFlattenContext context)
        {
			// #FlattenStart

				context.Flatten(Parent);

			// #FlattenEnd
        }

		internal override IqlExpression ReplaceExpressions(ReplaceContext context)
		{
			// #ReplaceStart

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
