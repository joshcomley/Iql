using System.Collections.Generic;
using System.Linq;

namespace Iql.Entities.Search
{
    public class IqlSearchText
    {
        private IqlSearchTerm[] _terms;
        private string _searchText;

        public bool Split { get; }

        public IqlSearchTerm[] Terms
        {
            get => _terms;
            set
            {
                _terms = value;
                UpdateSearchText();
            }
        }

        private void UpdateSearchText()
        {
            _searchText = Terms == null ? null : string.Join(" ", Terms.Select(t => t.AsSearchPart()));
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                UpdateSearchTerms();
            }
        }

        private void UpdateSearchTerms()
        {
            if (string.IsNullOrEmpty(_searchText))
            {
                _terms = new IqlSearchTerm[] { };
            }
            else if (Split)
            {
                _terms = ExtractSearchTerms(_searchText);
            }
            else
            {
                _terms = new IqlSearchTerm[]
                {
                    new IqlSearchTerm(_searchText, false)
                };
            }
        }

        public IqlSearchText(string text = null, bool split = true)
        {
            Split = split;
            SearchText = text;
            UpdateSearchText();
        }

        public IqlSearchTerm[] ExtractSearchTerms(string text)
        {
            bool quoteBegin = false;
            var term = "";
            var terms = new List<IqlSearchTerm>();
            for (var i = 0; i < text.Length; i++)
            {
                if (text[i] == '"')
                {
                    if (quoteBegin)
                    {
                        quoteBegin = false;
                        terms.Add(new IqlSearchTerm(term.Trim(), true));
                        term = "";
                    }
                    else
                    {
                        quoteBegin = true;
                    }
                }
                else if (text[i] == ' ')
                {
                    if (quoteBegin)
                    {
                        term += text[i];
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(term))
                        {
                            terms.Add(new IqlSearchTerm(term.Trim(), false));
                        }
                        term = "";
                    }
                }
                else
                {
                    term += text[i];
                }
            }
            if (!string.IsNullOrWhiteSpace(term))
            {
                if (quoteBegin)
                {
                    var finalTerms = term.Split(' ');
                    finalTerms[0] = "\"" + finalTerms[0];
                    terms.AddRange(finalTerms.Where(_ => !string.IsNullOrWhiteSpace(_)).Select(_ => new IqlSearchTerm(_, false)));
                }
                else
                {
                    terms.Add(new IqlSearchTerm(term.Trim(), false));
                }
            }
            return terms.ToArray();
        }
    }
}