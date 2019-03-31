using System;
using System.Collections;
using System.Threading.Tasks;
using Iql.Entities;

namespace Iql.Data.Evaluation
{
    public interface IIqlDataEvaluator
    {
        Task<object> GetEntityByKeyAsync(
            IEntityConfiguration entityConfiguration,
            CompositeKey key,
            string[] expandPaths);

        bool IsEntityNew(
            IEntityConfiguration entityConfiguration,
            object entity);
    }
}