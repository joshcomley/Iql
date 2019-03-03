using System;
using Brandless.Data.Entities;

namespace IqlSampleApp.Data.Entities
{
    public class UserSetting : DbObjectBase<ApplicationUser, Guid>
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string Key1 { get; set; }
        public string Key2 { get; set; }
        public string Key3 { get; set; }
        public string Key4 { get; set; }
        public string Value { get; set; }
    }
}