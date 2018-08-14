using System;
using Iql.Data;

namespace Iql.Tests.Tests.OData
{
    public class MyCustomReport : EntityBase
    {
        private Guid _id;
        public Guid MyId
        {
            get => _id;
            set => SetPropertyValue<MyCustomReport, Guid>(nameof(MyId), _id, value, () => _id = value);
        }

        private string _myUserId;
        public string MyUserId
        {
            get => _myUserId;
            set => SetPropertyValue<MyCustomReport, string>(nameof(MyUserId), _myUserId, value, () => _myUserId = value);
        }

        private string _name;
        public string MyName
        {
            get => _name;
            set => SetPropertyValue<MyCustomReport, string>(nameof(MyName), _name, value, () => _name = value);
        }

        private string _entityType;
        public string MyEntityType
        {
            get => _entityType;
            set => SetPropertyValue<MyCustomReport, string>(nameof(MyEntityType), _entityType, value, () => _entityType = value);
        }

        private string _iql;
        public string MyIql
        {
            get => _iql;
            set => SetPropertyValue<MyCustomReport, string>(nameof(Iql), _iql, value, () => _iql = value);
        }

        private string _fields;
        public string MyFields
        {
            get => _fields;
            set => SetPropertyValue<MyCustomReport, string>(nameof(MyFields), _fields, value, () => _fields = value);
        }

        private string _sort;
        public string MySort
        {
            get => _sort;
            set => SetPropertyValue<MyCustomReport, string>(nameof(MySort), _sort, value, () => _sort = value);
        }

        private bool _sortDescending;
        public bool MySortDescending
        {
            get => _sortDescending;
            set => SetPropertyValue<MyCustomReport, bool>(nameof(MySortDescending), _sortDescending, value, () => _sortDescending = value);
        }

        private string _search;
        public string MySearch
        {
            get => _search;
            set => SetPropertyValue<MyCustomReport, string>(nameof(MySearch), _search, value, () => _search = value);
        }
    }
}