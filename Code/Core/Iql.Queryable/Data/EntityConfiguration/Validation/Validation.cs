using System;
using System.Linq.Expressions;

namespace Iql.Queryable.Data.EntityConfiguration.Validation
{
    public class Validation<TEntity> : IValidation
    {
        private readonly Func<object, bool> _ivalidate;

        private readonly Expression<Func<object, bool>> _ivalidationExpression;
        private readonly Func<TEntity, bool> _validate;

        public Validation(Expression<Func<TEntity, bool>> validationExpression, string key, string message)
        {
            Key = key;
            Message = message;
            ValidationExpression = validationExpression;
            _validate = validationExpression.Compile();
            _ivalidationExpression = _ => Validate((TEntity) _);
            _ivalidate = _ivalidationExpression.Compile();
        }

        public Expression<Func<TEntity, bool>> ValidationExpression { get; }
        public Func<TEntity, bool> Validate => _ => !_validate(_);
        public string Key { get; }
        public string Message { get; }
        Expression<Func<object, bool>> IValidation.ValidationExpression => _ivalidationExpression;
        Func<object, bool> IValidation.Validate => _ivalidate;
    }
}