using System;

namespace Iql.Entities
{
    public interface IEntityKey
    {
        Type KeyType { get; set; }
        Type Type { get; set; }
        bool HasRelationshipKeys { get; }
        bool CanWrite { get; set; }
        IProperty[] Properties { get; }
        void AddProperty(IProperty property);
        bool IsPivot();
        void SetReadKind(PropertyReadKind readKind);
        void SetEditKind(PropertyEditKind editKind);
    }
}