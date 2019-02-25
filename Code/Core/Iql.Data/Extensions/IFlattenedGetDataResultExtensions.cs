using System.Collections.Generic;
using System.Reflection;
using Iql.Data.Context;
using Iql.Data.Crud.Operations.Results;
using Iql.Data.Lists;
using Iql.Extensions;

namespace Iql.Data.Extensions
{
    // ReSharper disable once InconsistentNaming
    public static class IFlattenedGetDataResultExtensions
    {
        public static bool IsSuccessful(this IFlattenedGetDataResult result)
        {
            return result.Success && result.Data != null;
        }

        public static IDbList ToDbList(this IFlattenedGetDataResult response)
        {
            return (IDbList) FlattenedGetDataResultExtensions.ToDbListMethod.InvokeGeneric(null,
                new object[] {response}, response.EntityType);
        }
    }

    public static class FlattenedGetDataResultExtensions
    {
        public static MethodInfo ToDbListMethod { get; private set; }

        static FlattenedGetDataResultExtensions()
        {
            ToDbListMethod = typeof(FlattenedGetDataResultExtensions).GetMethod(nameof(ToDbList), BindingFlags.Static | BindingFlags.Public);
        }

        public static DbList<T> ToDbList<T>(this FlattenedGetDataResult<T> response)
            where T : class
        {
            var dbList = new DbList<T>();
            dbList.SourceQueryable = (DbQueryable<T>)response.Queryable;
            dbList.AddRange(response.Root);
            dbList.Success = response.IsSuccessful();
            return dbList;
        }

        public static List<TEntity> ResolveRoot<TEntity>(this FlattenedGetDataResult<TEntity> response) where TEntity : class
        {
            if (response.Data.ContainsKey(response.EntityType))
            {
                return (List<TEntity>)response.Data[response.EntityType];
            }

            return null;
        }
    }
}