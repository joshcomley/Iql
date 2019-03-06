namespace Iql.OData.TypeScript.Generator.DataContext
{
    public class GeneratedContexts
    {
        public GeneratedContext TypeScriptContext { get; }
        public GeneratedContext CSharpContext { get; }

        public GeneratedContexts(
            GeneratedContext cSharpContext,
            GeneratedContext typeScriptContext
        )
        {
            CSharpContext = cSharpContext;
            TypeScriptContext = typeScriptContext;
        }
    }
}