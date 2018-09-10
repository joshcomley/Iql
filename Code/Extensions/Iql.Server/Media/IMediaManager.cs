﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Iql.Entities;
using Iql.Entities.PropertyGroups.Files;

namespace Iql.Server.Media
{
    public interface IMediaManager
    {
        Task DeleteAssociatedMediaAsync<T>(T entity, IEntityConfigurationBuilder configuration) where T : class;
        IEnumerable<File<T>> GetMediaProperties<T>(IEntityConfigurationBuilder configuration) where T : class;
        Task<string> GetMediaUriAsync<T>(T entity, IFileUrl<T> file, MediaAccessKind accessKind, TimeSpan? lifetime = null) where T : class;
        Task DeleteAsync<T>(T entity, IFileUrl<T> file) where T : class;
    }
}