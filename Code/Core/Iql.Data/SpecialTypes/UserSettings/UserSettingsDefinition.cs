using System;
using System.Linq.Expressions;

namespace Iql.Entities.SpecialTypes
{
    public class UserSettingsDefinition : SpecialTypeDefinition
    {
        private PropertyMap[] _propertyMaps;
        public override Type InternalType => typeof(IqlUserSetting);

        public IProperty UserIdProperty { get; set; }
        public IProperty Key1Property { get; set; }
        public IProperty Key2Property { get; set; }
        public IProperty Key3Property { get; set; }
        public IProperty Key4Property { get; set; }
        public IProperty ValueProperty { get; set; }

        public UserSettingsDefinition(IProperty idProperty, IProperty userIdProperty, IProperty key1Property, IProperty key2Property, IProperty key3Property, IProperty key4Property, IProperty valueProperty) : base(idProperty)
        {
            UserIdProperty = userIdProperty;
            Key1Property = key1Property;
            Key2Property = key2Property;
            Key3Property = key3Property;
            Key4Property = key4Property;
            ValueProperty = valueProperty;
        }

        public static UserSettingsDefinition Define<T>(
            EntityConfiguration<T> entityConfiguration,
            Expression<Func<T, object>> idProperty,
            Expression<Func<T, object>> userIdProperty,
            Expression<Func<T, object>> key1Property,
            Expression<Func<T, object>> key2Property,
            Expression<Func<T, object>> key3Property,
            Expression<Func<T, object>> key4Property,
            Expression<Func<T, object>> valueProperty)
            where T : class
        {
            var definition = new UserSettingsDefinition(
                entityConfiguration.FindNestedPropertyByLambdaExpression(idProperty),
                entityConfiguration.FindNestedPropertyByLambdaExpression(userIdProperty),
                entityConfiguration.FindNestedPropertyByLambdaExpression(key1Property),
                entityConfiguration.FindNestedPropertyByLambdaExpression(key2Property),
                entityConfiguration.FindNestedPropertyByLambdaExpression(key3Property),
                entityConfiguration.FindNestedPropertyByLambdaExpression(key4Property),
                entityConfiguration.FindNestedPropertyByLambdaExpression(valueProperty));
            entityConfiguration.SpecialTypeDefinition = definition;
            var entityType = entityConfiguration.Builder.EntityType<IqlUserSetting>();
            entityType.ManageKind = EntityManageKind.None;
            entityType.SpecialTypeDefinition = definition;
            if (entityType.Properties.Count == 0)
            {
                entityType
                    .DefineProperty(_ => _.Id, true, IqlType.Guid)
                    .DefineProperty(_ => _.UserId, true, IqlType.String)
                    .DefineProperty(_ => _.Key1, false, IqlType.String)
                    .DefineProperty(_ => _.Key2, true, IqlType.String)
                    .DefineProperty(_ => _.Key3, true, IqlType.String)
                    .DefineProperty(_ => _.Key4, true, IqlType.String)
                    .DefineProperty(_ => _.Value, true, IqlType.String)
                    .HasKey(_ => _.Id)
                    ;
            }
            return definition;
        }

        public override PropertyMap[] PropertyMaps =>
            _propertyMaps =
                _propertyMaps ??
                new[]
                {
                    new PropertyMap(EntityConfiguration, nameof(IqlUserSetting.Id), IdProperty),
                    new PropertyMap(EntityConfiguration, nameof(IqlUserSetting.UserId), UserIdProperty),
                    new PropertyMap(EntityConfiguration, nameof(IqlUserSetting.Key1), Key1Property),
                    new PropertyMap(EntityConfiguration, nameof(IqlUserSetting.Key2), Key2Property),
                    new PropertyMap(EntityConfiguration, nameof(IqlUserSetting.Key3), Key3Property),
                    new PropertyMap(EntityConfiguration, nameof(IqlUserSetting.Key4), Key4Property),
                    new PropertyMap(EntityConfiguration, nameof(IqlUserSetting.Value), ValueProperty),
                };


        public override PropertyGroupMetadata[] GetPropertyGroupMetadata()
        {
            return new PropertyGroupMetadata[]
            {
                new PropertyGroupMetadata(IdProperty, PropertySearchKind.None),
                new PropertyGroupMetadata(UserIdProperty, PropertySearchKind.None),
                new PropertyGroupMetadata(Key1Property, PropertySearchKind.Secondary),
                new PropertyGroupMetadata(Key2Property, PropertySearchKind.Secondary),
                new PropertyGroupMetadata(Key3Property, PropertySearchKind.Secondary),
                new PropertyGroupMetadata(Key4Property, PropertySearchKind.Secondary),
                new PropertyGroupMetadata(ValueProperty, PropertySearchKind.None),
            };
        }

        public override IPropertyGroup[] GetSpecialTypeProperties()
        {
            return new[] { UserIdProperty, Key1Property, Key2Property, Key3Property, Key4Property, ValueProperty };
        }
    }
}