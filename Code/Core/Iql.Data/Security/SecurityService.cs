using System;
using System.Threading.Tasks;
using Iql.Data.Context;
using Iql.Data.Evaluation;
using Iql.Entities;

namespace Iql.Data.Security
{
    public class SecurityService<TUser>
    {
        public TUser CurrentUser { get; }
        public Type UserType { get; }

        public SecurityService(TUser currentUser)
        {
            CurrentUser = currentUser;
            UserType = typeof(TUser);
        }

        public async Task<SecurityResult> GetPermissionsAsync<T>(
            DataContext dataContext,
            T entity,
            IUserPermission property)
        {
            var permissions = await new PermissionsEvaluationSession().GetUserPermissionAsync(
                property.EntityConfiguration.Builder.PermissionManager,
                property,
                dataContext,
                CurrentUser,
                UserType,
                entity,
                typeof(T),
                dataContext,
                dataContext.EntityConfigurationContext);
            return new SecurityResult(permissions);
        }
    }
}