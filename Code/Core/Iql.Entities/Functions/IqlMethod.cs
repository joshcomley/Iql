using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Iql.Entities.Functions
{
    public class IqlMethod : IUserPermission
    {
        private UserPermissionsCollection _permissions;
        public UserPermissionsCollection Permissions => _permissions = _permissions ?? new UserPermissionsCollection(EntityConfiguration.Builder);
        public string Name { get; set; }
        public bool SupportsOffline { get; set; }
        public string DataStoreRequired { get; set; }
        public Func<object, object[], Task<object>> RunAsync { get; set; }
        public IEntityConfiguration EntityConfiguration { get; }
        public string NameSpace { get; set; }
        public List<IqlMethodParameter> Parameters { get; set; }

        public IqlMethod(
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