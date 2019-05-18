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
    }
}
