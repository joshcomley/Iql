using System;

namespace Iql.OData.TypeScript.Generator
{
    public class GeneratorSettings
    {
        public string Namespace { get; set; }
        public Func<string, string> NameMapper { get; set; }
        public bool GenerateEntitySets { get; set; } = true;
        public bool GenerateCountProperties { get; set; } = true;
        public bool GenerateEntities { get; set; } = true;
        public bool GenerateDataContext { get; set; } = true;
        public bool ConfigureOData { get; set; } = true;

        public GeneratorSettings(string ns, Func<string, string> nameMapper)
        {
            Namespace = ns;
            NameMapper = nameMapper;
        }
    }
}