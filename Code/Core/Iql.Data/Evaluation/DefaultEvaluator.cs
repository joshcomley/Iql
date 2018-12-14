using System.Threading.Tasks;
using Iql.Data.Context;
using Iql.Data.Extensions;
using Iql.Entities;

namespace Iql.Data.Evaluation
{
    public class DefaultEvaluator : IIqlCustomEvaluator
    {
        public IDataContext DataContext { get; }

        public DefaultEvaluator(IDataContext dataContext)
        {
            DataContext = dataContext;
        }

        public Task<object> GetEntityByKeyAsync(IEntityConfiguration entityConfiguration, CompositeKey key)
        {
            return DataContext.GetDbSetByEntityType(entityConfiguration.Type)
                .GetWithKeyAsync(key);
        }

        public bool IsEntityNew(IEntityConfiguration entityConfiguration, object entity)
        {
            return DataContext.IsEntityNew(entity, entityConfiguration.Type) != false;
        }
    }
}