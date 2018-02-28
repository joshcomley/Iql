using System;
using System.Linq.Expressions;

namespace Iql.Queryable.Data.EntityConfiguration
{
    public class EntityDisplayTextFormatter<TEntity> : IEntityDisplayTextFormatter
    {
        private Func<TEntity, string> _formatterFunction;
        public Expression<Func<TEntity, string>> FormatterExpression { get; }
        public string Key { get; }

        public Func<TEntity, string> Format => _formatterFunction ?? (_formatterFunction = FormatterExpression.Compile());

        LambdaExpression IEntityDisplayTextFormatter.FormatterExpression => FormatterExpression;
        Func<object, string> IEntityDisplayTextFormatter.Format => obj => Format((TEntity)obj);

        public EntityDisplayTextFormatter(Expression<Func<TEntity, string>> formatterExpression, string key)
        {
            FormatterExpression = formatterExpression;
            Key = key;
        }
    }
}