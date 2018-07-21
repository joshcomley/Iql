using System.Collections.Generic;
using System.Linq;
using Iql.OData.TypeScript.Generator.Models;

namespace Iql.OData.TypeScript.Generator.Definitions
{
    public class ODataGenericTypeDefinition : ODataTypeDefinition
    {
        public ODataGenericTypeDefinition(ODataTypeDefinition baseDefinition,
            IEnumerable<GenericTypeParameter> genericParameters)
        {
            BaseDefinition = baseDefinition;
            Namespace = baseDefinition.Namespace;
            Name = baseDefinition.Name;
            GenericParameters = genericParameters.ToList();
        }

        public ODataTypeDefinition BaseDefinition { get; set; }
    }
}