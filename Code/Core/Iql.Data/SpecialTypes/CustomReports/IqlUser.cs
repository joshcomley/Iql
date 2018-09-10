using System;
using Iql.Data;

namespace Iql.Entities.SpecialTypes
{
    public class IqlUser : EntityBase
    {
        private string _id;
        public string Id
        {
            get => _id;
            set => SetPropertyValue<IqlUser, string>(nameof(Id), _id, value, () => _id = value);
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetPropertyValue<IqlUser, string>(nameof(Name), _name, value, () => _name = value);
        }
    }
}