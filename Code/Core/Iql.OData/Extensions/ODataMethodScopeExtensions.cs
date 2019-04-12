using Iql.Entities.Functions;

namespace Iql.OData.Extensions
{
    public static class ODataMethodScopeExtensions
    {
        public static IqlMethodScopeKind ToIqlMethodScope(this ODataMethodScopeKind scope)
        {
            switch (scope)
            {
                case ODataMethodScopeKind.Collection:
                    return IqlMethodScopeKind.EntitySet;
                case ODataMethodScopeKind.Entity:
                    return IqlMethodScopeKind.Entity;
            }

            return IqlMethodScopeKind.Global;
        }
        public static ODataMethodScopeKind ToODataMethodScope(this IqlMethodScopeKind scope)
        {
            switch (scope)
            {
                case IqlMethodScopeKind.EntitySet:
                    return ODataMethodScopeKind.Collection;
                case IqlMethodScopeKind.Entity:
                    return ODataMethodScopeKind.Entity;
            }

            return ODataMethodScopeKind.Global;
        }
    }
}