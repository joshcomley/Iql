using System;
using Iql.Data.Evaluation;
using Iql.Entities;
using Iql.Events;

namespace Iql.Parsing.Types
{
    public interface ITypeResolver
    {
        //IIqlTypeMetadata FindVariableType(string variableName);
        EventEmitter<string> ResolvingType { get; }
        IIqlTypeMetadata FindType<T>();
        IIqlTypeMetadata FindTypeByType(Type type);
        IIqlTypeMetadata ResolveTypeFromTypeName(string typeName);
        IIqlTypeMetadata GetTypeMap(IIqlTypeMetadata type);
    }
}