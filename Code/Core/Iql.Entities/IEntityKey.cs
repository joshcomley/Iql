using System;

namespace Iql.Entities
{
    public interface IEntityKey
    {
        bool HasRelationshipKeys { get; }
        Type KeyType { get; set; }
        Type Type { get; set; }
        IProperty[] Properties { get; }
        void AddProperty(IProperty property);
        bool IsPivot();
        void SetReadOnly(bool readOnly = true);
    }
}