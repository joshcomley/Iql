using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Iql.Entities;
using Iql.Entities.Permissions;

namespace Iql.Data.Evaluation
{
    public class PermissionResult
    {
        public PermissionResultPath Path { get; }
        public IUserPermission Container { get; }
        public IqlUserPermission OriginalResult { get; }
        public IqlUserPermission PathResult { get; }
        public IqlUserPermissionRule Rule { get; }

        public PermissionResult(PermissionResultPath path, IUserPermission container, IqlUserPermission originalResult, IqlUserPermission pathResult, IqlUserPermissionRule rule)
        {
            Path = path;
            Container = container;
            OriginalResult = originalResult;
            PathResult = pathResult;
            Rule = rule;
        }
    }
    public class PermissionResultPath
    {
        private IqlUserPermission _result = IqlUserPermission.Unset;
        private bool _pathDelayedInitialized;
        private List<PermissionResult> _pathDelayed;
        private List<PermissionResult> _path { get { if(!_pathDelayedInitialized) { _pathDelayedInitialized = true; _pathDelayed = new List<PermissionResult>(); } return _pathDelayed; } set { _pathDelayedInitialized = true; _pathDelayed = value; } }
        public UserPermissionsManager Manager { get; }
        public object User { get; }
        public Type UserType { get; }
        public object Entity { get; }
        public Type EntityType { get; }
        public IqlUserPermission Result => _result;
        public List<PermissionResult> Path => _path;
        public IUserPermission Container { get; }

        public PermissionResultPath(UserPermissionsManager manager, IUserPermission container, object user, Type userType, object entity, Type entityType)
        {
            Manager = manager;
            User = user;
            UserType = userType;
            Entity = entity;
            EntityType = entityType;
            Container = container;
        }

        public PermissionResultPath Add(IUserPermission container, IqlUserPermissionRule rule, IqlUserPermission originalResult, IqlUserPermission result)
        {
            _path.Add(new PermissionResult(this, container, originalResult, result, rule));
            var filtered = _path.Where(_ => _.PathResult != IqlUserPermission.Unset).ToArray();
            if (filtered.Length == 0)
            {
                _result = IqlUserPermission.Unset;
            }
            else
            {
                _result = filtered.Last().PathResult;
            }
            return this;
        }
    }
}
