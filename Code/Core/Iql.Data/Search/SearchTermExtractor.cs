using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Iql.Data.Search
{
    public class SearchTermExtractor
    {
        public SearchTerm[] ExtrapolateSearchTerms(string terms)
        {
            //var terms = 'some search "terms that" we like "to see" over here "to see" again';
            var re = new Regex(@"""([^""]*?)""");
            var match = re.Match(terms);
            var quotedTerms = new List<string>();
            while (match.Success)
            {
                var value = match.Groups[1].Value;
                if (quotedTerms.All(qt => qt != value))
                {
                    quotedTerms.Add(value);
                }
                match = re.Match(terms, match.Index + match.Length);
            }

            for (var i = 0; i < quotedTerms.Count; i++)
            {
                var quotedTerm = quotedTerms[i];
                var wrappedQuotedTerm = '"' + quotedTerm + '"';
                var startIndex = 0;
                while (true)
                {
                    startIndex = terms.IndexOf(wrappedQuotedTerm, startIndex);
                    if (startIndex != -1)
                    {
                        terms = terms.Replace(wrappedQuotedTerm, "");
                    }
                    else
                    {
                        break;
                    }
                }
            }

            var simpleTerms = terms.Split(' ');
            for (var i = 0; i < simpleTerms.Length; i++)
            {
                var simpleTerm = simpleTerms[i].Trim();
                if (!string.IsNullOrWhiteSpace(simpleTerm))
                {
                    quotedTerms.Add(simpleTerms[i]);
                }
            }
                
            var searchTerms = new List<SearchTerm>();
            for (var i = 0; i < quotedTerms.Count; i++)
            {
                searchTerms.Add(new SearchTerm(quotedTerms[i]));
            }
            return searchTerms.ToArray();
        }
    }
}