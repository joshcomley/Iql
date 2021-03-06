﻿using System;
using System.Linq.Expressions;

namespace Iql.Entities.SpecialTypes
{
    public class UsersDefinition : SpecialTypeDefinition
    {
        private PropertyMap[] _propertyMaps;
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
            if (entityType.Properties.Count == 0)
            {
                entityType
                    .DefineProperty(_ => _.Id, true, IqlType.Guid)
                    .DefineProperty(_ => _.Name, false, IqlType.String)
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
                    new PropertyMap(EntityConfiguration,
                        nameof(IqlUser.Id), IdProperty),
                    new PropertyMap(EntityConfiguration,
                        nameof(IqlUser.Name),
                        NameProperty),
                };

        public override IPropertyGroup[] GetSpecialTypeProperties()
        {
            return new[] { NameProperty };
        }


        public override PropertyGroupMetadata[] GetPropertyGroupMetadata()
        {
            return new PropertyGroupMetadata[]
            {
                new PropertyGroupMetadata(IdProperty, IqlPropertySearchKind.None),
                new PropertyGroupMetadata(NameProperty, IqlPropertySearchKind.Primary),
            };
        }
    }
}