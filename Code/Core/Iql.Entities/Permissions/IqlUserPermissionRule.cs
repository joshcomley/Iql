using System.Collections.Generic;
using Iql.Entities.Functions;

namespace Iql.Entities
{
    public class IqlUserPermissionRule
    {
        public string Key { get; set; }
        public IqlLambdaExpression Rule { get; set; }
        public bool AcceptsEntity { get; set; }

        public IqlUserPermissionRule(string key = null, IqlLambdaExpression rule = null, bool acceptsEntity = false)
        {
            Key = key;
            Rule = rule;
            AcceptsEntity = acceptsEntity;
        }

        public IqlUserPermissionRule()
        {
            
        }
    }
}