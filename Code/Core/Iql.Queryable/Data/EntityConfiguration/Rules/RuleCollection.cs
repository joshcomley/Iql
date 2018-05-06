using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Iql.Queryable.Data.EntityConfiguration.Rules
{
    public abstract class RuleCollection<TEntity, TRule> : IRuleCollection
    where TRule : Rule<TEntity>
    {
        private readonly Dictionary<string, TRule> _validationsDictionary
            = new Dictionary<string, TRule>();

        public TRule Get(string key)
        {
            if (_validationsDictionary.ContainsKey(key))
            {
                return _validationsDictionary[key];
            }

            return null;
        }

        public List<TRule> All { get; } = new List<TRule>();

        IEnumerable<IRule> IRuleCollection.All => All;

        public TRule Add(Expression<Func<TEntity, bool>> expression, string key, string message)
        {
            var validation = NewRule(expression, key, message);
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

        protected abstract TRule NewRule(Expression<Func<TEntity, bool>> expression, string key, string message);

        public void Remove(string key)
        {
            if (_validationsDictionary.ContainsKey(key))
            {
                var old = _validationsDictionary[key];
                _validationsDictionary.Remove(key);
                All.Remove(old);
            }
        }
        IRule IRuleCollection.Get(string key) => Get(key);

        IRule IRuleCollection.Add(Expression<Func<object, bool>> expression, string key, string message)
        {
            var compiled = expression.Compile();
            return Add(e => compiled(e), key, message);
        }
    }
}