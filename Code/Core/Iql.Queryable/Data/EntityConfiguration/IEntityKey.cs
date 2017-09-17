using System.Collections.Generic;

namespace Iql.Queryable.Data.EntityConfiguration
{
    public interface IEntityKey
    {
        List<IqlPropertyExpression> Properties { get; set; }
    }
}