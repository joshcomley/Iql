using System;

namespace IqlSampleApp.Data.Entities
{
    public class ApplicationLog
    {
        public Guid Id { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public string Module { get; set; }
        public string Message { get; set; }
        public string Kind { get; set; }
    }
}
