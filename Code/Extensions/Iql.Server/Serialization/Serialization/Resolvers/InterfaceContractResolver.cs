using System;
using System.Collections.Generic;
using System.Linq;
using Iql.Entities;
using Iql.Entities.DisplayFormatting;
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
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Iql.Server.Serialization.Serialization.Resolvers
{
    internal class InterfaceContractResolver : DefaultContractResolver
    {
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            if (typeof(SpecialTypeDefinition).IsAssignableFrom(type))
            {
                return base.CreateProperties(type, memberSerialization)
                    .Where(p => !new string[]{nameof(SpecialTypeDefinition.InternalType), nameof(SpecialTypeDefinition.EntityConfiguration)}.Contains(p.PropertyName))
                    .ToList();
            }

            if (typeof(RelationshipMapping).IsAssignableFrom(type) ||
                typeof(ValueMapping).IsAssignableFrom(type))
            {
                return base.CreateProperties(type, memberSerialization)
                    .Where(p => p.PropertyName != nameof(IMapping<object>.Container))
                    .ToList();
            }
            if (typeof(IEntityConfiguration).IsAssignableFrom(type))
            {
                return base.CreateProperties(typeof(IEntityMetadata), memberSerialization);
            }
            if (typeof(IInferredValueConfiguration).IsAssignableFrom(type))
            {
                return base.CreateProperties(typeof(IInferredValueConfiguration), memberSerialization)
                    .Where(p => p.PropertyName != nameof(IInferredValueConfiguration.Property))
                    .ToList();
            }
            if (typeof(IProperty).IsAssignableFrom(type))
            {
                return base.CreateProperties(typeof(IPropertyMetadata), memberSerialization)
                    .Where(p => p.PropertyName != nameof(IProperty.EntityConfiguration))
                    .ToList();
            }
            if (typeof(ITypeDefinition).IsAssignableFrom(type))
            {
                return base.CreateProperties(typeof(ITypeConfiguration), memberSerialization);
            }
            if (typeof(IRelationship).IsAssignableFrom(type))
            {
                return base.CreateProperties(typeof(IRelationship), memberSerialization);
            }
            if (typeof(IRelationshipDetail).IsAssignableFrom(type))
            {
                return base.CreateProperties(typeof(IRelationshipDetailMetadata), memberSerialization)
                    .Where(p => p.PropertyName != nameof(IRelationshipDetailMetadata.EntityConfiguration))
                    .ToList();
            }
            if (typeof(IDisplayFormatting).IsAssignableFrom(type))
            {
                return base.CreateProperties(typeof(IDisplayFormatting), memberSerialization);
            }
            if (typeof(IEntityDisplayTextFormatter).IsAssignableFrom(type))
            {
                return base.CreateProperties(typeof(IEntityDisplayTextFormatter), memberSerialization)
                    .Where(p => p.PropertyName != nameof(IEntityDisplayTextFormatter.Format))
                    .ToList();
            }
            if (typeof(IDisplayRule).IsAssignableFrom(type))
            {
                return base.CreateProperties(typeof(IDisplayRule), memberSerialization)
                    .Where(p => p.PropertyName != nameof(IRuleBase<string>.Run))
                    .ToList();
            }
            if (typeof(IGeographicPoint).IsAssignableFrom(type))
            {
                return base.CreateProperties(typeof(IGeographicPoint), memberSerialization)
                    .Where(p => p.PropertyName != nameof(IGeographicPoint.EntityConfiguration))
                    .ToList();
            }
            if (typeof(IDateRange).IsAssignableFrom(type))
            {
                return base.CreateProperties(typeof(IDateRange), memberSerialization)
                    .Where(p => p.PropertyName != nameof(IDateRange.EntityConfiguration))
                    .ToList();
            }
            if (typeof(IFile).IsAssignableFrom(type))
            {
                return base.CreateProperties(typeof(IFile), memberSerialization)
                    .Where(p => p.PropertyName != nameof(IFile.EntityConfiguration) && p.PropertyName != nameof(IFile.RootFile))
                    .ToList();
            }
            if (typeof(IFilePreview).IsAssignableFrom(type))
            {
                return base.CreateProperties(typeof(IFilePreview), memberSerialization)
                    .Where(p => p.PropertyName != nameof(IFilePreview.EntityConfiguration) &&
                                p.PropertyName != nameof(IFilePreview.File) &&
                                p.PropertyName != nameof(IFilePreview.RootFile))
                    .ToList();
            }
            if (typeof(INestedSet).IsAssignableFrom(type))
            {
                return base.CreateProperties(typeof(INestedSet), memberSerialization)
                    .Where(p => p.PropertyName != nameof(INestedSet.EntityConfiguration))
                    .ToList();
            }
            if (typeof(IMediaKey).IsAssignableFrom(type))
            {
                return base.CreateProperties(typeof(IMediaKey), memberSerialization)
                    .Where(p => p.PropertyName != nameof(IMediaKey.File))
                    .ToList();
            }
            if (typeof(IMediaKeyGroup).IsAssignableFrom(type))
            {
                return base.CreateProperties(typeof(IMediaKeyGroup), memberSerialization)
                    .Where(p => p.PropertyName != nameof(IMediaKeyGroup.MediaKey))
                    .ToList();
            }
            if (typeof(IMediaKeyPart).IsAssignableFrom(type))
            {
                return base.CreateProperties(typeof(IMediaKeyPart), memberSerialization)
                    .Where(p => p.PropertyName != nameof(IMediaKeyPart.MediaKey))
                    .ToList();
            }
            if (typeof(IEntityKey).IsAssignableFrom(type))
            {
                return base.CreateProperties(typeof(IEntityKey), memberSerialization)
                    .Where(p => p.PropertyName != nameof(IEntityKey.Type) &&
                                p.PropertyName != nameof(IEntityKey.KeyType))
                    .ToList();
            }
            if (typeof(IRelationshipRule).IsAssignableFrom(type))
            {
                return base.CreateProperties(typeof(IRelationshipRule), memberSerialization)
                    .Where(p => p.PropertyName != nameof(IRuleBase<string>.Run))
                    .ToList();
            }
            if (typeof(IBinaryRule).IsAssignableFrom(type))
            {
                return base.CreateProperties(typeof(IBinaryRule), memberSerialization)
                    .Where(p => p.PropertyName != nameof(IRuleBase<string>.Run))
                    .ToList();
            }
            if (typeof(IPropertyPath).IsAssignableFrom(type))
            {
                return base.CreateProperties(type, memberSerialization)
                    .Where(p => p.PropertyName != nameof(IPropertyPath.Property))
                    .Where(p => p.PropertyName != nameof(IPropertyPath.EntityConfiguration))
                    .ToList();
            }
            if (typeof(IPropertyGroup).IsAssignableFrom(type))
            {
                return base.CreateProperties(type, memberSerialization)
                    .Where(p => p.PropertyName != nameof(IProperty.EntityConfiguration))
                    .ToList();
            }
            //if (type == typeof(IRuleCollection<IBinaryRule>))
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

            //if (type == typeof(IBinaryRule))
            //{
            //    int a = 0;
            //}
            //IList<JsonProperty> properties = base.CreateProperties(type, memberSerialization);
            return base.CreateProperties(type, memberSerialization);
        }
    }
}