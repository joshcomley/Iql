using System;
using System.Linq.Expressions;

namespace Iql.Entities.SpecialTypes
{
    public class UserSettingsDefinition : SpecialTypeDefinition
    {
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
            return definition;
        }

        public override IProperty ResolvePropertyMap(string internalPropertyName)
        {
            switch (internalPropertyName)
            {
                case nameof(IqlUserSetting.Id):
                    return IdProperty;
                case nameof(IqlUserSetting.UserId):
                    return UserIdProperty;
                case nameof(IqlUserSetting.Key1):
                    return Key1Property;
                case nameof(IqlUserSetting.Key2):
                    return Key2Property;
                case nameof(IqlUserSetting.Key3):
                    return Key3Property;
                case nameof(IqlUserSetting.Key4):
                    return Key4Property;
                case nameof(IqlUserSetting.Value):
                    return ValueProperty;
            }
            return null;
        }

        public override IPropertyGroup[] GetSpecialTypeProperties()
        {
            return new[] { UserIdProperty, Key1Property, Key2Property, Key3Property, Key4Property, ValueProperty };
        }
    }
}