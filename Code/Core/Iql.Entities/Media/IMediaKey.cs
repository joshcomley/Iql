using System.Collections.Generic;
using Iql.Entities.PropertyGroups.Files;

namespace Iql.Entities
{
    public interface IMediaKey
    {
        IFileUrlBase File { get; }
        string Separator { get; set; }
        IList<IMediaKeyGroup> Groups { get; }
        string[][] Evaluate(object entity);
        string EvaluateToString(object entity);
        IMediaKey Clear();
    }
}