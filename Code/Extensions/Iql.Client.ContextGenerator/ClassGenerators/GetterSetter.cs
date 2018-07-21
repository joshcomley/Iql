using System;

namespace Iql.OData.TypeScript.Generator.ClassGenerators
{
    public class GetterSetter
    {
        public string NewValueName { get; set; }
        public string BackingFieldName { get; set; }
        public Action BeforeSet { get; set; }
        public Action AfterSet { get; set; }
        public Action BeforeGet { get; set; }
        public Action AfterGet { get; set; }
    }
}