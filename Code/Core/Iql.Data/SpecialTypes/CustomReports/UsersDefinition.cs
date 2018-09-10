using System;
using System.Linq.Expressions;

namespace Iql.Entities.SpecialTypes
{
    public class UsersDefinition : SpecialTypeDefinition
    {
        public override Type InternalType => typeof(IqlUser);
        public IProperty NameProperty { get; set; }

        public UsersDefinition(IProperty idProperty, IProperty nameProperty) : base(idProperty)
        {
            NameProperty = nameProperty;
        }

        public static UsersDefinition Define<T>(
            EntityConfiguration<T> entityConfiguration,
            Expression<Func<T, object>> idProperty,
            Expression<Func<T, object>> nameProperty)
            where T : class
        {
            var definition = new UsersDefinition(
                entityConfiguration.FindNestedPropertyByLambdaExpression(idProperty),
                entityConfiguration.FindNestedPropertyByLambdaExpression(nameProperty));
            entityConfiguration.SpecialTypeDefinition = definition;
            var entityType = entityConfiguration.Builder.EntityType<IqlUser>();
            entityType.ManageKind = EntityManageKind.None;
            entityType.SpecialTypeDefinition = definition;
            return definition;
        }


        public override IProperty ResolvePropertyMap(string internalPropertyName)
        {
            switch (internalPropertyName)
            {
                case nameof(IqlUser.Id):
                    return IdProperty;
                case nameof(IqlUser.Name):
                    return NameProperty;
            }
            return null;
        }

        public override IPropertyGroup[] GetSpecialTypeProperties()
        {
            return new[] { NameProperty };
        }
    }
}