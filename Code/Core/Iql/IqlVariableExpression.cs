using System;
using Iql.Extensions;

namespace Iql
{
    public class IqlVariableExpression : IqlReferenceExpression
    {
        public IqlVariableExpression(
            string variableName,
            string value,
            Type entityType = null) : base(IqlExpressionType.Variable, entityType.ToIqlType())
        {
            VariableName = variableName;
            Value = value;
        }

        public IqlVariableExpression() : this(null, null)
        {
        }

        public string Value { get; set; }

        public string VariableName { get; set; }
    }
}