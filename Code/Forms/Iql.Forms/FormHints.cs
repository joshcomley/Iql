namespace Iql.Forms
{
    public static class FormHints
    {
        private static string Separator = ":";
        private static string Prefix = "Iql.Forms" + Separator;
        private static string IsKnownProperty = nameof(IsKnownProperty);
        public static string CreatedDate => IsKnownProperty + Separator + nameof(CreatedDate);
        public static string CreatedByUser => IsKnownProperty + Separator + nameof(CreatedByUser);
        public static string WizardTitle = Prefix + nameof(WizardTitle);
        public static string WizardAlways = Prefix + nameof(WizardAlways);
        public static string WizardKey = Prefix + nameof(WizardKey);
        public static string WizardOrder = Prefix + nameof(WizardOrder);
        public static string WizardKeyOrder = Prefix + nameof(WizardKeyOrder);
        public static string TokenThumbnailStretch = Prefix + nameof(TokenThumbnailStretch);
        public static string AsMediaCollection = Prefix + nameof(AsMediaCollection);

        public static string EmailAddress = Prefix + nameof(EmailAddress);
        public static string InlineEdit = Prefix + nameof(InlineEdit);
        public static string BigText = Prefix + nameof(BigText);
        public static string Time = Prefix + nameof(Time);
        public static string Date = Prefix + nameof(Date);
        public static string Currency = Prefix + nameof(Currency);
        public static string CustomReport = Prefix + nameof(CustomReport);
        public static string Percentage = Prefix + nameof(Percentage);
        public static string DateAndTime = Prefix + nameof(DateAndTime);
        public static string PhoneNumber = Prefix + nameof(PhoneNumber);
        public static string CurrentUserKey = Prefix + nameof(CurrentUserKey);
        public static string SnapshotUnaffected = Prefix + nameof(SnapshotUnaffected);
        private static string HelpText = Prefix + nameof(HelpText) + ":";
        public static string HelpTextTop = HelpText + "Top";
        public static string HelpTextBottom = HelpText + "Bottom";
        public static string HelpTextHint = HelpText + "Hint";
        //public static string Geographic = Prefix + nameof(Geographic);
        //private const string Latitude = Geographic + ":" + nameof(Latitude);
        //private const string Longitude = Geographic + ":" + nameof(Longitude);
        public static string Image = Prefix + nameof(Image);
        public static string Video = Prefix + nameof(Video);
        public static string Pdf = Prefix + nameof(Pdf);
        public static string Spreadsheet = Prefix + nameof(Spreadsheet);

        public static string CustomHint(string name)
        {
            return $"{Prefix}{name}";
        }
    }
}
