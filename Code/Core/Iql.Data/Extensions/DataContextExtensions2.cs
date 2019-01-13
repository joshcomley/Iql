using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Iql.Data.Context;
using Iql.Data.Crud.Operations;
using Iql.Data.Evaluation;
using Iql.Data.Tracking.State;
using Iql.Entities;
using Iql.Entities.Extensions;

namespace Iql.Data.Extensions
{
    public static class DataContextExtensions2
    {
        public static void IsEntityNew4(this IDataContext dataContext)
        {

        }

        public static async Task<bool> TrySetInferredValuesAsync2(
            this IDataContext dataContext,
            object entity)
        {
            var config = dataContext.EntityConfigurationContext.GetEntityByType(entity.GetType());
            return await config.TrySetInferredValuesAsync(null, null, null);
        }
    }
}