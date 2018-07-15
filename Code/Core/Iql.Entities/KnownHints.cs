using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Iql.Entities
{
    public class KnownHints
    {
        private const string Prefix = "Iql:";
        public const string EmailAddress = Prefix + "EmailAddress";
        public const string NestedSet = Prefix + "NestedSets";
        public const string NestedSetLeft = NestedSet + ":Left";
        public const string NestedSetRight = NestedSet + ":Right";
        public const string NestedSetLeftOf = NestedSet + ":LeftOf";
        public const string NestedSetRightOf = NestedSet + ":RightOf";
        public const string NestedSetKey = NestedSet + ":Key";
        public const string NestedSetLevel = NestedSet + ":Level";
        public const string NestedSetParentId = NestedSet + ":ParentId";
        public const string NestedSetParent = NestedSet + ":Parent";
        public const string NestedSetId = NestedSet + ":Id";
        public const string BigText = Prefix + "BigText";
        public const string Time = Prefix + "Time";
        public const string Date = Prefix + "Date";
        public const string Currency = Prefix + "Currency";
        public const string CustomReport = Prefix + "CustomReport";
        public const string Percentage = Prefix + "Percentage";
        public const string DateAndTime = Prefix + "DateAndTime";
        public const string PhoneNumber = Prefix + "PhoneNumber";
        private const string Range = Prefix + "Range:";
        public const string RangeStart = Range + "Start";
        public const string RangeEnd = Range + "End";
        private const string HelpText = Prefix + "HelpText:";
        public const string HelpTextTop = HelpText + "Top";
        public const string HelpTextBottom = HelpText + "Bottom";
        public const string HelpTextHint = HelpText + "Hint";
        public const string Geographic = Prefix + "Geographic";
        public const string Latitude = Geographic + ":Latitude";
        public const string Longitude = Geographic + ":Longitude";
        public const string Title = Prefix + "Title";
        public const string Image = Prefix + "Image";
        public const string Video = Prefix + "Video";
        public const string File = Prefix + "File";
        public const string EntityPreview = Prefix + "EntityPreview";
        public const string SubTitle = Prefix + "SubTitle";

        public static string FileTypeFor(string propertyName)
        {
            return $"{Prefix}FileTypeFor:{propertyName}";
        }
        public static string FileNameFor(string propertyName)
        {
            return $"{Prefix}FileNameFor:{propertyName}";
        }
        public static string PreviewFor(string propertyName)
        {
            return $"{Prefix}PreviewFor:{propertyName}";
        }
        public static string FileRevisionFor(string propertyName)
        {
            return $"{Prefix}FileRevisionFor:{propertyName}";
        }

        public const string Version = Prefix + "Version";
    }
}