using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Iql.Queryable.Data.EntityConfiguration.Validation
{

    public class ValidationCollection<TEntity> : IValidationCollection
    {
        private readonly Dictionary<string, Validation<TEntity>> _validationsDictionary
            = new Dictionary<string, Validation<TEntity>>();

        public Validation<TEntity> Get(string key)
        {
            if (_validationsDictionary.ContainsKey(key))
            {
                return _validationsDictionary[key];
            }

            return null;
        }

        public List<Validation<TEntity>> All { get; } = new List<Validation<TEntity>>();

        IEnumerable<IValidation> IValidationCollection.All => All;

        public Validation<TEntity> Add(Expression<Func<TEntity, bool>> expression, string key, string message)
        {
            var validation = new Validation<TEntity>(expression, key, message);
            if (_validationsDictionary.ContainsKey(key))
            {
                var old = _validationsDictionary[key];
                _validationsDictionary[key] = validation;
                All.Remove(old);
            }
            else
            {
                _validationsDictionary.Add(key, validation);
            }

            All.Add(validation);
            return validation;
        }

        public void Remove(string key)
        {
            if (_validationsDictionary.ContainsKey(key))
            {
                var old = _validationsDictionary[key];
                _validationsDictionary.Remove(key);
                All.Remove(old);
            }
        }
        IValidation IValidationCollection.Get(string key) => Get(key);

        IValidation IValidationCollection.Add(Expression<Func<object,bool>> expression, string key, string message)
        {
            var compiled = expression.Compile();
            return Add(e => compiled(e), key, message);
        }
    }
}