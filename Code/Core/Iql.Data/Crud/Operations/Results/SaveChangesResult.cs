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
        private SaveChangeKind _kind;

        public SaveChangeKind Kind => _kind;

        public override bool Success
        {
            get => Kind != SaveChangeKind.Fail;
            set => _kind = value ? SaveChangeKind.Success : SaveChangeKind.Fail;
        }
        public SaveChangesResult(SaveChangeKind kind) : base(kind != SaveChangeKind.Fail)
        {
            _kind = kind;
        }

        public List<IEntityCrudResult> Results { get; set; } = new List<IEntityCrudResult>();
    }
}