using System;
using System.Collections.Generic;
using System.Text;

namespace Iql.Tests.Extensions
{
    public static class GuidExtensions
    {
        public static string ToGuidString(this Guid guid)
        {
            if(Equals(null, guid))
            {
                return "00000000-0000-0000-0000-000000000000";
            }

            return guid.ToString();
        }
    }
}
