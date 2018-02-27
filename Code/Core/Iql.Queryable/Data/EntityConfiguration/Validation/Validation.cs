using System;
using System.Linq.Expressions;

namespace Iql.Queryable.Data.EntityConfiguration
{
    public class Validation<TEntity> : IValidation
    {
        public string Key { get; }
        public string Message { get; }
        public Expression<Func<TEntity, bool>> ValidationExpression { get; }
        public Func<TEntity, bool> Validate { get; }

        private readonly Expression<Func<object, bool>> _ivalidationExpression;
        Expression<Func<object, bool>> IValidation.ValidationExpression => _ivalidationExpression;
        private readonly Func<object, bool> _ivalidate;
        Func<object, bool> IValidation.Validate => _ivalidate;

        public Validation(Expression<Func<TEntity, bool>> validationExpression, string key, string message)
        {
            Key = key;
            Message = message;
            ValidationExpression = validationExpression;
            Validate = validationExpression.Compile();
            _ivalidationExpression = _ => _ivalidate(_);
            _ivalidate = _ivalidationExpression.Compile();
        }
    }
}