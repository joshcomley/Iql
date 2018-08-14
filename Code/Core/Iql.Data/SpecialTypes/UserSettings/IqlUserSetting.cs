using System;
using Iql.Data;

namespace Iql.Entities.SpecialTypes
{
    public class IqlUserSetting : EntityBase
    {
        private Guid _id;
        public Guid Id
        {
            get => _id;
            set => SetPropertyValue<IqlUserSetting, Guid>(nameof(Id), _id, value, () => _id = value);
        }

        private string _userId;
        public string UserId
        {
            get => _userId;
            set => SetPropertyValue<IqlUserSetting, string>(nameof(UserId), _userId, value, () => _userId = value);
        }

        private string _key1;
        public string Key1
        {
            get => _key1;
            set => SetPropertyValue<IqlUserSetting, string>(nameof(Key1), _key1, value, () => _key1 = value);
        }

        private string _key2;
        public string Key2
        {
            get => _key2;
            set => SetPropertyValue<IqlUserSetting, string>(nameof(Key2), _key2, value, () => _key2 = value);
        }

        private string _key3;
        public string Key3
        {
            get => _key3;
            set => SetPropertyValue<IqlUserSetting, string>(nameof(Key3), _key3, value, () => _key3 = value);
        }

        private string _key4;
        public string Key4
        {
            get => _key4;
            set => SetPropertyValue<IqlUserSetting, string>(nameof(Key4), _key4, value, () => _key4 = value);
        }

        private string _value;
        public string Value
        {
            get => _value;
            set => SetPropertyValue<IqlUserSetting, string>(nameof(Value), _value, value, () => _value = value);
        }
    }
}