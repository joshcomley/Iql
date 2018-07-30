using System.Collections.Generic;
using Iql.Entities.Dates;

namespace Iql.Entities
{
    public interface IMediaKey
    {
        IFile File { get; }
        string Separator { get; set; }
        IList<IMediaKeyGroup> Groups { get; }
        string[][] Evaluate(object entity);
        string EvaluateToString(object entity);
        IMediaKey Clear();
    }
}