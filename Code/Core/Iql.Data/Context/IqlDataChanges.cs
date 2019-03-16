using System.Linq;
using Iql.Data.Crud.Operations;
using Iql.Data.Crud.Operations.Queued;
using Iql.Entities;

namespace Iql.Data.Context
{
    public class IqlDataChanges
    {
        private IQueuedAddEntityOperation[] _additions;
        private IQueuedUpdateEntityOperation[] _updates;
        private IQueuedDeleteEntityOperation[] _deletions;

        public int Length => AllChanges.Length;
        public bool HasChanges => AllChanges.Length > 0;

        public IqlDataChanges(IQueuedOperation[] allChanges)
        {
            AllChanges = allChanges ?? new IQueuedOperation[] { };
        }

        public IQueuedOperation[] AllChanges { get; }

        public IQueuedAddEntityOperation[] Additions
        {
            get
            {
                return _additions = _additions ?? AllChanges.Where(op => op.Kind == QueuedOperationKind.Add)
                    .Select(_ => _ as IQueuedAddEntityOperation)
                    .ToArray();
            }
        }

        public IQueuedUpdateEntityOperation[] Updates
        {
            get
            {
                return _updates = _updates ?? AllChanges.Where(op => op.Kind == QueuedOperationKind.Update)
                    .Select(_ => _ as IQueuedUpdateEntityOperation)
                    .ToArray();
            }
        }

        public IQueuedDeleteEntityOperation[] Deletions
        {
            get
            {
                return _deletions = _deletions ?? AllChanges.Where(op => op.Kind == QueuedOperationKind.Delete)
                    .Select(_ => _ as IQueuedDeleteEntityOperation)
                    .ToArray();
            }
        }
    }
}