using System.Collections.Generic;

namespace Iql.Data.Crud.Operations.Results
{
    public class SaveChangesResult : CrudResultBase
    {
        public SaveChangesResult(bool success) : base(success)
        {
        }

        public List<IEntityCrudResult> Results { get; set; } = new List<IEntityCrudResult>();
    }
}