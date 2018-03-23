using System.Collections.Generic;

namespace Iql.Queryable.Data.EntityConfiguration
{
    public interface IMetadata
    {
        string Description { get; set; }
        string FriendlyName { get; set; }
        List<string> Hints { get; set; }
        string Name { get; set; }
        string Title { get; set; }
        string ResolveFriendlyName();
        string ResolveName();
    }
}