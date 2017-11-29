﻿using System.Threading.Tasks;
using Iql.Queryable;
using Iql.Queryable.Data.Crud.Operations.Results;

namespace Iql.OData.Data
{
    public interface IODataHttpProvider
    {
        Task<GetDataResult<TResult>> PostOnEntityInstance<TEntity, TResult>(object payload) where TResult : class where TEntity : class;
        Task<GetDataResult<TResult>> GetOnEntityInstance<TEntity, TResult>(object payload) where TResult : class where TEntity : class;
        Task<GetDataResult<TResult>> PostOnEntitySet<TEntity, TResult>(object payload) where TResult : class where TEntity : class;
        Task<GetDataResult<TResult>> GetOnEntitySet<TEntity, TResult>(object payload) where TResult : class where TEntity : class;
    }
}