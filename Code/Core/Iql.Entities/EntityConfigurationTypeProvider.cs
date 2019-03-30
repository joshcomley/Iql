using System;
using Iql.Entities.SpecialTypes;

namespace Iql.Entities
{
    public class EntityConfigurationTypeProvider : IIqlTypeMetadata
    {
        public object UnderlyingObject => EntityConfiguration;
        public IEntityConfiguration EntityConfiguration { get; }

        public EntityConfigurationTypeProvider(IEntityConfiguration entityConfiguration)
        {
            EntityConfiguration = entityConfiguration;
        }

        public IGenericTypeParameter[] GenericTypeParameters { get; } = new IGenericTypeParameter[] { };
        public Type Type => EntityConfiguration.Type;

        public ITypeProperty FindProperty(string name)
        {
            return EntityConfiguration.FindProperty(name)?.PropertyMetadata;
        }

        public SpecialTypeDefinition SpecialTypeDefinition => EntityConfiguration.SpecialTypeDefinition;
    }
}