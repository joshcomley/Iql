using Microsoft.EntityFrameworkCore;

namespace Iql.Server.OData.Net.Media
{
    public interface IDeletedMedia
    {
        DbSet<DeletedMedia> DeletedMedia { get; set; }
    }
}