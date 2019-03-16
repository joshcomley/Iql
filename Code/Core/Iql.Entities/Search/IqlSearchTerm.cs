namespace Iql.Entities.Search
{
    public class IqlSearchTerm
    {
        public string Value { get; }
        public bool IsWrappedInQuotes { get; }

        public IqlSearchTerm(string value, bool isWrappedInQuotes)
        {
            Value = value;
            IsWrappedInQuotes = isWrappedInQuotes;
        }

        public string AsSearchPart()
        {
            return IsWrappedInQuotes ? $@"""{Value}""" : Value;
        }
    }
}