using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iql.Data.Types;
using Iql.Extensions;

namespace Iql.Entities.Functions
{
    public class IqlMethod : MetadataBase
    {
        public int Ordering { get; set; }
        public bool SupportsOffline { get; set; }
        public string DataStoreRequired { get; set; }
        public Func<object, object[], Task<object>> RunAsync { get; set; }
        public string NameSpace { get; set; }
        public bool IsPublic { get; set; }
        public List<IqlMethodParameter> Parameters { get; set; }
        public IqlMethodScopeKind ScopeKind { get; set; }
        public IqlMethodKind Kind { get; set; }
        public Type ReturnType { get; set; }
        private string _returnTypeName;
        public string ReturnTypeName
        {
            get => ReturnType == null ? _returnTypeName : TypeName.Parse(ReturnType.GetFullName()).FullName;
            set => _returnTypeName = value;
        }
        public bool HasReturnType => ReturnType != null;

        public IEnumerable<IqlMethodParameter> NonBindingParameters => Parameters == null
            ? new IqlMethodParameter[] { }
            : Parameters.Where(_ => _.IsBindingParameter == false);
        public IEnumerable<IqlMethodParameter> BindingParameters => Parameters == null
            ? new IqlMethodParameter[] { }
            : Parameters.Where(_ => _.IsBindingParameter == true);

        public IqlMethod(
            IqlMethodKind kind = IqlMethodKind.Action,
            IqlMethodScopeKind scopeKind = IqlMethodScopeKind.Global,
            string name = null, 
            IEnumerable<IqlMethodParameter> parameters = null,
            Type returnType = null,
            Func<object, object[], Task<object>> runAsync = null,
            IEntityConfiguration entityConfiguration = null,
            string nameSpace = null,
            bool supportsOffline = false,
            string dataStoreRequired = null)
        {
            Kind = kind;
            Name = name;
            Parameters = parameters?.ToList() ?? new List<IqlMethodParameter>();
            ScopeKind = scopeKind;
            ReturnType = returnType;
            RunAsync = runAsync;
            EntityConfiguration = entityConfiguration;
            NameSpace = nameSpace;
            SupportsOffline = supportsOffline;
            DataStoreRequired = dataStoreRequired;
        }

        public IqlMethod()
        {
            

        }

        public IqlMethod Configure(Action<IqlMethod> action)
        {
            if (action != null)
            {
                action(this);
            }

            return this;
        }
        public override IUserPermission ParentPermissions => EntityConfiguration;
    }
}