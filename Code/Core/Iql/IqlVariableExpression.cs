using System.Collections.Generic;
using System;
using Iql.Extensions;

namespace Iql
{
    public class IqlVariableExpression : IqlReferenceExpression
    {
        public IqlVariableExpression(
            string variableName = null,
            string value = null,
            Type entityType = null) : base(IqlExpressionKind.Variable, entityType.ToIqlType())
        {
            VariableName = variableName;
            Value = value;
            EntityTypeName = entityType?.GetFullName();
        }
        
        public IqlVariableExpression() : this(null, null)
        {
        }

        public string EntityTypeName { get; set; }

        public string Value { get; set; }

        public string VariableName { get; set; }

		public override IqlExpression Clone()
		{
			// #CloneStart

			var expression = new IqlVariableExpression();
			expression.EntityTypeName = EntityTypeName;
			expression.Value = Value;
			expression.VariableName = VariableName;
			expression.Key = Key;
			expression.Kind = Kind;
			expression.ReturnType = ReturnType;
			expression.Parent = Parent?.Clone();
			return expression;

			// #CloneEnd
		}

		internal override void FlattenInternal(IList<IqlExpression> expressions)
        {
			// #FlattenStart

			if(expressions.Contains(this))
			{
				return;
			}
			expressions.Add(this);
			Parent?.FlattenInternal(expressions);

			// #FlattenEnd
        }
    }
}
