using System.Collections.Generic;
using System.Threading.Tasks;
using Brandless.Data.EntityFramework.Crud;
using Iql.Data.Evaluation;
using Iql.Entities;

namespace Iql.Server.OData.Net
{
    public class IqlServerEvaluator : IIqlCustomEvaluator
    {
        private readonly bool _isEntityNew;
        public CrudManager CrudManager { get; set; }

        public IqlServerEvaluator(CrudManager crudManager, bool isEntityNew)
        {
            _isEntityNew = isEntityNew;
            CrudManager = crudManager;
        }

        public Task<object> GetEntityByKeyAsync(IEntityConfiguration entityConfiguration, CompositeKey key)
        {
            var dic = new List<KeyValuePair<string, object>>();
            foreach (var constraint in key.Keys)
            {
                dic.Add(new KeyValuePair<string, object>(
                    constraint.Name,
                    constraint.Value));
            }

            try
            {
                var entity = CrudManager.FindEntity(dic, entityConfiguration.Type);
                return Task.FromResult(entity);
            }
            catch
            {

            }

            return Task.FromResult<object>(null);
        }

        public bool IsEntityNew(IEntityConfiguration entityConfiguration, object entity)
        {
            return _isEntityNew;
        }
    }
}