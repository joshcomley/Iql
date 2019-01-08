using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Iql.OData.TypeScript.Generator.Models;

namespace Iql.OData.TypeScript.Generator.Definitions
{
    [DebuggerDisplay("{" + nameof(Name) + "}")]
    public class ODataTypeDefinition : ITypeInfo
    {
        public string SetNamespace { get; set; }
        private string _originalName;

        public ODataTypeDefinition()
        {
        }

        public ODataTypeDefinition(string name, string path, bool isImport)
        {
            IsImport = isImport;
            Name = name;
            Namespace = path;
        }

        public bool IsUnknown { get; set; }

        public bool IsImport { get; set; }

        public string Namespace { get; set; }

        public string FullName => Namespace + "." + AbsoluteName;

        public string OriginalName
        {
            get { return _originalName == null ? Name : _originalName; }
            set { _originalName = value; }
        }

        public string Name { get; set; }

        public bool IsFlags { get; set; }

        public string AbsoluteName
        {
            get
            {
                return string.Format("{0}{1}", OriginalName,
                    GenericParameters.Any()
                        ? string.Format("<{0}>", string.Join(", ", GenericParameters.Select(p => p.Name)))
                        : "");
            }
        }

        public List<GenericTypeParameter> GenericParameters { get; set; } = new List<GenericTypeParameter>();

        public ODataGenericTypeDefinition MakeGeneric(params GenericTypeParameter[] genericParameters)
        {
            return new ODataGenericTypeDefinition(this, genericParameters);
        }

        public bool IsEnum { get; set; }

        public string EdmType { get { return Name; }
            set { }
        }
        public bool Nullable { get; set; }
        public string ResolvedType { get; set; }
        public string ConstructorType { get; set; }
    }
}