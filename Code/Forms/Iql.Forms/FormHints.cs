namespace Iql.Forms
{
    public static class FormHints
    {
        private static string Separator = ":";
        private static string Prefix = "Iql.Forms" + Separator;
        private static string IsKnownProperty = nameof(IsKnownProperty);
        public static string CreatedDate => IsKnownProperty + Separator + nameof(CreatedDate);
        public static string CreatedByUser => IsKnownProperty + Separator + nameof(CreatedByUser);
        public static string SubTitle = Prefix + nameof(SubTitle);
        public static string WizardTitle = Prefix + nameof(WizardTitle);
        public static string WizardAlways = Prefix + nameof(WizardAlways);
        public static string WizardKey = Prefix + nameof(WizardKey);
        public static string WizardOrder = Prefix + nameof(WizardOrder);
        public static string WizardKeyOrder = Prefix + nameof(WizardKeyOrder);
        public static string TokenThumbnailStretch = Prefix + nameof(TokenThumbnailStretch);
        public static string AsMediaCollection = Prefix + nameof(AsMediaCollection);
    }
}
