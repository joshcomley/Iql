using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Iql.Entities.DisplayFormatting;

namespace Iql.Server.Serialization.Deserialization.EntityConfiguration
{
    public class DisplayFormatting : IDisplayFormatting
    {
        public DisplayFormatting()
        {
            All = new List<IEntityDisplayTextFormatter>();
        }
        public IEnumerable<IEntityDisplayTextFormatter> All { get; set; }
        public IEntityDisplayTextFormatter Default { get; set; }
        public IEntityDisplayTextFormatter Get(string key)
        {
            throw new NotImplementedException();
        }

        public IEntityDisplayTextFormatter Set(Expression<Func<object, string>> expression, string key = null)
        {
            throw new NotImplementedException();
        }
    }
}