namespace Iql.Entities.Metadata
{
    public class HelpText
    {
        public string Text { get; set; }
        public HelpTextKind Kind { get; set; } = HelpTextKind.Top;

        public HelpText(string text = null, HelpTextKind kind = HelpTextKind.Top)
        {
            Text = text;
            Kind = kind;
        }

        public HelpText()
        {
        }
    }
}