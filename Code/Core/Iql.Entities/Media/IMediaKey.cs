using System.Collections.Generic;

namespace Iql.Entities
{
    public interface IMediaKey
    {
        string Separator { get; set; }
        IProperty Property { get; }
        IList<IMediaKeyGroup> Groups { get; }
        string[][] Evaluate(object entity);
        string EvaluateToString(object entity);
        IMediaKey Clear();
    }
}