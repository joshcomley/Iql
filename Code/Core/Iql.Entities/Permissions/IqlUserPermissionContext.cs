using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Iql.Entities.Permissions
{
    public class IqlUserPermissionContext<TUser>
        where TUser : class
    {
        public TUser User { get; set; }

        public bool QueryAny<T>(Expression<Func<T, bool>> expression)
            where T : class
        {
            return false;
        }

        public bool QueryAll<T>(Expression<Func<T, bool>> expression)
            where T : class
        {
            return false;
        }

        public long QueryCount<T>(Expression<Func<T, bool>> expression)
            where T : class
        {
            return -1;
        }

        public IqlUserPermissionContext(TUser user)
        {
            User = user;
        }
    }
}
