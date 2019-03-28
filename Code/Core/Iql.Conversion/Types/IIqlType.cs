using System;
using Iql.Parsing.Types;

namespace Iql.Entities
{
    public interface IIqlTypeMetadataProvider
    {
        IIqlTypeMetadata TypeMetadata { get; }
    }
    
    public interface IIqlTypeMetadata
    {
        IIqlTypeMetadata[] GenericTypeParameters { get; }
        Type Type { get; }
        ITypeProperty FindProperty(string name);
        object UnderlyingObject { get; }
        //SpecialTypeDefinition SpecialTypeDefinition { get; }
        //IEntityConfiguration EntityConfiguration { get; }
    }
}