using Iql.Entities;

namespace Iql.Data.Events
{
    public interface IEntityPropertyEvent : IEntityEventBase
    {
        IPropertyContainer Property { get; }
    }
}