using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Iql.Entities.Permissions
{
    public class IqlUserPermissionContext<TUser>
        where TUser : class
    {
        public TUser User { get; set; }

        public bool Query<T>(Expression<Func<List<T>, bool>> expression)
            where T : class
        {
            return false;
        }

        public IqlUserPermissionContext(TUser user)
        {
            User = user;
        }
    }
}
