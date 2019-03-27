using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Iql.Data;
using Iql.Data.Context;
using Iql.Data.Crud.Operations.Results;
using Iql.Data.DataStores;
using Iql.Data.Events;
using Iql.Data.Http;
using Iql.Data.Lists;
using Iql.Data.Methods;
using Iql.Entities;
using Iql.Entities.Events;
using Iql.Entities.Functions;
using Iql.Entities.InferredValues;
using Iql.Entities.Metadata;
using Iql.Entities.PropertyChangers;
using Iql.Entities.Relationships;
using Iql.Entities.Rules.Display;
using Iql.Entities.SpecialTypes;
using Iql.Entities.Validation.Validation;
using Iql.Events;
using Iql.OData.Json;
using Iql.OData.Methods;
using Iql.Parsing;
using Newtonsoft.Json.Linq;

namespace Iql.OData.TypeScript.Generator.DataContext
{
    public static class ImportHelper
    {
        public static Type[] PotentialImports = new[]
        {
            typeof(CompositeKey),
            typeof(ODataConfiguration),
            typeof(IHttpProvider),
            typeof(Data.Context.DataContext),
            typeof(IEntity),
            typeof(EntityValidationResult<>),
            typeof(RelatedList<,>),
            typeof(PropertyValidationResult<>),
            typeof(GetDataResult<object>),
            typeof(UsersDefinition),
            typeof(CustomReportsDefinition),
            typeof(UserSettingsDefinition),
            typeof(EntityConfiguration<>),
            typeof(EntityConfigurationBuilder),
            typeof(IDataStore),
            typeof(ODataDataStore),
            typeof(ODataResult<object>),
            typeof(EvaluateContext),
            typeof(Type),
            typeof(List<object>),
            typeof(DbSet<object, object>),
            typeof(Task),
            typeof(JObject),
            typeof(EventEmitter<>),
            typeof(PropertyChangeEvent<>),
            typeof(ODataMethodScope),
            typeof(ODataMethodType),
            typeof(MethodResult),
            typeof(DataMethodResult<>),
            typeof(ODataParameter),
            typeof(ExistsChangeEvent),
            typeof(ObservableCollection<>),
            typeof(ODataDataMethodRequest<>),
            typeof(ODataMethodRequest),
            typeof(IPropertyChangeEvent),
            typeof(PropertyKind),
            typeof(Expression),
            typeof(Func<>),
            typeof(Enum),
            typeof(IDataContext),
            typeof(IqlType),
            typeof(DisplayRuleKind),
            typeof(DisplayRuleAppliesToKind),
            typeof(EntityManageKind),
            typeof(HelpTextKind),
            typeof(IInferredValueConfiguration),
            typeof(InferredValueConfiguration),
            typeof(HelpText),
            typeof(IqlPointExpression),
            typeof(IqlMultiPointExpression),
            typeof(IqlPolygonExpression),
            typeof(IqlMultiPolygonExpression),
            typeof(ValueMapping),
            typeof(RelationshipMapping),
            typeof(IqlLineExpression),
            typeof(IqlMultiLineExpression),
            typeof(PrimitivePropertyChanger),
            typeof(PointPropertyChanger),
            typeof(PolygonPropertyChanger),
            typeof(EntityTypeService),
            typeof(IqlMethod),
            typeof(IqlUserPermissionRule),

        };

        private static Dictionary<string, Type> _importLookup = null;

        private static Dictionary<string, Type> ImportLookup
        {
            get { return _importLookup = _importLookup ?? PotentialImports.ToDictionary(SimpleCSharpTypeName); }
        }

        public static Type GetBySimpleName(string name)
        {
            return ImportLookup[name];
        }
        private static string[] _importNames = null;
        public static string[] ImportNames
        {
            get
            {
                if (_importNames == null)
                {
                    _importNames = ImportLookup.Keys.ToArray();
                }
                return _importNames;
            }
        }

        private static string SimpleCSharpTypeName(Type i)
        {
            var name = i.Name;
            var import = name;
            var index = import.IndexOf("`");
            if (index != -1)
            {
                import = import.Substring(0, index);
            }
            return import;
        }

        public static IEnumerable<Type> ResolveImportsWithinCSharpFile(this string contents)
        {
            var imports = new List<Type>();
            foreach (var classToImport in ImportNames)
            {
                if (Regex.IsMatch(contents, $"\\b{classToImport}\\b"))
                {
                    imports.Add(GetBySimpleName(classToImport));
                }
            }
            return imports.Distinct();
        }
    }
}