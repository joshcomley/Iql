using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Iql.Entities.Permissions;

namespace Iql.Entities.Functions
{
    public class IqlMethod : IUserPermission
    {
        private UserPermissionsManager _permissions = null;
        public List<IqlUserPermissionRule> PermissionRules { get; } = new List<IqlUserPermissionRule>();
        public UserPermissionsManager Permissions => _permissions = _permissions ?? new UserPermissionsManager(this, EntityConfiguration);
        public string Name { get; set; }
        public bool SupportsOffline { get; set; }
        public string DataStoreRequired { get; set; }
        public Func<object, object[], Task<object>> RunAsync { get; set; }
        public IEntityConfiguration EntityConfiguration { get; }
        public string NameSpace { get; set; }
        public List<IqlMethodParameter> Parameters { get; set; }

        public IqlMethod(
            string name, 
            IEnumerable<IqlMethodParameter> parameters,
            Func<object, object[], Task<object>> runAsync,
            IEntityConfiguration entityConfiguration,
            string nameSpace = null,
            bool supportsOffline = false,
            string dataStoreRequired = null)
        {
            Name = name;
            Parameters = parameters?.ToList() ?? new List<IqlMethodParameter>();
            RunAsync = runAsync;
            EntityConfiguration = entityConfiguration;
            NameSpace = nameSpace;
            SupportsOffline = supportsOffline;
            DataStoreRequired = dataStoreRequired;
        }
    }
}