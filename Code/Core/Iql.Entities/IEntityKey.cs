using System;

namespace Iql.Entities
{
    public interface IEntityKey
    {
        Type KeyType { get; set; }
        Type Type { get; set; }
        bool HasRelationshipKeys { get; }
        IProperty[] Properties { get; }
        void AddProperty(IProperty property);
        bool IsPivot();
        void SetReadOnly(bool readOnly = true);
    }
}