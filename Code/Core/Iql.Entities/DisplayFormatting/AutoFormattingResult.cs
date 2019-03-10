namespace Iql.Entities.DisplayFormatting
{
    public class AutoFormattingResult
    {
        public AutoFormattingKind Kind { get; }

        public IProperty[] Properties { get; }

        public AutoFormattingResult(AutoFormattingKind kind, IProperty[] properties)
        {
            Kind = kind;
            Properties = properties;
        }
    }
}