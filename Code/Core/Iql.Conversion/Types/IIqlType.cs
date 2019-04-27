using System;
using Iql.Parsing.Types;

namespace Iql.Entities
{
    public interface IIqlTypeMetadataProvider
    {
        IIqlTypeMetadata TypeMetadata { get; }
    }

    public interface IGenericTypeParameter
    {
        string Name { get; }
        IIqlTypeMetadata Type { get; }
    }

    public interface IIqlTypeMetadata
    {
        IGenericTypeParameter[] GenericTypeParameters { get; }
        Type Type { get; }
        string TypeName { get; }
        ITypeProperty FindProperty(string name);
        object UnderlyingObject { get; }
        //SpecialTypeDefinition SpecialTypeDefinition { get; }
        //IEntityConfiguration EntityConfiguration { get; }
    }
}