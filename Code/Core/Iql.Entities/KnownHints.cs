using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Iql.Entities
{
    public class KnownHints
    {
        private const string Prefix = "Iql:";
        public const string EmailAddress = Prefix + nameof(EmailAddress);
        public const string InlineEdit = Prefix + nameof(InlineEdit);
        public const string NestedSet = Prefix + nameof(NestedSet);
        public const string NestedSetLeft = NestedSet + ":Left";
        public const string NestedSetRight = NestedSet + ":Right";
        public const string NestedSetLeftOf = NestedSet + ":LeftOf";
        public const string NestedSetRightOf = NestedSet + ":RightOf";
        public const string NestedSetKey = NestedSet + ":Key";
        public const string NestedSetLevel = NestedSet + ":Level";
        public const string NestedSetParentId = NestedSet + ":ParentId";
        public const string NestedSetParent = NestedSet + ":Parent";
        public const string NestedSetId = NestedSet + ":Id";
        public const string BigText = Prefix + nameof(BigText);
        public const string Time = Prefix + nameof(Time);
        public const string Date = Prefix + nameof(Date);
        public const string Currency = Prefix + nameof(Currency);
        public const string CustomReport = Prefix + nameof(CustomReport);
        public const string Percentage = Prefix + nameof(Percentage);
        public const string DateAndTime = Prefix + nameof(DateAndTime);
        public const string PhoneNumber = Prefix +nameof(PhoneNumber);
        private const string Range = Prefix + nameof(Range) + ":";
        public const string RangeStart = Range + "Start";
        public const string RangeEnd = Range + "End";
        private const string HelpText = Prefix + nameof(HelpText) + ":";
        public const string HelpTextTop = HelpText + "Top";
        public const string HelpTextBottom = HelpText + "Bottom";
        public const string HelpTextHint = HelpText + "Hint";
        //public const string Geographic = Prefix + nameof(Geographic);
        //private const string Latitude = Geographic + ":" + nameof(Latitude);
        //private const string Longitude = Geographic + ":" + nameof(Longitude);
        public const string Title = Prefix + nameof(Title);
        public const string Image = Prefix + nameof(Image);
        public const string Video = Prefix + nameof(Video);
        public const string File = Prefix + nameof(File);
        public const string EntityPreview = Prefix + nameof(EntityPreview);
        public const string SubTitle = Prefix + nameof(SubTitle);

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