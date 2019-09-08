using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Iql.Tests.Extensions
{
    public static class GuidExtensions
    {
        public static string NormalizeGuidsInJson(this string json)
        {
            return Regex.Replace(json, @"[A-Za-z0-9]{8}-[A-Za-z0-9]{4}-[A-Za-z0-9]{4}-[A-Za-z0-9]{4}-[A-Za-z0-9]{12}", "00000000-0000-0000-0000-000000000000");
        }
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
