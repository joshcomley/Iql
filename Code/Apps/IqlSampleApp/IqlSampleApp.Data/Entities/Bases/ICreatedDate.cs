using System;

namespace Brandless.Data.Entities
{
    public interface ICreatedDate
    {
        DateTimeOffset CreatedDate { get; set; }
    }
}