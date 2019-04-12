using Iql.Entities;

namespace Iql.Data.Types
{
    public class EntityConfigurationTypeResolver : TypeResolver
    {
        public IEntityConfigurationBuilder Builder { get; }

        public EntityConfigurationTypeResolver(IEntityConfigurationBuilder builder)
        {
            Builder = builder;
        }
        public override IIqlTypeMetadata ResolveTypeFromTypeName(string fullTypeName)
        {
            return Builder.GetEntityByTypeName(CleanTypeName(fullTypeName))?.TypeMetadata ?? base.ResolveTypeFromTypeName(fullTypeName);
        }

        public override IIqlTypeMetadata GetTypeMap(IIqlTypeMetadata type)
        {
            return Builder.GetTypeMap(type);
        }
    }
}