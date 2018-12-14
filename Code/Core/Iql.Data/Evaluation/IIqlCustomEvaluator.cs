using System;
using System.Collections;
using System.Threading.Tasks;
using Iql.Entities;

namespace Iql.Data.Evaluation
{
    public interface IIqlCustomEvaluator
    {
        Task<object> GetEntityByKeyAsync(
            IEntityConfiguration entityConfiguration,
            CompositeKey key);

        bool IsEntityNew(
            IEntityConfiguration entityConfiguration,
            object entity);
    }
}