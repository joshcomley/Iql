using Iql.Entities.DisplayFormatting;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Iql.Server.Serialization
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