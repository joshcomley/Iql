using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iql.Data.Crud.Operations;
using Iql.Data.Crud.Operations.Results;

namespace Iql.Data.Context
{
    public class DataContextSaveEvents : OperationEvents<SaveChangesOperation, SaveChangesResult>
    {
        private static bool _activeDelayedInitialized;
        private static List<SaveChangesOperation> _activeDelayed;
        private static List<SaveChangesOperation> _active { get { if(!_activeDelayedInitialized) { _activeDelayedInitialized = true; _activeDelayed = new List<SaveChangesOperation>(); } return _activeDelayed; } set { _activeDelayedInitialized = true; _activeDelayed = value; } }

        public DataContextSaveEvents(IOperationEvents<SaveChangesOperation, SaveChangesResult> global = null) : base(global)
        {
        }

        public static int ActiveOperationsCount => _active.Count;

        public override async Task EmitStartedAsync(Func<SaveChangesOperation> ev)
        {
            _active.Add(ev());
            await base.EmitStartedAsync(ev);
        }

        public override async Task EmitCompletedAsync(Func<SaveChangesResult> ev)
        {
            var saveChangesResult = ev();
            saveChangesResult.Operation.SetResult(saveChangesResult);
            _active = _active.Where(_ => _ != saveChangesResult.Operation).ToList();
            await base.EmitCompletedAsync(ev);
        }

        public override async Task EmitSuccessAsync(Func<SaveChangesResult> ev)
        {
            var saveChangesResult = ev();
            saveChangesResult.Operation.SetResult(saveChangesResult);
            await base.EmitSuccessAsync(ev);
        }
    }
}