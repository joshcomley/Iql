using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Iql.Entities.DisplayFormatting
{
    public class DisplayFormatting<TEntity> : IDisplayFormatting
        where TEntity : class
    {
        public EntityConfiguration<TEntity> EntityConfiguration { get; }

        internal DisplayFormatting(EntityConfiguration<TEntity> entityConfiguration)
        {
            EntityConfiguration = entityConfiguration;
        }

        private readonly Dictionary<string, EntityDisplayTextFormatter<TEntity>> _formatters
            = new Dictionary<string, EntityDisplayTextFormatter<TEntity>>();

        public IEnumerable<EntityDisplayTextFormatter<TEntity>> All => _formatters.Values.ToArray();

        public EntityDisplayTextFormatter<TEntity> Default { get; private set; }

        public EntityDisplayTextFormatter<TEntity> Get(string key)
        {
            if (key == null)
            {
                return Default;
            }

            if (_formatters.ContainsKey(key))
            {
                return _formatters[key];
            }

            return null;
        }

        public EntityDisplayTextFormatter<TEntity> Set(Expression<Func<TEntity, string>> expression, string key = null)
        {
            var formatter = new EntityDisplayTextFormatter<TEntity>(expression, key);
            if (key == null)
            {
                key = "Default";
                Default = formatter;
            }
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

        public string FormatWith(TEntity entity, string key = null)
        {
            var formatter = Get(key);
            if (formatter == null)
            {
                return "";
            }
            return Default.Format(entity);
        }

        public string TryFormat(TEntity entity, string key = null)
        {
            var formatter = Get(key);
            if (formatter == null)
            {
                var titleProperties = EntityConfiguration.OrderedProperties().Where(p => p.HasHint(KnownHints.Title)).ToArray();
                if (titleProperties.Any())
                {
                    var parts = new List<string>();

                    for (var i = 0; i < titleProperties.Length; i++)
                    {
                        var property = titleProperties[i];
                        var value = property.PropertyGetter(entity);
                        if (value != null)
                        {
                            parts.Add("" + value);
                        }
                    }

                    return string.Join(" - ", parts);
                }

                var firstSearchProperty = EntityConfiguration.Properties.FirstOrDefault(p => p.SearchKind == PropertySearchKind.Primary);
                if (firstSearchProperty != null)
                {
                    return "" + (firstSearchProperty.PropertyGetter(entity) ?? "");
                }

                var firstNameProperty =
                    TryFindProperty("FirstName", "ForeName", "ChristianName");
                var lastNameProperty =
                    TryFindProperty("LastName", "Surname");

                if (firstNameProperty != null && lastNameProperty != null)
                {
                    return
                        $"{firstNameProperty.PropertyGetter(entity) ?? ""} {lastNameProperty.PropertyGetter(entity) ?? ""}";
                }

                var formatterProperty =
                    TryFindProperty("Name", "FullName", "Title", "Id")
                    ;

                if (formatterProperty != null)
                {
                    return "" + (formatterProperty.PropertyGetter(entity) ?? "");
                }
                return "";
            }
            return Default.Format(entity);
        }

        private IProperty TryFindProperty(params string[] names)
        {
            foreach (var name in names)
            {
                var property = EntityConfiguration.Properties.FirstOrDefault(p => p.Name.ToLower() == name.ToLower());
                if (property != null)
                {
                    return property;
                }
            }

            return null;
        }

        IEntityDisplayTextFormatter IDisplayFormatting.Default => Default;
        IEntityDisplayTextFormatter IDisplayFormatting.Get(string key) => Get(key);

        private Func<object, string> _nonTypedExpression = null;
        IEntityDisplayTextFormatter IDisplayFormatting.Set(Expression<Func<object, string>> expression, string key = null)
        {
            _nonTypedExpression = expression.Compile();
            return Set(e => _nonTypedExpression(e), key);
        }
    }
}