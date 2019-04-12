using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Iql.Entities.Functions
{
    public class IqlMethod : MetadataBase
    {
        public bool SupportsOffline { get; set; }
        public string DataStoreRequired { get; set; }
        public Func<object, object[], Task<object>> RunAsync { get; set; }
        public string NameSpace { get; set; }
        public bool IsPublic { get; set; }
        public List<IqlMethodParameter> Parameters { get; set; }
        public IqlMethodScopeKind ScopeKind { get; set; }

        public IqlMethod(
            IqlMethodScopeKind scopeKind = IqlMethodScopeKind.Global,
            string name = null, 
            IEnumerable<IqlMethodParameter> parameters = null,
            Func<object, object[], Task<object>> runAsync = null,
            IEntityConfiguration entityConfiguration = null,
            string nameSpace = null,
            bool supportsOffline = false,
            string dataStoreRequired = null)
        {
            Name = name;
            Parameters = parameters?.ToList() ?? new List<IqlMethodParameter>();
            ScopeKind = scopeKind;
            RunAsync = runAsync;
            EntityConfiguration = entityConfiguration;
            NameSpace = nameSpace;
            SupportsOffline = supportsOffline;
            DataStoreRequired = dataStoreRequired;
        }

        public IqlMethod()
        {
            

        }
    }
}