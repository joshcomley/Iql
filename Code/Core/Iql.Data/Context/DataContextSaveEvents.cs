using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iql.Data.Crud.Operations;
using Iql.Data.Crud.Operations.Results;

namespace Iql.Data.Context
{
    public class DataContextSaveEvents : SaveEvents<SaveChangesOperation, SaveChangesResult>
    {
        private static List<SaveChangesOperation> _active = new List<SaveChangesOperation>();

        public DataContextSaveEvents(ISaveEvents<SaveChangesOperation, SaveChangesResult> global = null) : base(global)
        {
        }

        public static int ActiveOperationsCount => _active.Count;

        public override async Task EmitSavingStartedAsync(Func<SaveChangesOperation> ev)
        {
            _active.Add(ev());
            await base.EmitSavingStartedAsync(ev);
        }

        public override async Task EmitSavingCompletedAsync(Func<SaveChangesResult> ev)
        {
            var saveChangesResult = ev();
            saveChangesResult.Operation.SetResult(saveChangesResult);
            _active = _active.Where(_ => _ != saveChangesResult.Operation).ToList();
            await base.EmitSavingCompletedAsync(ev);
        }

        public override async Task EmitSavedSuccessfullyAsync(Func<SaveChangesResult> ev)
        {
            var saveChangesResult = ev();
            saveChangesResult.Operation.SetResult(saveChangesResult);
            await base.EmitSavedSuccessfullyAsync(ev);
        }
    }
}