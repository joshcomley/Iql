namespace Iql.OData.TypeScript.Generator.Definitions
{
    public class GeneratorTypeDefinition
    {
        private string _originalName;

        public string OriginalName
        {
            get => _originalName ?? Name;
            set => _originalName = value;
        }

        public string Name { get; set; }

        public bool IsCollection { get; set; }

        public string ElementName { get; set; }
    }
}