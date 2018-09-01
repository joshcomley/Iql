using System;

namespace Iql.Entities.DisplayFormatting
{
    public class InlineFormatterContext<TEntity> : FormatterContext<TEntity>
    {
        public Func<FormatterContext<TEntity>, IqlExpression, object, string> Expression { get; }

        public InlineFormatterContext(TEntity entity, Func<FormatterContext<TEntity>, IqlExpression, object, string> expression) : base(entity)
        {
            Expression = expression;
            Context = new FormatterContext<TEntity>(entity);
        }

        public FormatterContext<TEntity> Context { get; set; }

        public override string Format(IqlExpression expression, object value)
        {
            return Expression(Context, expression, value);
        }
    }
}