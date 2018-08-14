using System;
using Iql.Data;

namespace Iql.Entities.SpecialTypes
{
    public class IqlCustomReport : EntityBase
    {
        private Guid _id;
        public Guid Id
        {
            get => _id;
            set => SetPropertyValue<IqlCustomReport, Guid>(nameof(Id), _id, value, () => _id = value);
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetPropertyValue<IqlCustomReport, string>(nameof(Name), _name, value, () => _name = value);
        }

        private string _userId;
        public string UserId
        {
            get => _userId;
            set => SetPropertyValue<IqlCustomReport, string>(nameof(UserId), _userId, value, () => _userId = value);
        }

        private string _entityType;
        public string EntityType
        {
            get => _entityType;
            set => SetPropertyValue<IqlCustomReport, string>(nameof(EntityType), _entityType, value, () => _entityType = value);
        }

        private string _iql;
        public string Iql
        {
            get => _iql;
            set => SetPropertyValue<IqlCustomReport, string>(nameof(Iql), _iql, value, () => _iql = value);
        }

        private string _fields;
        public string Fields
        {
            get => _fields;
            set => SetPropertyValue<IqlCustomReport, string>(nameof(Fields), _fields, value, () => _fields = value);
        }

        private string _sort;
        public string Sort
        {
            get => _sort;
            set => SetPropertyValue<IqlCustomReport, string>(nameof(Sort), _sort, value, () => _sort = value);
        }

        private bool _sortDescending;
        public bool SortDescending
        {
            get => _sortDescending;
            set => SetPropertyValue<IqlCustomReport, bool>(nameof(SortDescending), _sortDescending, value, () => _sortDescending = value);
        }

        private string _search;
        public string Search
        {
            get => _search;
            set => SetPropertyValue<IqlCustomReport, string>(nameof(Search), _search, value, () => _search = value);
        }
    }
}