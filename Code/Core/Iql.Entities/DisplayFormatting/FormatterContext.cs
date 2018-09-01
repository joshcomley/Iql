using System.Collections.Generic;

namespace Iql.Entities.DisplayFormatting
{
    public class FormatterContext<TEntity>: IFormatterContext
    {
        internal Dictionary<string, IqlExpression> ExpressionLookup = new Dictionary<string, IqlExpression>();
        public TEntity Entity { get; set; }
        object IFormatterContext.Entity
        {
            get => Entity;
            set => Entity = (TEntity) value;
        }

        public FormatterContext(TEntity entity)
        {
            Entity = entity;
        }

        public string FormatInternal(string expressionId, object value)
        {
            return Format(ExpressionLookup[expressionId], value);
        }

        public virtual string Format(IqlExpression expression, object value)
        {
            return value == null ? "" : value.ToString();
        }
    }
}