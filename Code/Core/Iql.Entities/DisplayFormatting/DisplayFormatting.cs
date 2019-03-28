using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Iql.Entities.Extensions;

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

        public EntityDisplayTextFormatter<TEntity> Default => All?.SingleOrDefault(_ => _.Key == "Default") ?? All?.FirstOrDefault();

        public EntityDisplayTextFormatter<TEntity> Get(string key = null)
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
            if (key == null)
            {
                key = "Default";
            }
            var formatter = new EntityDisplayTextFormatter<TEntity>(EntityConfiguration, expression, key);
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

        public AutoFormattingResult ResolveAutoProperties()
        {
            return AutoFormatting(EntityConfiguration);
        }

        public static AutoFormattingResult AutoFormatting(IEntityConfiguration entityConfiguration)
        {
            var titleProperties = entityConfiguration.GetGroupProperties().SelectMany(p => p.FlattenToValueProperties())
                .Where(p => p.HasHint(KnownHints.Title)).ToArray();
            if (titleProperties.Any())
            {
                return new AutoFormattingResult(AutoFormattingKind.Title, titleProperties);
            }

            var firstSearchProperty = entityConfiguration.Properties.FirstOrDefault(p => p.SearchKind == PropertySearchKind.Primary);
            if (firstSearchProperty != null)
            {
                return new AutoFormattingResult(AutoFormattingKind.FirstSearchProperty, new IProperty[] { firstSearchProperty });
            }

            var firstNameProperty =
                entityConfiguration.TryMatchProperty("FirstName", "ForeName", "ChristianName");
            var lastNameProperty =
                entityConfiguration.TryMatchProperty("LastName", "Surname");

            if (firstNameProperty != null && lastNameProperty != null)
            {
                return new AutoFormattingResult(AutoFormattingKind.FirstNameLastName, new IProperty[] { firstNameProperty[0], lastNameProperty[0] });
            }

            var nameProperty =
                    entityConfiguration.TryMatchProperty("Name", "FullName", "Title")
                ;
            if (nameProperty != null)
            {
                return new AutoFormattingResult(AutoFormattingKind.Name, new IProperty[] { nameProperty[0] });
            }
            var idProperty =
                    entityConfiguration.TryMatchProperty("Id", "Key")
                ;
            if (idProperty != null)
            {
                return new AutoFormattingResult(AutoFormattingKind.Id, new IProperty[] { idProperty[0] });
            }
            return new AutoFormattingResult(AutoFormattingKind.NoneFound, new IProperty[] { idProperty[0] });
        }

        public string TryFormat(TEntity entity, string key = null)
        {
            var formatter = Get(key);
            if (formatter == null)
            {
                formatter = All?.FirstOrDefault();
            }
            if (formatter == null)
            {
                var autoFormattingResult = ResolveAutoProperties();
                switch (autoFormattingResult.Kind)
                {
                    case AutoFormattingKind.Title:
                        var parts = new List<string>();

                        for (var i = 0; i < autoFormattingResult.Properties.Length; i++)
                        {
                            var property = autoFormattingResult.Properties[i];
                            var value = property.GetValue(entity);
                            if (value != null)
                            {
                                parts.Add("" + value);
                            }
                        }

                        return string.Join(" - ", parts);
                    case AutoFormattingKind.FirstSearchProperty:
                        return "" + (autoFormattingResult.Properties[0].GetValue(entity) ?? "");
                    case AutoFormattingKind.FirstNameLastName:
                        return
                            $"{autoFormattingResult.Properties[0].GetValue(entity) ?? ""} {autoFormattingResult.Properties[1].GetValue(entity) ?? ""}";
                    case AutoFormattingKind.Name:
                    case AutoFormattingKind.Id:
                        return "" + (autoFormattingResult.Properties[0].GetValue(entity) ?? "");
                }
                return "";
            }
            return formatter.Format(entity);
        }

        IEntityDisplayTextFormatter IDisplayFormatting.Default => Default;

        IEnumerable<IEntityDisplayTextFormatter> IDisplayFormatting.All => All.ToArray();

        IEntityDisplayTextFormatter IDisplayFormatting.Get(string key) => Get(key);

        private Func<object, string> _nonTypedExpression = null;
        IEntityDisplayTextFormatter IDisplayFormatting.Set(Expression<Func<object, string>> expression, string key = null)
        {
            _nonTypedExpression = expression.Compile();
            return Set(e => _nonTypedExpression(e), key);
        }
    }
}