using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Iql.Queryable.Data.EntityConfiguration
{
    public class DisplayFormatting<TEntity> : IDisplayFormatting
    {
        private readonly Dictionary<string, EntityDisplayTextFormatter<TEntity>> _formatters 
        = new Dictionary<string, EntityDisplayTextFormatter<TEntity>>();
        public EntityDisplayTextFormatter<TEntity> Default { get; }

        public EntityDisplayTextFormatter<TEntity> Get(string key)
        {
            if (_formatters.ContainsKey(key))
            {
                return _formatters[key];
            }

            return null;
        }

        public EntityDisplayTextFormatter<TEntity> Set(Expression<Func<TEntity, string>> expression, string key = null)
        {
            if (key == null)
            {
                key = "Default";
            }
            var formatter = new EntityDisplayTextFormatter<TEntity>(expression, key);
            if (_formatters.ContainsKey(key))
            {
                _formatters[key] = formatter;
            }
            else
            {
                _formatters.Add(key, formatter);
            }

            return formatter;
        }

        IEntityDisplayTextFormatter IDisplayFormatting.Default => Default;
        IEntityDisplayTextFormatter IDisplayFormatting.Get(string key) => Get(key);

        private Func<object, string> _nonTypedExpression = null;
        IEntityDisplayTextFormatter IDisplayFormatting.Set(Expression<Func<object,string>> expression, string key = null)
        {
            _nonTypedExpression = expression.Compile();
            return Set(e => _nonTypedExpression(e), key);
        }
    }
}