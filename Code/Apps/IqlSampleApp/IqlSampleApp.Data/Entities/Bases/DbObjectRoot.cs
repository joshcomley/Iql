using System;
using Brandless.Data.Contracts;

namespace Brandless.Data.Entities
{
    public interface IRevisionable
    {
        string RevisionKey { get; set; }
    }

    public class DbObjectRoot<TUser> : DbObjectPersistent, ICreatedDate, ICreatedBy<TUser>, IRevisionable
    {
        public TUser CreatedByUser { get; set; }
        public string CreatedByUserId { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public string RevisionKey { get; set; }
    }
}