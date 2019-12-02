using System.Collections.Generic;

namespace Iql.Data.Crud.Operations.Results
{
    public enum SaveChangeKind
    {
        Success,
        Fail,
        NoAction
    }
    public class SaveChangesResult : CrudResultBase
    {
        public SaveChangesOperation Operation { get; }
        private SaveChangeKind _kind;

        public SaveChangeKind Kind => _kind;

        public override bool Success
        {
            get => Kind != SaveChangeKind.Fail;
            set => _kind = value ? SaveChangeKind.Success : SaveChangeKind.Fail;
        }

        public SaveChangesResult(SaveChangesOperation operation, SaveChangeKind kind) : base(operation, kind != SaveChangeKind.Fail)
        {
            Operation = operation;
            _kind = kind;
        }
        private bool _resultsInitialized;
        private List<IEntityCrudResult> _results;

        public List<IEntityCrudResult> Results { get { if(!_resultsInitialized) { _resultsInitialized = true; _results = new List<IEntityCrudResult>(); } return _results; } set { _resultsInitialized = true; _results = value; } }
    }
}