using System;

namespace Iql.Data.Context
{
    public class DataSnapshotChain
    {
        private DataSnapshotChain _next;

        public Guid CreatorId { get; }
        public DataSnapshot Snapshot { get; }
        public DataSnapshotChain Next
        {
            get { return _next; }
            set
            {
                var old = _next;
                _next = value;
                if (_next != null)
                {
                    _next.Previous = this;
                }
                if (old != null)
                {
                    old.Previous = null;
                    old.Invalidate();
                }
            }
        }
        private bool _isInvalid = false;
        public bool IsInvalid => _isInvalid;
        public void Invalidate()
        {
            this._isInvalid = true;
        }
        public DataSnapshotChain Previous { get; set; }
        public DataSnapshotChain Root
        {
            get
            {
                var previous = Previous;
                var earliest = this;
                while (previous != null)
                {
                    earliest = previous;
                    previous = earliest.Previous;
                }
                return earliest;
            }
        }

        public DataSnapshotChain Earliest => Root.Next;

        public DataSnapshotChain Latest
        {
            get
            {
                var next = Next;
                var latest = this;
                while (next != null)
                {
                    latest = next;
                    next = latest.Next;
                }
                return latest;
            }
        }
        public DataSnapshotChain(Guid creatorId, DataSnapshot snapshot = null)
        {
            CreatorId = creatorId;
            Snapshot = snapshot;
        }
        public DataSnapshotChain Find(DataSnapshot snapshot)
        {
            var root = Root;
            var next = root.Next;
            while (next != null)
            {
                if (next.Snapshot == snapshot)
                {
                    return next;
                }
            }
            return null;
        }
        public DataSnapshotChain FindById(Guid id)
        {
            var root = Root;
            var next = root.Next;
            while (next != null)
            {
                if (next.Snapshot.Id == id)
                {
                    return next;
                }
            }
            return null;
        }
    }
}
