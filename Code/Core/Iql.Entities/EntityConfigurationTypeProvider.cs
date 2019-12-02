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
        private IGenericTypeParameter[] _genericTypeParameters;

        public IGenericTypeParameter[] GenericTypeParameters => _genericTypeParameters = _genericTypeParameters ?? new IGenericTypeParameter[] { };
        public Type Type => EntityConfiguration?.Type;
        public string TypeName => EntityConfiguration?.TypeName;

        public ITypeProperty FindProperty(string name)
        {
            return EntityConfiguration.FindProperty(name)?.PropertyMetadata;
        }

        public SpecialTypeDefinition SpecialTypeDefinition => EntityConfiguration.SpecialTypeDefinition;
    }
}