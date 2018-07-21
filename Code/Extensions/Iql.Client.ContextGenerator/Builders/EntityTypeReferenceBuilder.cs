using System.Linq;
using Iql.OData.TypeScript.Generator.Definitions;
using Iql.OData.TypeScript.Generator.Models;
using Iql.OData.TypeScript.Generator.Parsers;

namespace Iql.OData.TypeScript.Generator.Builders
{
    public class EntityTypeReferenceBuilder
    {
        public EntityTypeReference Build(ITypeInfo type, ODataSchema schema)
        {
            var entityTypeReference = new EntityTypeReference();
            const string collectionOpening = "Collection(";
            const string collectionClosing = ")";
            if (type.EdmType == null)
            {
                return null;
            }
            var name = type.EdmType;
            if (name.StartsWith(collectionOpening) && name.EndsWith(collectionClosing))
            {
                entityTypeReference.IsCollection = true;
                entityTypeReference.IsNullable = false;
                name = name.Substring(collectionOpening.Length,
                    name.Length - collectionOpening.Length - collectionClosing.Length);
            }
            //name.Dump();
            entityTypeReference.Type = schema
                .EntityTypes.Cast<ODataTypeDefinition>()
                .Concat(schema.EnumTypes)
                //.Concat(schema.EntitySets)
                //			.Dump()
                .SingleOrDefault(et => et.FullName == name || et.OriginalName == name);
            return entityTypeReference;
        }
    }
}