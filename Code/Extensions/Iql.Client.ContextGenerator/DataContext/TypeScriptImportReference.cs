namespace Iql.OData.TypeScript.Generator.DataContext
{
    internal class TypeScriptImportReference
    {
        public TypeScriptImportReference(string relativePath, string import)
        {
            RelativePath = relativePath;
            Import = import;
        }

        public string RelativePath { get; set; }
        public string Import { get; set; }
    }
}