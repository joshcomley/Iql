using System.Collections.Generic;
using System;
using Iql.Extensions;

namespace Iql
{
    public class IqlVariableExpression : IqlLiteralExpression
    {
        private string _entityTypeName;

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

        public string EntityTypeName
        {
            get => _entityTypeName;
            set
            {
                if (value == "InferredValueContext`1")
                {
                    int a = 0;
                }
                _entityTypeName = value;
            }
        }

        public string VariableName { get; set; }


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
            if (replaced != this)
            {
                return replaced;
            }
            return this;

            // #ReplaceEnd
        }

        public static IqlVariableExpression Clone(IqlVariableExpression source)
        {
            // #CloneStart

            var expression = new IqlVariableExpression();
            expression.EntityTypeName = source.EntityTypeName;
            expression.VariableName = source.VariableName;
            expression.Value = source.Value?.TryCloneIql();
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
