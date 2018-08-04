namespace Iql.Entities
{
    public class KnownHints
    {
        private const string Prefix = "Iql:";
        public const string EmailAddress = Prefix + nameof(EmailAddress);
        public const string InlineEdit = Prefix + nameof(InlineEdit);
        public const string BigText = Prefix + nameof(BigText);
        public const string Time = Prefix + nameof(Time);
        public const string Date = Prefix + nameof(Date);
        public const string Currency = Prefix + nameof(Currency);
        public const string CustomReport = Prefix + nameof(CustomReport);
        public const string Percentage = Prefix + nameof(Percentage);
        public const string DateAndTime = Prefix + nameof(DateAndTime);
        public const string PhoneNumber = Prefix +nameof(PhoneNumber);
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
        public const string Pdf = Prefix + nameof(Pdf);
        public const string Spreadsheet = Prefix + nameof(Spreadsheet);
        public const string EntityPreview = Prefix + nameof(EntityPreview);
        public const string SubTitle = Prefix + nameof(SubTitle);

        public const string Version = Prefix + "Version";
    }
}