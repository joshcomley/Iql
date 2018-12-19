namespace Iql.Data.Search
{
    public class SearchTerm
    {
        public string Value { get; set; }
        public SearchTerm(string value)
        {
            Value = value;
        }
    }
}