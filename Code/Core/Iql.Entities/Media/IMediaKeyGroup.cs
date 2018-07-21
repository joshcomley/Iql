using System.Collections.Generic;

namespace Iql.Entities
{
    public interface IMediaKeyGroup
    {
        IMediaKey MediaKey { get; }
        string Separator { get; set; }
        List<IMediaKeyPart> Parts { get; set; }
        string[] Evaluate(object entity);
        string EvaluateToString(object entity);
    }
}