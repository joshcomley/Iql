using Iql.OData.TypeScript.Generator.Models;

namespace Iql.OData.TypeScript.Generator.Definitions
{
    public class EntityFunctionParameterDefinition : IVariable
    {
        public EntityFunctionParameterDefinition()
        {
        }

        public EntityFunctionParameterDefinition(string name, ITypeInfo type = null, bool hasDefaultValue = false, object defaultValue = null,
            ITypeInfo[] alternativeTypes = null)
        {
            Name = name;
            TypeInfo = type ?? new TypeInfo();
            HasDefaultValue = hasDefaultValue;
            DefaultValue = defaultValue;
            AlternativeTypes = alternativeTypes;
        }

        public ITypeInfo[] AlternativeTypes { get; set; }
        public bool HasDefaultValue { get; set; }
        public object DefaultValue { get; set; }
        public string Name { get; set; }
        public ITypeInfo TypeInfo { get; set; }
        public bool Private { get; set; }
        public string ResolvedType { get; set; }
        public bool IsLocal { get; set; }
    }
}