using System.Collections.Generic;

namespace Iql
{
    public interface IPointsExpression
    {
        List<IqlPointExpression> Points { get; set; }
    }
}