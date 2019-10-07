using System;

namespace Iql.Data.Context
{
    public class DataSnapshot
    {
        public Guid CreatorId { get; }
        public DateTime DateTaken { get; }
        public Guid Id { get; }
        public string Snapshot { get; }

        public DataSnapshot(Guid creatorId, string snapshot)
        {
            Id = Guid.NewGuid();
            Snapshot = snapshot;
            DateTaken = DateTime.Now;
            CreatorId = creatorId;
        }
    }
}
