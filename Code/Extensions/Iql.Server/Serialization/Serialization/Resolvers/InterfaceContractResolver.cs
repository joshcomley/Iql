using System;
using System.Collections.Generic;
using System.Linq;
using Iql.Entities;
using Iql.Entities.DisplayFormatting;
using Iql.Entities.Functions;
using Iql.Entities.Geography;
using Iql.Entities.InferredValues;
using Iql.Entities.NestedSets;
using Iql.Entities.PropertyGroups.Dates;
using Iql.Entities.PropertyGroups.Files;
using Iql.Entities.Relationships;
using Iql.Entities.Rules;
using Iql.Entities.Rules.Display;
using Iql.Entities.Rules.Relationship;
using Iql.Entities.SpecialTypes;
using Iql.Events;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Iql.Server.Serialization.Serialization.Resolvers
{
    internal class InterfaceContractResolver : DefaultContractResolver
    {
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            //return base.CreateProperties(type, memberSerialization)
            var ignoreProperties = new List<string>();
            Type resolvedType = type;
            if (typeof(IEventUnsubscriber).IsAssignableFrom(type) &&
                typeof(IEventSubscriberSubscriber).IsAssignableFrom(type))
            {
                ignoreProperties.AddRange(new [] { nameof(IEventUnsubscriber.OnUnsubscribe), nameof(IEventSubscriberSubscriber.OnSubscribe) });
            }
            if (typeof(IEventUnsubscriber).IsAssignableFrom(type))
            {
                ignoreProperties.AddRange(new [] { nameof(IEventUnsubscriber.OnUnsubscribe) });
            }
            if (typeof(IEventSubscriberSubscriber).IsAssignableFrom(type))
            {
                ignoreProperties.AddRange(new [] { nameof(IEventSubscriberSubscriber.OnSubscribe) });
            }
            if (typeof(SpecialTypeDefinition).IsAssignableFrom(type))
            {
                ignoreProperties.AddRange(new [] { nameof(SpecialTypeDefinition.InternalType), nameof(SpecialTypeDefinition.EntityConfiguration) });
            }
            if (typeof(PropertyMap).IsAssignableFrom(type))
            {
                ignoreProperties.AddRange(new[] { nameof(PropertyMap.EntityConfiguration) });
            }
            if (typeof(IqlMethod).IsAssignableFrom(type))
            {
                ignoreProperties.AddRange(new[] { nameof(IqlMethod.EntityConfiguration) });
            }
            if (typeof(IUserPermission).IsAssignableFrom(type))
            {
                ignoreProperties.AddRange(new[] {nameof(IUserPermission.Permissions)});
            }
            if (typeof(RelationshipMapping).IsAssignableFrom(type) ||
                typeof(ValueMapping).IsAssignableFrom(type))
            {
                ignoreProperties.AddRange(new [] { nameof(IMapping<object>.Container) });
            }
            if (typeof(IEntityConfiguration).IsAssignableFrom(type))
            {
                resolvedType = typeof(IEntityMetadata);
            }
            if (typeof(IInferredValueConfiguration).IsAssignableFrom(type))
            {
                ignoreProperties.AddRange(new[] { nameof(IInferredValueConfiguration.Property) });
            }
            if (typeof(IqlUserPermissionRule).IsAssignableFrom(type))
            {
                ignoreProperties.AddRange(new[]
                {
                    nameof(IqlUserPermissionRule.EntityConfigurationBuilder),
                    nameof(IqlUserPermissionRule.UserType)
                });
            }
            if (typeof(IProperty).IsAssignableFrom(type))
            {
                resolvedType = typeof(IPropertyMetadata);
                ignoreProperties.AddRange(new [] { nameof(IProperty.EntityConfiguration) });
            }
            if (typeof(ITypeDefinition).IsAssignableFrom(type))
            {
                resolvedType = typeof(ITypeConfiguration);
            }
            if (typeof(IRelationship).IsAssignableFrom(type))
            {
                resolvedType = typeof(IRelationship);
            }
            if (typeof(IRelationshipDetail).IsAssignableFrom(type))
            {
                resolvedType = typeof(IRelationshipDetailMetadata);
                ignoreProperties.AddRange(new [] { nameof(IRelationshipDetailMetadata.EntityConfiguration) });
            }
            if (typeof(IDisplayFormatting).IsAssignableFrom(type))
            {
                resolvedType = typeof(IDisplayFormatting);
            }
            if (typeof(IEntityDisplayTextFormatter).IsAssignableFrom(type))
            {
                ignoreProperties.AddRange(new [] { nameof(IEntityDisplayTextFormatter.Format) });
            }
            if (typeof(IDisplayRule).IsAssignableFrom(type))
            {
                ignoreProperties.AddRange(new [] { nameof(IRuleBase<string>.Run) });
            }
            if (typeof(IGeographicPoint).IsAssignableFrom(type))
            {
                ignoreProperties.AddRange(new [] { nameof(IGeographicPoint.EntityConfiguration) });
            }
            if (typeof(IDateRange).IsAssignableFrom(type))
            {
                ignoreProperties.AddRange(new [] { nameof(IDateRange.EntityConfiguration) });
            }
            if (typeof(IFile).IsAssignableFrom(type))
            {
                ignoreProperties.AddRange(new [] { nameof(IFile.EntityConfiguration), nameof(IFile.RootFile) });
            }
            if (typeof(IFilePreview).IsAssignableFrom(type))
            {
                ignoreProperties.AddRange(new [] { nameof(IFile.EntityConfiguration), nameof(IFilePreview.File), nameof(IFilePreview.RootFile) });
            }
            if (typeof(INestedSet).IsAssignableFrom(type))
            {
                ignoreProperties.AddRange(new [] { nameof(INestedSet.EntityConfiguration) });
            }
            if (typeof(IMediaKey).IsAssignableFrom(type))
            {
                ignoreProperties.AddRange(new [] { nameof(IMediaKey.File) });
            }
            if (typeof(IMediaKeyGroup).IsAssignableFrom(type))
            {
                ignoreProperties.AddRange(new [] { nameof(IMediaKeyGroup.MediaKey) });
            }
            if (typeof(IMediaKeyPart).IsAssignableFrom(type))
            {
                ignoreProperties.AddRange(new [] { nameof(IMediaKeyPart.MediaKey) });
            }
            if (typeof(IEntityKey).IsAssignableFrom(type))
            {
                ignoreProperties.AddRange(new [] { nameof(IEntityKey.Type), nameof(IEntityKey.KeyType) });
            }
            if (typeof(IRelationshipRule).IsAssignableFrom(type))
            {
                ignoreProperties.AddRange(new [] { nameof(IRuleBase<string>.Run) });
            }
            if (typeof(IBinaryRule).IsAssignableFrom(type))
            {
                ignoreProperties.AddRange(new [] { nameof(IRuleBase<string>.Run) });
            }
            if (typeof(IPropertyPath).IsAssignableFrom(type))
            {
                ignoreProperties.AddRange(new [] { nameof(IPropertyPath.Property), nameof(IPropertyPath.EntityConfiguration) });
            }
            if (typeof(IPropertyGroup).IsAssignableFrom(type))
            {
                ignoreProperties.AddRange(new [] { nameof(IPropertyPath.PropertyGroup), nameof(IPropertyPath.EntityConfiguration) });
            }

            ignoreProperties = ignoreProperties.Distinct().ToList();
            //if (resolvedType == typeof(IRuleCollection<IBinaryRule>))
            //{
            //    return base.CreateProperties(typeof(IRuleCollection<IBinaryRule>), memberSerialization);
            //}
            //if (typeof(IRuleCollection<IDisplayRule>).IsAssignableFrom(type))
            //{
            //    int a = 0;
            //}
            //if (typeof(IRuleCollection<IRelationshipRule>).IsAssignableFrom(type))
            //{
            //    int a = 0;
            //}

            //if (resolvedType == typeof(IBinaryRule))
            //{
            //    int a = 0;
            //}
            //IList<JsonProperty> properties = base.CreateProperties(type, memberSerialization);
            return base.CreateProperties(resolvedType, memberSerialization)
                .Where(p => !ignoreProperties.Contains(p.PropertyName))
                .ToArray();
        }
    }
}